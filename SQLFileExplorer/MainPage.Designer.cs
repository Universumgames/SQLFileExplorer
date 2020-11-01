namespace SQLFileExplorer
{
    partial class MainPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pathLabel = new System.Windows.Forms.Label();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.openButton = new System.Windows.Forms.Button();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(14, 19);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(35, 13);
            this.pathLabel.TabIndex = 0;
            this.pathLabel.Text = "Path: ";
            // 
            // pathBox
            // 
            this.pathBox.Location = new System.Drawing.Point(55, 16);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(307, 20);
            this.pathBox.TabIndex = 1;
            this.pathBox.Text = "C:\\";
            this.pathBox.TextChanged += new System.EventHandler(this.pathBox_TextChanged);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(368, 13);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 2;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // browser
            // 
            this.browser.Location = new System.Drawing.Point(16, 42);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(772, 396);
            this.browser.TabIndex = 3;
            this.browser.Url = new System.Uri("C:/", System.UriKind.Absolute);
            this.browser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.navigated);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.pathBox);
            this.Controls.Add(this.pathLabel);
            this.Name = "MainPage";
            this.Text = "SQL File Explorer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.WebBrowser browser;
    }
}