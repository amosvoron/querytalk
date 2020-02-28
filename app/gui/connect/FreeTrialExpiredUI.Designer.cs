namespace QueryTalk.Mapper
{
    partial class FreeTrialExpiredUI
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
            this._btnRegister = new System.Windows.Forms.Button();
            this._linkAccount = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this._imageVideo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._imageVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // _btnRegister
            // 
            this._btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            this._btnRegister.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnRegister.FlatAppearance.BorderSize = 0;
            this._btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnRegister.ForeColor = System.Drawing.Color.White;
            this._btnRegister.Location = new System.Drawing.Point(468, 335);
            this._btnRegister.Name = "_btnRegister";
            this._btnRegister.Size = new System.Drawing.Size(197, 63);
            this._btnRegister.TabIndex = 23;
            this._btnRegister.Text = "Register";
            this._btnRegister.UseVisualStyleBackColor = false;
            this._btnRegister.Click += new System.EventHandler(this._btnRegister_Click);
            // 
            // _linkAccount
            // 
            this._linkAccount.AutoSize = true;
            this._linkAccount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._linkAccount.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkAccount.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkAccount.Location = new System.Drawing.Point(332, 429);
            this._linkAccount.Name = "_linkAccount";
            this._linkAccount.Size = new System.Drawing.Size(465, 25);
            this._linkAccount.TabIndex = 134;
            this._linkAccount.TabStop = true;
            this._linkAccount.Text = "Already registered? Click here to go to the login page.";
            this._linkAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkAccount_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 31F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(142, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(818, 91);
            this.label1.TabIndex = 148;
            this.label1.Text = "Free Trial Period has expired!";
            // 
            // _imageVideo
            // 
            this._imageVideo.Image = global::QueryTalk.Mapper.Properties.Resources.video;
            this._imageVideo.Location = new System.Drawing.Point(702, 621);
            this._imageVideo.Name = "_imageVideo";
            this._imageVideo.Size = new System.Drawing.Size(202, 57);
            this._imageVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._imageVideo.TabIndex = 201;
            this._imageVideo.TabStop = false;
            this._imageVideo.Visible = false;
            // 
            // FreeTrialExpiredUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._imageVideo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._linkAccount);
            this.Controls.Add(this._btnRegister);
            this.Name = "FreeTrialExpiredUI";
            this.Size = new System.Drawing.Size(1248, 708);
            ((System.ComponentModel.ISupportInitialize)(this._imageVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnRegister;
        private System.Windows.Forms.LinkLabel _linkAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox _imageVideo;
    }
}
