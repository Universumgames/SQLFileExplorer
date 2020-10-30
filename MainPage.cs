using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudyingFileExplorer
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browserDialog = new FolderBrowserDialog() { Description = "Select your path" })
            {
                if(browserDialog.ShowDialog() == DialogResult.OK)
                {
                    browser.Url = new Uri(browserDialog.SelectedPath);
                    pathBox.Text = browserDialog.SelectedPath;
                }
            }
        }

        private void pathBox_TextChanged(object sender, EventArgs e)
        {
            browser.Url = new Uri(pathBox.Text);
        }

        private void navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Console.WriteLine("navigated");
            Console.WriteLine(((WebBrowser)sender).);
        }
    }
}
