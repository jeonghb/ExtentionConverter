using ImageMagick;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace HEICtoJPGConverter
{
    public partial class Form1 : Form
    {
        enum ConvertType
        {
            [Description("HEIC -> JPG")]
            HEICtoJPG,
            [Description("MP4 -> MP3")]
            Mp4toMp3,
        }

        public Form1()
        {
            InitializeComponent();

            foreach (ConvertType type in Enum.GetValues(typeof(ConvertType)))
            {
                string description = GetEnumDescription(type);
                if (description != "") CbConvertType.Items.Add(new KeyValuePair<ConvertType, string>(type, description));
            }

            CbConvertType.DisplayMember = "Value";
            CbConvertType.ValueMember = "Key";

            if (CbConvertType.Items.Count > 0)
                CbConvertType.SelectedIndex = 0;
        }

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo? fi = value.GetType().GetField(value.ToString());
            if (fi == null) return "";

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
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

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            if (TbxDirectoryPath.Text.Trim().Length == 0)
            {
                MessageBox.Show("디렉토리가 선택되지 않았어요.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BtnConvert.Enabled = false;
                BtnConvert.Text = "변환 중";

                if (CbConvertType.SelectedItem == null)
                {
                    MessageBox.Show("변환 타입이 선택되지 않았어요.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                switch (((KeyValuePair<ConvertType, string>)CbConvertType.SelectedItem).Key)
                {
                    case ConvertType.HEICtoJPG:
                        string[] allfiles = Directory.GetFiles(TbxDirectoryPath.Text, "*.heic");

                        foreach (var file in allfiles)
                        {
                            FileInfo info = new(file);
                            using (MagickImage image = new(info.FullName))
                            {
                                // Save frame as jpg
                                image.Write($"{TbxDirectoryPath.Text}\\{info.Name.Replace(".heic", "")}.jpg");
                            }
                        }
                        break;
                    case ConvertType.Mp4toMp3:
                        string[] allMp4Files = Directory.GetFiles(TbxDirectoryPath.Text, "*.mp4");
                        foreach (var file in allMp4Files)
                        {
                            FileInfo info = new(file);
                            string outputPath = Path.Combine(TbxDirectoryPath.Text, Path.GetFileNameWithoutExtension(info.Name) + ".mp3");

                            using (Process process = new())
                            {
                                process.StartInfo = new ProcessStartInfo
                                {
                                    FileName = "ffmpeg",
                                    Arguments = $"-i \"{info.FullName}\" -vn -ab 192k -ar 44100 -y \"{outputPath}\"",
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
                        }
                        break;
                }

                MessageBox.Show("변환 완료!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BtnConvert.Enabled = true;
                BtnConvert.Text = "변환";
            }
        }

        private void BtnSelectTypeDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 타입의 파일들을 디렉토리에서 전부 제거할건가요?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            if (CbConvertType.SelectedItem == null)
            {
                MessageBox.Show("변환 타입이 선택되지 않았어요.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] allFiles;
            switch (((KeyValuePair<ConvertType, string>)(CbConvertType.SelectedItem)).Key)
            {
                case ConvertType.HEICtoJPG:
                    allFiles = Directory.GetFiles(TbxDirectoryPath.Text, "*.heic");

                    foreach (var file in allFiles)
                    {
                        File.Delete(file);
                    }
                    break;
                case ConvertType.Mp4toMp3:
                    allFiles = Directory.GetFiles(TbxDirectoryPath.Text, "*.mp4");
                    foreach (var file in allFiles)
                    {
                        File.Delete(file);
                    }
                    break;
            }

            MessageBox.Show("제거 완료!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
