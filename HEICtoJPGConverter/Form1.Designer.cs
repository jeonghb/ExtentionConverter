namespace HEICtoJPGConverter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BtnDirectorySelect = new Button();
            TbxDirectoryPath = new TextBox();
            BtnConvert = new Button();
            CbSourceFormat = new ComboBox();
            CbTargetFormat = new ComboBox();
            LblArrow = new Label();
            BtnSelectTypeDelete = new Button();
            PbConvert = new ProgressBar();
            LblProgress = new Label();
            colorDialog1 = new ColorDialog();
            SuspendLayout();
            //
            // BtnDirectorySelect
            //
            BtnDirectorySelect.Location = new Point(324, 12);
            BtnDirectorySelect.Name = "BtnDirectorySelect";
            BtnDirectorySelect.Size = new Size(129, 24);
            BtnDirectorySelect.TabIndex = 0;
            BtnDirectorySelect.Text = "디렉토리 선택";
            BtnDirectorySelect.UseVisualStyleBackColor = true;
            BtnDirectorySelect.Click += BtnDirectorySelect_Click;
            //
            // TbxDirectoryPath
            //
            TbxDirectoryPath.Location = new Point(12, 12);
            TbxDirectoryPath.Name = "TbxDirectoryPath";
            TbxDirectoryPath.ReadOnly = true;
            TbxDirectoryPath.Size = new Size(306, 23);
            TbxDirectoryPath.TabIndex = 1;
            //
            // BtnConvert
            //
            BtnConvert.Location = new Point(324, 40);
            BtnConvert.Name = "BtnConvert";
            BtnConvert.Size = new Size(129, 24);
            BtnConvert.TabIndex = 4;
            BtnConvert.Text = "변환";
            BtnConvert.UseVisualStyleBackColor = true;
            BtnConvert.Click += BtnConvert_Click;
            //
            // CbSourceFormat
            //
            CbSourceFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            CbSourceFormat.FormattingEnabled = true;
            CbSourceFormat.Location = new Point(12, 41);
            CbSourceFormat.Name = "CbSourceFormat";
            CbSourceFormat.Size = new Size(130, 23);
            CbSourceFormat.TabIndex = 2;
            CbSourceFormat.SelectedIndexChanged += CbSourceFormat_SelectedIndexChanged;
            //
            // LblArrow
            //
            LblArrow.AutoSize = true;
            LblArrow.Location = new Point(148, 45);
            LblArrow.Name = "LblArrow";
            LblArrow.Size = new Size(16, 15);
            LblArrow.TabIndex = 7;
            LblArrow.Text = "→";
            //
            // CbTargetFormat
            //
            CbTargetFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            CbTargetFormat.FormattingEnabled = true;
            CbTargetFormat.Location = new Point(170, 41);
            CbTargetFormat.Name = "CbTargetFormat";
            CbTargetFormat.Size = new Size(130, 23);
            CbTargetFormat.TabIndex = 3;
            //
            // BtnSelectTypeDelete
            //
            BtnSelectTypeDelete.Location = new Point(12, 70);
            BtnSelectTypeDelete.Name = "BtnSelectTypeDelete";
            BtnSelectTypeDelete.Size = new Size(129, 24);
            BtnSelectTypeDelete.TabIndex = 5;
            BtnSelectTypeDelete.Text = "선택타입 파일 삭제";
            BtnSelectTypeDelete.UseVisualStyleBackColor = true;
            BtnSelectTypeDelete.Click += BtnSelectTypeDelete_Click;
            //
            // PbConvert
            //
            PbConvert.Location = new Point(12, 100);
            PbConvert.Name = "PbConvert";
            PbConvert.Size = new Size(441, 20);
            PbConvert.TabIndex = 6;
            //
            // LblProgress
            //
            LblProgress.AutoSize = true;
            LblProgress.Location = new Point(12, 126);
            LblProgress.Name = "LblProgress";
            LblProgress.Size = new Size(0, 15);
            LblProgress.TabIndex = 8;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 155);
            Controls.Add(LblProgress);
            Controls.Add(PbConvert);
            Controls.Add(BtnSelectTypeDelete);
            Controls.Add(LblArrow);
            Controls.Add(CbTargetFormat);
            Controls.Add(CbSourceFormat);
            Controls.Add(BtnConvert);
            Controls.Add(TbxDirectoryPath);
            Controls.Add(BtnDirectorySelect);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "확장자 변환기";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnDirectorySelect;
        private TextBox TbxDirectoryPath;
        private Button BtnConvert;
        private ComboBox CbSourceFormat;
        private ComboBox CbTargetFormat;
        private Label LblArrow;
        private Button BtnSelectTypeDelete;
        private ProgressBar PbConvert;
        private Label LblProgress;
        private ColorDialog colorDialog1;
    }
}
