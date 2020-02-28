namespace QueryTalk.Mapper
{
    partial class AccountUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountUI));
            this._ctrEmail = new System.Windows.Forms.TextBox();
            this._ctrEmailLabel = new System.Windows.Forms.Label();
            this._ctrPassword = new System.Windows.Forms.TextBox();
            this._ctrPasswordLabel = new System.Windows.Forms.Label();
            this._ctrTitleDesc = new System.Windows.Forms.Label();
            this._btnConnect = new System.Windows.Forms.Button();
            this._ctrDenied = new System.Windows.Forms.Label();
            this._iconAjax = new System.Windows.Forms.PictureBox();
            this._ctrTitle = new System.Windows.Forms.Label();
            this._checkAutoconnect = new System.Windows.Forms.CheckBox();
            this._ctrCancel = new System.Windows.Forms.LinkLabel();
            this._iconEye = new System.Windows.Forms.PictureBox();
            this._ctrPasswordVal = new System.Windows.Forms.Label();
            this._ctrEmailVal = new System.Windows.Forms.Label();
            this._linkDeniedDescription = new System.Windows.Forms.LinkLabel();
            this._linkConnectAsAnonymous = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this._iconAjax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconEye)).BeginInit();
            this.SuspendLayout();
            // 
            // _ctrEmail
            // 
            this._ctrEmail.BackColor = System.Drawing.Color.White;
            this._ctrEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._ctrEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this._ctrEmail.ForeColor = System.Drawing.Color.Black;
            this._ctrEmail.Location = new System.Drawing.Point(418, 256);
            this._ctrEmail.Name = "_ctrEmail";
            this._ctrEmail.Size = new System.Drawing.Size(368, 36);
            this._ctrEmail.TabIndex = 12;
            // 
            // _ctrEmailLabel
            // 
            this._ctrEmailLabel.AutoSize = true;
            this._ctrEmailLabel.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._ctrEmailLabel.ForeColor = System.Drawing.Color.Black;
            this._ctrEmailLabel.Location = new System.Drawing.Point(276, 257);
            this._ctrEmailLabel.Name = "_ctrEmailLabel";
            this._ctrEmailLabel.Size = new System.Drawing.Size(69, 30);
            this._ctrEmailLabel.TabIndex = 11;
            this._ctrEmailLabel.Text = "Email:";
            // 
            // _ctrPassword
            // 
            this._ctrPassword.BackColor = System.Drawing.Color.White;
            this._ctrPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._ctrPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this._ctrPassword.ForeColor = System.Drawing.Color.Black;
            this._ctrPassword.Location = new System.Drawing.Point(418, 306);
            this._ctrPassword.Name = "_ctrPassword";
            this._ctrPassword.PasswordChar = '*';
            this._ctrPassword.Size = new System.Drawing.Size(368, 36);
            this._ctrPassword.TabIndex = 14;
            // 
            // _ctrPasswordLabel
            // 
            this._ctrPasswordLabel.AutoSize = true;
            this._ctrPasswordLabel.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._ctrPasswordLabel.ForeColor = System.Drawing.Color.Black;
            this._ctrPasswordLabel.Location = new System.Drawing.Point(276, 307);
            this._ctrPasswordLabel.Name = "_ctrPasswordLabel";
            this._ctrPasswordLabel.Size = new System.Drawing.Size(108, 30);
            this._ctrPasswordLabel.TabIndex = 13;
            this._ctrPasswordLabel.Text = "Password:";
            // 
            // _ctrTitleDesc
            // 
            this._ctrTitleDesc.AutoSize = true;
            this._ctrTitleDesc.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._ctrTitleDesc.ForeColor = System.Drawing.Color.Black;
            this._ctrTitleDesc.Location = new System.Drawing.Point(407, 158);
            this._ctrTitleDesc.Name = "_ctrTitleDesc";
            this._ctrTitleDesc.Size = new System.Drawing.Size(313, 38);
            this._ctrTitleDesc.TabIndex = 16;
            this._ctrTitleDesc.Text = "to your QueryTalk account";
            // 
            // _btnConnect
            // 
            this._btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            this._btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnConnect.FlatAppearance.BorderSize = 0;
            this._btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnConnect.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnConnect.ForeColor = System.Drawing.Color.White;
            this._btnConnect.Location = new System.Drawing.Point(468, 429);
            this._btnConnect.Name = "_btnConnect";
            this._btnConnect.Size = new System.Drawing.Size(197, 63);
            this._btnConnect.TabIndex = 18;
            this._btnConnect.Text = "Connect";
            this._btnConnect.UseVisualStyleBackColor = false;
            // 
            // _ctrDenied
            // 
            this._ctrDenied.AutoSize = true;
            this._ctrDenied.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._ctrDenied.ForeColor = System.Drawing.Color.Red;
            this._ctrDenied.Location = new System.Drawing.Point(276, 525);
            this._ctrDenied.Name = "_ctrDenied";
            this._ctrDenied.Size = new System.Drawing.Size(243, 25);
            this._ctrDenied.TabIndex = 19;
            this._ctrDenied.Text = "(Denied response message)";
            this._ctrDenied.Visible = false;
            // 
            // _iconAjax
            // 
            this._iconAjax.Image = ((System.Drawing.Image)(resources.GetObject("_iconAjax.Image")));
            this._iconAjax.Location = new System.Drawing.Point(632, 411);
            this._iconAjax.Name = "_iconAjax";
            this._iconAjax.Size = new System.Drawing.Size(33, 22);
            this._iconAjax.TabIndex = 22;
            this._iconAjax.TabStop = false;
            this._iconAjax.Visible = false;
            // 
            // _ctrTitle
            // 
            this._ctrTitle.AutoSize = true;
            this._ctrTitle.Font = new System.Drawing.Font("Segoe Print", 45F);
            this._ctrTitle.ForeColor = System.Drawing.Color.Black;
            this._ctrTitle.Location = new System.Drawing.Point(420, 38);
            this._ctrTitle.Name = "_ctrTitle";
            this._ctrTitle.Size = new System.Drawing.Size(320, 131);
            this._ctrTitle.TabIndex = 23;
            this._ctrTitle.Text = "Sign in";
            // 
            // _checkAutoconnect
            // 
            this._checkAutoconnect.AutoSize = true;
            this._checkAutoconnect.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._checkAutoconnect.ForeColor = System.Drawing.Color.Black;
            this._checkAutoconnect.Location = new System.Drawing.Point(418, 364);
            this._checkAutoconnect.Name = "_checkAutoconnect";
            this._checkAutoconnect.Size = new System.Drawing.Size(141, 29);
            this._checkAutoconnect.TabIndex = 24;
            this._checkAutoconnect.Text = "Autoconnect";
            this._checkAutoconnect.UseVisualStyleBackColor = true;
            // 
            // _ctrCancel
            // 
            this._ctrCancel.AutoSize = true;
            this._ctrCancel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._ctrCancel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._ctrCancel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._ctrCancel.Location = new System.Drawing.Point(586, 496);
            this._ctrCancel.Name = "_ctrCancel";
            this._ctrCancel.Size = new System.Drawing.Size(69, 28);
            this._ctrCancel.TabIndex = 33;
            this._ctrCancel.TabStop = true;
            this._ctrCancel.Text = "Cancel";
            this._ctrCancel.Visible = false;
            // 
            // _iconEye
            // 
            this._iconEye.BackColor = System.Drawing.Color.White;
            this._iconEye.Image = global::QueryTalk.Mapper.Properties.Resources.eye;
            this._iconEye.Location = new System.Drawing.Point(728, 308);
            this._iconEye.Name = "_iconEye";
            this._iconEye.Size = new System.Drawing.Size(57, 35);
            this._iconEye.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._iconEye.TabIndex = 34;
            this._iconEye.TabStop = false;
            // 
            // _ctrPasswordVal
            // 
            this._ctrPasswordVal.AutoSize = true;
            this._ctrPasswordVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctrPasswordVal.ForeColor = System.Drawing.Color.Red;
            this._ctrPasswordVal.Location = new System.Drawing.Point(795, 316);
            this._ctrPasswordVal.Name = "_ctrPasswordVal";
            this._ctrPasswordVal.Size = new System.Drawing.Size(162, 23);
            this._ctrPasswordVal.TabIndex = 35;
            this._ctrPasswordVal.Text = "Password is missing.";
            this._ctrPasswordVal.Visible = false;
            // 
            // _ctrEmailVal
            // 
            this._ctrEmailVal.AutoSize = true;
            this._ctrEmailVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctrEmailVal.ForeColor = System.Drawing.Color.Red;
            this._ctrEmailVal.Location = new System.Drawing.Point(795, 267);
            this._ctrEmailVal.Name = "_ctrEmailVal";
            this._ctrEmailVal.Size = new System.Drawing.Size(133, 23);
            this._ctrEmailVal.TabIndex = 36;
            this._ctrEmailVal.Text = "Email is missing.";
            this._ctrEmailVal.Visible = false;
            // 
            // _linkDeniedDescription
            // 
            this._linkDeniedDescription.AutoSize = true;
            this._linkDeniedDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._linkDeniedDescription.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkDeniedDescription.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkDeniedDescription.Location = new System.Drawing.Point(277, 550);
            this._linkDeniedDescription.Name = "_linkDeniedDescription";
            this._linkDeniedDescription.Size = new System.Drawing.Size(161, 23);
            this._linkDeniedDescription.TabIndex = 39;
            this._linkDeniedDescription.TabStop = true;
            this._linkDeniedDescription.Text = "(denied description)";
            this._linkDeniedDescription.Visible = false;
            // 
            // _linkConnectAsAnonymous
            // 
            this._linkConnectAsAnonymous.AutoSize = true;
            this._linkConnectAsAnonymous.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._linkConnectAsAnonymous.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkConnectAsAnonymous.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkConnectAsAnonymous.Location = new System.Drawing.Point(692, 450);
            this._linkConnectAsAnonymous.Name = "_linkConnectAsAnonymous";
            this._linkConnectAsAnonymous.Size = new System.Drawing.Size(171, 28);
            this._linkConnectAsAnonymous.TabIndex = 41;
            this._linkConnectAsAnonymous.TabStop = true;
            this._linkConnectAsAnonymous.Text = "Go to Trial Version";
            this._linkConnectAsAnonymous.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkConnectAsAnonymous_LinkClicked);
            // 
            // AccountUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._linkConnectAsAnonymous);
            this.Controls.Add(this._ctrTitleDesc);
            this.Controls.Add(this._linkDeniedDescription);
            this.Controls.Add(this._ctrEmailVal);
            this.Controls.Add(this._ctrPasswordVal);
            this.Controls.Add(this._iconEye);
            this.Controls.Add(this._ctrCancel);
            this.Controls.Add(this._checkAutoconnect);
            this.Controls.Add(this._ctrTitle);
            this.Controls.Add(this._btnConnect);
            this.Controls.Add(this._iconAjax);
            this.Controls.Add(this._ctrDenied);
            this.Controls.Add(this._ctrPassword);
            this.Controls.Add(this._ctrPasswordLabel);
            this.Controls.Add(this._ctrEmail);
            this.Controls.Add(this._ctrEmailLabel);
            this.Name = "AccountUI";
            this.Size = new System.Drawing.Size(1248, 710);
            ((System.ComponentModel.ISupportInitialize)(this._iconAjax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconEye)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _ctrEmail;
        private System.Windows.Forms.Label _ctrEmailLabel;
        private System.Windows.Forms.TextBox _ctrPassword;
        private System.Windows.Forms.Label _ctrPasswordLabel;
        private System.Windows.Forms.Label _ctrTitleDesc;
        private System.Windows.Forms.Button _btnConnect;
        private System.Windows.Forms.Label _ctrDenied;
        private System.Windows.Forms.PictureBox _iconAjax;
        private System.Windows.Forms.Label _ctrTitle;
        private System.Windows.Forms.CheckBox _checkAutoconnect;
        private System.Windows.Forms.LinkLabel _ctrCancel;
        private System.Windows.Forms.PictureBox _iconEye;
        private System.Windows.Forms.Label _ctrPasswordVal;
        private System.Windows.Forms.Label _ctrEmailVal;
        private System.Windows.Forms.LinkLabel _linkDeniedDescription;
        private System.Windows.Forms.LinkLabel _linkConnectAsAnonymous;
    }
}
