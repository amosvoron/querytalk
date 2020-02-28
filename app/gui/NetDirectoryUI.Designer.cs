namespace QueryTalk.Mapper
{
    partial class NetDirectoryUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._ctrDirectory = new System.Windows.Forms.TextBox();
            this._ctrValidationMessage = new System.Windows.Forms.Label();
            this._messageBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._ctrTitle = new System.Windows.Forms.Label();
            this._ctrFreeTrial = new System.Windows.Forms.Label();
            this._linkBack = new System.Windows.Forms.LinkLabel();
            this._btnChoose = new System.Windows.Forms.Button();
            this._iconOK = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconOK)).BeginInit();
            this.SuspendLayout();
            // 
            // _ctrDirectory
            // 
            this._ctrDirectory.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._ctrDirectory.Location = new System.Drawing.Point(233, 345);
            this._ctrDirectory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ctrDirectory.Name = "_ctrDirectory";
            this._ctrDirectory.Size = new System.Drawing.Size(538, 42);
            this._ctrDirectory.TabIndex = 8;
            // 
            // _ctrValidationMessage
            // 
            this._ctrValidationMessage.AutoSize = true;
            this._ctrValidationMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctrValidationMessage.Location = new System.Drawing.Point(244, 399);
            this._ctrValidationMessage.Name = "_ctrValidationMessage";
            this._ctrValidationMessage.Size = new System.Drawing.Size(191, 28);
            this._ctrValidationMessage.TabIndex = 25;
            this._ctrValidationMessage.Text = "(validation message)";
            // 
            // _messageBox
            // 
            this._messageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._messageBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._messageBox.Location = new System.Drawing.Point(216, 209);
            this._messageBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._messageBox.Name = "_messageBox";
            this._messageBox.Size = new System.Drawing.Size(911, 94);
            this._messageBox.TabIndex = 27;
            this._messageBox.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QueryTalk.Mapper.Properties.Resources.net_directory;
            this.pictureBox1.Location = new System.Drawing.Point(358, 481);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(530, 265);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // _ctrTitle
            // 
            this._ctrTitle.AutoSize = true;
            this._ctrTitle.BackColor = System.Drawing.Color.Transparent;
            this._ctrTitle.Font = new System.Drawing.Font("Segoe Print", 45F);
            this._ctrTitle.ForeColor = System.Drawing.Color.Black;
            this._ctrTitle.Location = new System.Drawing.Point(204, 49);
            this._ctrTitle.Name = "_ctrTitle";
            this._ctrTitle.Size = new System.Drawing.Size(920, 157);
            this._ctrTitle.TabIndex = 179;
            this._ctrTitle.Text = "Set .NET directory";
            // 
            // _ctrFreeTrial
            // 
            this._ctrFreeTrial.AutoSize = true;
            this._ctrFreeTrial.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._ctrFreeTrial.ForeColor = System.Drawing.Color.Green;
            this._ctrFreeTrial.Location = new System.Drawing.Point(29, 15);
            this._ctrFreeTrial.Name = "_ctrFreeTrial";
            this._ctrFreeTrial.Size = new System.Drawing.Size(146, 45);
            this._ctrFreeTrial.TabIndex = 193;
            this._ctrFreeTrial.Text = "Free Trial";
            this._ctrFreeTrial.Visible = false;
            // 
            // _linkBack
            // 
            this._linkBack.AutoSize = true;
            this._linkBack.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._linkBack.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkBack.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkBack.Location = new System.Drawing.Point(68, 784);
            this._linkBack.Name = "_linkBack";
            this._linkBack.Size = new System.Drawing.Size(82, 45);
            this._linkBack.TabIndex = 192;
            this._linkBack.TabStop = true;
            this._linkBack.Text = "Back";
            this._linkBack.Visible = false;
            this._linkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkBack_LinkClicked);
            // 
            // _btnChoose
            // 
            this._btnChoose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._btnChoose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnChoose.FlatAppearance.BorderSize = 0;
            this._btnChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnChoose.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this._btnChoose.ForeColor = System.Drawing.Color.White;
            this._btnChoose.Location = new System.Drawing.Point(804, 326);
            this._btnChoose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._btnChoose.Name = "_btnChoose";
            this._btnChoose.Size = new System.Drawing.Size(240, 81);
            this._btnChoose.TabIndex = 194;
            this._btnChoose.Text = "Choose folder";
            this._btnChoose.UseVisualStyleBackColor = false;
            this._btnChoose.Click += new System.EventHandler(this._btnChoose_Click);
            // 
            // _iconOK
            // 
            this._iconOK.Image = global::QueryTalk.Mapper.Properties.Resources.ok;
            this._iconOK.Location = new System.Drawing.Point(1119, 715);
            this._iconOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._iconOK.Name = "_iconOK";
            this._iconOK.Size = new System.Drawing.Size(85, 85);
            this._iconOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._iconOK.TabIndex = 195;
            this._iconOK.TabStop = false;
            this._iconOK.Visible = false;
            // 
            // NetDirectoryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._iconOK);
            this.Controls.Add(this._btnChoose);
            this.Controls.Add(this._ctrFreeTrial);
            this.Controls.Add(this._linkBack);
            this.Controls.Add(this._ctrTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._messageBox);
            this.Controls.Add(this._ctrValidationMessage);
            this.Controls.Add(this._ctrDirectory);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NetDirectoryUI";
            this.Size = new System.Drawing.Size(1277, 888);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox _ctrDirectory;
        private System.Windows.Forms.Label _ctrValidationMessage;
        private System.Windows.Forms.RichTextBox _messageBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label _ctrTitle;
        private System.Windows.Forms.Label _ctrFreeTrial;
        private System.Windows.Forms.LinkLabel _linkBack;
        private System.Windows.Forms.Button _btnChoose;
        private System.Windows.Forms.PictureBox _iconOK;
    }
}
