namespace TwitterAnalyzeEstimation
{
    partial class FormConnect
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
            this.components = new System.ComponentModel.Container();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonId = new System.Windows.Forms.Button();
            this.labelPin = new System.Windows.Forms.Label();
            this.textBoxPin = new System.Windows.Forms.TextBox();
            this.buttonPin = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // textBoxId
            // 
            this.textBoxId.AccessibleDescription = "";
            this.textBoxId.AccessibleName = "";
            this.textBoxId.Location = new System.Drawing.Point(153, 12);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(153, 19);
            this.textBoxId.TabIndex = 0;
            this.textBoxId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxId_KeyDown);
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(12, 15);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(135, 12);
            this.labelId.TabIndex = 1;
            this.labelId.Text = "TwitterのID(screen name)";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonId
            // 
            this.buttonId.Location = new System.Drawing.Point(312, 11);
            this.buttonId.Name = "buttonId";
            this.buttonId.Size = new System.Drawing.Size(80, 20);
            this.buttonId.TabIndex = 2;
            this.buttonId.Text = "認証";
            this.buttonId.UseVisualStyleBackColor = true;
            this.buttonId.Click += new System.EventHandler(this.buttonId_Click);
            // 
            // labelPin
            // 
            this.labelPin.AutoSize = true;
            this.labelPin.Location = new System.Drawing.Point(444, 15);
            this.labelPin.Name = "labelPin";
            this.labelPin.Size = new System.Drawing.Size(182, 12);
            this.labelPin.TabIndex = 3;
            this.labelPin.Text = "PIN(認証後、下画面に表示されます)";
            // 
            // textBoxPin
            // 
            this.textBoxPin.AccessibleDescription = "";
            this.textBoxPin.AccessibleName = "";
            this.textBoxPin.Location = new System.Drawing.Point(632, 12);
            this.textBoxPin.Name = "textBoxPin";
            this.textBoxPin.ReadOnly = true;
            this.textBoxPin.Size = new System.Drawing.Size(153, 19);
            this.textBoxPin.TabIndex = 4;
            this.textBoxPin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPin_KeyDown);
            // 
            // buttonPin
            // 
            this.buttonPin.Location = new System.Drawing.Point(792, 11);
            this.buttonPin.Name = "buttonPin";
            this.buttonPin.Size = new System.Drawing.Size(80, 20);
            this.buttonPin.TabIndex = 5;
            this.buttonPin.Text = "開始";
            this.buttonPin.UseVisualStyleBackColor = true;
            this.buttonPin.Click += new System.EventHandler(this.buttonPin_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(14, 37);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(858, 713);
            this.webBrowser.TabIndex = 6;
            // 
            // FormConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 762);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.buttonPin);
            this.Controls.Add(this.textBoxPin);
            this.Controls.Add(this.labelPin);
            this.Controls.Add(this.buttonId);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.textBoxId);
            this.Name = "FormConnect";
            this.Text = "Twitter評価分析ツール";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonId;
        private System.Windows.Forms.Label labelPin;
        private System.Windows.Forms.TextBox textBoxPin;
        private System.Windows.Forms.Button buttonPin;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}