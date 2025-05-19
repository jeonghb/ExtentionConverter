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
            CbConvertType = new ComboBox();
            BtnSelectTypeDelete = new Button();
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
            BtnConvert.Location = new Point(189, 40);
            BtnConvert.Name = "BtnConvert";
            BtnConvert.Size = new Size(129, 24);
            BtnConvert.TabIndex = 2;
            BtnConvert.Text = "변환";
            BtnConvert.UseVisualStyleBackColor = true;
            BtnConvert.Click += BtnConvert_Click;
            // 
            // CbConvertType
            // 
            CbConvertType.DropDownStyle = ComboBoxStyle.DropDownList;
            CbConvertType.FormattingEnabled = true;
            CbConvertType.Location = new Point(12, 41);
            CbConvertType.Name = "CbConvertType";
            CbConvertType.Size = new Size(171, 23);
            CbConvertType.TabIndex = 3;
            // 
            // BtnSelectTypeDelete
            // 
            BtnSelectTypeDelete.Location = new Point(324, 40);
            BtnSelectTypeDelete.Name = "BtnSelectTypeDelete";
            BtnSelectTypeDelete.Size = new Size(129, 24);
            BtnSelectTypeDelete.TabIndex = 4;
            BtnSelectTypeDelete.Text = "선택타입 파일 삭제";
            BtnSelectTypeDelete.UseVisualStyleBackColor = true;
            BtnSelectTypeDelete.Click += BtnSelectTypeDelete_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 72);
            Controls.Add(BtnSelectTypeDelete);
            Controls.Add(CbConvertType);
            Controls.Add(BtnConvert);
            Controls.Add(TbxDirectoryPath);
            Controls.Add(BtnDirectorySelect);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnDirectorySelect;
        private TextBox TbxDirectoryPath;
        private Button BtnConvert;
        private ComboBox CbConvertType;
        private Button BtnSelectTypeDelete;
        private ColorDialog colorDialog1;
    }
}
