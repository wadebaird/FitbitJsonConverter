
namespace Sunbreak.FitbitJsonConverter
{
    partial class MainForm
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
            Sunbreak.FitbitJsonConverter.Properties.Settings settings1 = new Sunbreak.FitbitJsonConverter.Properties.Settings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InputFolderTextBox = new System.Windows.Forms.TextBox();
            this.OutputFileTextBox = new System.Windows.Forms.TextBox();
            this.InputFolderSelectButton = new System.Windows.Forms.Button();
            this.OutputFileSelectButton = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.OutputFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.InputFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output File";
            // 
            // InputFolderTextBox
            // 
            this.InputFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            settings1.SettingsKey = "";
            this.InputFolderTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", settings1, "InitialInputFilesFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.InputFolderTextBox.Location = new System.Drawing.Point(13, 65);
            this.InputFolderTextBox.Name = "InputFolderTextBox";
            this.InputFolderTextBox.ReadOnly = true;
            this.InputFolderTextBox.Size = new System.Drawing.Size(758, 31);
            this.InputFolderTextBox.TabIndex = 2;
            // 
            // OutputFileTextBox
            // 
            this.OutputFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputFileTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", settings1, "OutputFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.OutputFileTextBox.Location = new System.Drawing.Point(13, 160);
            this.OutputFileTextBox.Name = "OutputFileTextBox";
            this.OutputFileTextBox.Size = new System.Drawing.Size(758, 31);
            this.OutputFileTextBox.TabIndex = 3;
            // 
            // InputFolderSelectButton
            // 
            this.InputFolderSelectButton.Location = new System.Drawing.Point(134, 19);
            this.InputFolderSelectButton.Name = "InputFolderSelectButton";
            this.InputFolderSelectButton.Size = new System.Drawing.Size(112, 34);
            this.InputFolderSelectButton.TabIndex = 4;
            this.InputFolderSelectButton.Text = "Select";
            this.InputFolderSelectButton.UseVisualStyleBackColor = true;
            this.InputFolderSelectButton.Click += new System.EventHandler(this.InputFolderSelectButton_Click);
            // 
            // OutputFileSelectButton
            // 
            this.OutputFileSelectButton.Location = new System.Drawing.Point(134, 116);
            this.OutputFileSelectButton.Name = "OutputFileSelectButton";
            this.OutputFileSelectButton.Size = new System.Drawing.Size(112, 34);
            this.OutputFileSelectButton.TabIndex = 5;
            this.OutputFileSelectButton.Text = "Select";
            this.OutputFileSelectButton.UseVisualStyleBackColor = true;
            this.OutputFileSelectButton.Click += new System.EventHandler(this.OutputFileSelectButton_Click);
            // 
            // ConvertButton
            // 
            this.ConvertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConvertButton.Location = new System.Drawing.Point(659, 19);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(112, 34);
            this.ConvertButton.TabIndex = 6;
            this.ConvertButton.Text = "Convert";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // OutputFileOpenFileDialog
            // 
            this.OutputFileOpenFileDialog.CheckFileExists = false;
            this.OutputFileOpenFileDialog.DefaultExt = "*.csv";
            this.OutputFileOpenFileDialog.Tag = " ";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ConvertButton);
            this.panel1.Controls.Add(this.OutputFileSelectButton);
            this.panel1.Controls.Add(this.InputFolderSelectButton);
            this.panel1.Controls.Add(this.OutputFileTextBox);
            this.panel1.Controls.Add(this.InputFolderTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 213);
            this.panel1.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 213);
            this.Controls.Add(this.panel1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", settings1, "FormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 269);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "FitBit Json Converter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox InputFolderTextBox;
        private System.Windows.Forms.TextBox OutputFileTextBox;
        private System.Windows.Forms.Button InputFolderSelectButton;
        private System.Windows.Forms.Button OutputFileSelectButton;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.OpenFileDialog OutputFileOpenFileDialog;
        private System.Windows.Forms.FolderBrowserDialog InputFolderBrowserDialog;
        private System.Windows.Forms.Panel panel1;
    }
}

