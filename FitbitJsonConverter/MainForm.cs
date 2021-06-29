using Sunbreak.FitbitJsonConverter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sunbreak.FitbitJsonConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Settings.Default.FormSize != default)
            {
                Size = Settings.Default.FormSize;
            }

            if (Settings.Default.FormLocation.X < 0 || Settings.Default.FormLocation.Y < 0)
            {
                Settings.Default.FormLocation = new Point(0, 0);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Settings.Default.FormSize = Size;
            Settings.Default.Save();
        }

        private void OutputFileSelectButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Settings.Default.InitialInputFilesFolder))
            {
                OutputFileOpenFileDialog.InitialDirectory = Settings.Default.OutputFileName;
            }

            DialogResult result = OutputFileOpenFileDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(OutputFileOpenFileDialog.FileName))
            {
                OutputFileTextBox.Text = OutputFileOpenFileDialog.FileName;
            }
        }

        private void InputFolderSelectButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Settings.Default.InitialInputFilesFolder))
            {
                InputFolderBrowserDialog.SelectedPath = Settings.Default.InitialInputFilesFolder;
            }

            DialogResult result = InputFolderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(InputFolderBrowserDialog.SelectedPath))
            { 
                InputFolderTextBox.Text = InputFolderBrowserDialog.SelectedPath;
            }
        }

        private async void ConvertButton_Click(object sender, EventArgs e)
        {
            var progressBar = this.CreateProgressBar("Converting fitbit data...");

            try
            {
                await Task.Run(async () =>
                {
                    await new FtibitJsonToCsvConverter().Convert(InputFolderTextBox.Text, OutputFileTextBox.Text);
                });
            }
            catch (Exception ex)
            {
                //TODO this should be inside the Task->run... test it
                MessageBox.Show(ex.Message);
            }

            this.DestroyProgressBar(progressBar);
        }
    }
}
