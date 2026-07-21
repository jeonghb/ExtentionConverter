using ImageMagick;
using System.Diagnostics;

namespace HEICtoJPGConverter
{
    public partial class Form1 : Form
    {
        enum FileFormat
        {
            HEIC,
            JPG,
            PNG,
            WEBP,
            MP4,
            MP3,
            MOV,
        }

        enum ConversionEngine
        {
            Image,
            Ffmpeg,
        }

        record ConversionRule(FileFormat Source, FileFormat Target, ConversionEngine Engine, string? FfmpegArgs = null)
        {
            public string SourceExtension => Source.ToString().ToLower();
            public string TargetExtension => Target.ToString().ToLower();
        }

        private static readonly FileFormat[] LossyImageFormats = { FileFormat.JPG, FileFormat.WEBP, FileFormat.HEIC };

        private static readonly List<ConversionRule> ConversionRules = new()
        {
            // 이미지 상호변환 (HEIC / JPG / PNG / WEBP)
            new(FileFormat.HEIC, FileFormat.JPG, ConversionEngine.Image),
            new(FileFormat.HEIC, FileFormat.PNG, ConversionEngine.Image),
            new(FileFormat.HEIC, FileFormat.WEBP, ConversionEngine.Image),
            new(FileFormat.JPG, FileFormat.HEIC, ConversionEngine.Image),
            new(FileFormat.JPG, FileFormat.PNG, ConversionEngine.Image),
            new(FileFormat.JPG, FileFormat.WEBP, ConversionEngine.Image),
            new(FileFormat.PNG, FileFormat.HEIC, ConversionEngine.Image),
            new(FileFormat.PNG, FileFormat.JPG, ConversionEngine.Image),
            new(FileFormat.PNG, FileFormat.WEBP, ConversionEngine.Image),
            new(FileFormat.WEBP, FileFormat.HEIC, ConversionEngine.Image),
            new(FileFormat.WEBP, FileFormat.JPG, ConversionEngine.Image),
            new(FileFormat.WEBP, FileFormat.PNG, ConversionEngine.Image),

            // 비디오 / 오디오
            new(FileFormat.MP4, FileFormat.MP3, ConversionEngine.Ffmpeg, "-vn -c:a libmp3lame -q:a 0"),
            new(FileFormat.MOV, FileFormat.MP4, ConversionEngine.Ffmpeg, "-c:v copy -c:a copy -movflags +faststart"),
        };

        public Form1()
        {
            InitializeComponent();

            foreach (FileFormat source in ConversionRules.Select(r => r.Source).Distinct())
            {
                CbSourceFormat.Items.Add(source);
            }

            if (CbSourceFormat.Items.Count > 0)
                CbSourceFormat.SelectedIndex = 0;
        }

        private void CbSourceFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            CbTargetFormat.Items.Clear();

            if (CbSourceFormat.SelectedItem is not FileFormat source) return;

            foreach (ConversionRule rule in ConversionRules.Where(r => r.Source == source))
            {
                CbTargetFormat.Items.Add(rule.Target);
            }

            if (CbTargetFormat.Items.Count > 0)
                CbTargetFormat.SelectedIndex = 0;
        }

        private void BtnDirectorySelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog directory = new()
            {
                ShowNewFolderButton = false
            };

            if (directory.ShowDialog() != DialogResult.OK) return;

            TbxDirectoryPath.Text = directory.SelectedPath;
        }

        private async void BtnConvert_Click(object sender, EventArgs e)
        {
            if (TbxDirectoryPath.Text.Trim().Length == 0)
            {
                MessageBox.Show("디렉토리가 선택되지 않았습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CbSourceFormat.SelectedItem is not FileFormat source || CbTargetFormat.SelectedItem is not FileFormat target)
            {
                MessageBox.Show("변환 타입이 선택되지 않았습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ConversionRule? rule = ConversionRules.FirstOrDefault(r => r.Source == source && r.Target == target);
            if (rule == null)
            {
                MessageBox.Show("지원하지 않는 변환 조합입니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BtnConvert.Enabled = false;
                BtnConvert.Text = "변환 중";

                string[] files = Directory.GetFiles(TbxDirectoryPath.Text, $"*.{rule.SourceExtension}");
                InitProgress(files.Length);

                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo info = new(files[i]);
                    string outputPath = Path.Combine(TbxDirectoryPath.Text, Path.GetFileNameWithoutExtension(info.Name) + "." + rule.TargetExtension);

                    await Task.Run(() =>
                    {
                        if (rule.Engine == ConversionEngine.Image)
                            ConvertImage(info.FullName, outputPath, rule.Target);
                        else
                            ConvertWithFfmpeg(info.FullName, outputPath, rule.FfmpegArgs!);
                    });

                    UpdateProgress(i + 1, files.Length, info.Name);
                }

                MessageBox.Show("변환 완료!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("/202"))
                {
                    MessageBox.Show($"파일이 손상되었습니다.\r\n{ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("/726"))
                {
                    MessageBox.Show($"파일 확장자가 일치하지 않습니다.\r\n{ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                BtnConvert.Enabled = true;
                BtnConvert.Text = "변환";
                ResetProgress();
            }
        }

        private static void ConvertImage(string inputPath, string outputPath, FileFormat target)
        {
            using MagickImage image = new(inputPath);
            if (LossyImageFormats.Contains(target))
                image.Quality = 95;
            image.Write(outputPath);
        }

        private static void ConvertWithFfmpeg(string inputPath, string outputPath, string ffmpegArgs)
        {
            using Process process = new();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i \"{inputPath}\" {ffmpegArgs} -y \"{outputPath}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            process.Start();

            string errorOutput = process.StandardError.ReadToEnd();
            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
                throw new Exception($"변환 실패: {errorOutput}");
        }

        private void InitProgress(int total)
        {
            PbConvert.Minimum = 0;
            PbConvert.Maximum = Math.Max(total, 1);
            PbConvert.Value = 0;
            LblProgress.Text = total == 0 ? "변환할 파일이 없습니다." : $"0 / {total}";
        }

        private void UpdateProgress(int current, int total, string fileName)
        {
            PbConvert.Value = current;
            LblProgress.Text = $"{current} / {total} 완료 ({fileName})";
        }

        private void ResetProgress()
        {
            PbConvert.Value = 0;
            LblProgress.Text = "";
        }

        private void BtnSelectTypeDelete_Click(object sender, EventArgs e)
        {
            if (CbSourceFormat.SelectedItem is not FileFormat source)
            {
                MessageBox.Show("변환 타입이 선택되지 않았습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"선택한 원본 형식({source}) 파일들을 디렉토리에서 모두 삭제하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            string[] allFiles = Directory.GetFiles(TbxDirectoryPath.Text, $"*.{source.ToString().ToLower()}");
            foreach (var file in allFiles)
            {
                File.Delete(file);
            }

            MessageBox.Show("삭제 완료!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
