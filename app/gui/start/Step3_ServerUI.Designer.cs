namespace QueryTalk.Mapper
{
    partial class Step3_ServerUI
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
            this._btnConnect = new System.Windows.Forms.Button();
            this._iconAjaxLocalServers = new System.Windows.Forms.PictureBox();
            this._ctrServerLabel = new System.Windows.Forms.Label();
            this._comboServer = new System.Windows.Forms.ComboBox();
            this._checkAuthentication = new System.Windows.Forms.CheckBox();
            this._ctrLogin = new System.Windows.Forms.TextBox();
            this._ctrPassword = new System.Windows.Forms.TextBox();
            this._labelPassword = new System.Windows.Forms.Label();
            this._labelLogin = new System.Windows.Forms.Label();
            this._ctrLoginMessage = new System.Windows.Forms.Label();
            this._iconAjaxConnect = new System.Windows.Forms.PictureBox();
            this._ctrTitle = new System.Windows.Forms.Label();
            this._linkBack = new System.Windows.Forms.LinkLabel();
            this._ctrFreeTrial = new System.Windows.Forms.Label();
            this._linkNext = new System.Windows.Forms.LinkLabel();
            this._ctrDone = new System.Windows.Forms.Label();
            this._linkFailed = new System.Windows.Forms.LinkLabel();
            this._iconOK = new System.Windows.Forms.PictureBox();
            this._linkMainPage = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this._iconAjaxLocalServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconAjaxConnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconOK)).BeginInit();
            this.SuspendLayout();
            // 
            // _btnConnect
            // 
            this._btnConnect.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnConnect.FlatAppearance.BorderSize = 0;
            this._btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnConnect.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnConnect.ForeColor = System.Drawing.Color.White;
            this._btnConnect.Location = new System.Drawing.Point(798, 348);
            this._btnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._btnConnect.Name = "_btnConnect";
            this._btnConnect.Size = new System.Drawing.Size(183, 81);
            this._btnConnect.TabIndex = 133;
            this._btnConnect.Text = "Connect";
            this._btnConnect.UseVisualStyleBackColor = false;
            this._btnConnect.Click += new System.EventHandler(this._btnConnect_Click);
            // 
            // _iconAjaxLocalServers
            // 
            this._iconAjaxLocalServers.Image = global::QueryTalk.Mapper.Properties.Resources.ajax_loader;
            this._iconAjaxLocalServers.Location = new System.Drawing.Point(724, 340);
            this._iconAjaxLocalServers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._iconAjaxLocalServers.Name = "_iconAjaxLocalServers";
            this._iconAjaxLocalServers.Size = new System.Drawing.Size(37, 28);
            this._iconAjaxLocalServers.TabIndex = 146;
            this._iconAjaxLocalServers.TabStop = false;
            this._iconAjaxLocalServers.Visible = false;
            // 
            // _ctrServerLabel
            // 
            this._ctrServerLabel.AutoSize = true;
            this._ctrServerLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._ctrServerLabel.ForeColor = System.Drawing.Color.Black;
            this._ctrServerLabel.Location = new System.Drawing.Point(243, 331);
            this._ctrServerLabel.Name = "_ctrServerLabel";
            this._ctrServerLabel.Size = new System.Drawing.Size(236, 30);
            this._ctrServerLabel.TabIndex = 145;
            this._ctrServerLabel.Text = "Enter or choose server:";
            // 
            // _comboServer
            // 
            this._comboServer.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._comboServer.FormattingEnabled = true;
            this._comboServer.Location = new System.Drawing.Point(235, 366);
            this._comboServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._comboServer.Name = "_comboServer";
            this._comboServer.Size = new System.Drawing.Size(528, 44);
            this._comboServer.TabIndex = 154;
            this._comboServer.TextChanged += new System.EventHandler(this._comboServer_TextChanged);
            // 
            // _checkAuthentication
            // 
            this._checkAuthentication.AutoSize = true;
            this._checkAuthentication.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._checkAuthentication.Location = new System.Drawing.Point(264, 432);
            this._checkAuthentication.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._checkAuthentication.Name = "_checkAuthentication";
            this._checkAuthentication.Size = new System.Drawing.Size(321, 36);
            this._checkAuthentication.TabIndex = 155;
            this._checkAuthentication.Text = "SQL Server Authentication";
            this._checkAuthentication.UseVisualStyleBackColor = true;
            this._checkAuthentication.CheckedChanged += new System.EventHandler(this._checkAuthentication_CheckedChanged);
            // 
            // _ctrLogin
            // 
            this._ctrLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctrLogin.Location = new System.Drawing.Point(402, 490);
            this._ctrLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ctrLogin.Name = "_ctrLogin";
            this._ctrLogin.Size = new System.Drawing.Size(234, 34);
            this._ctrLogin.TabIndex = 156;
            this._ctrLogin.Visible = false;
            this._ctrLogin.KeyUp += new System.Windows.Forms.KeyEventHandler(this._ctrLogin_KeyUp);
            // 
            // _ctrPassword
            // 
            this._ctrPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctrPassword.Location = new System.Drawing.Point(402, 538);
            this._ctrPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ctrPassword.Name = "_ctrPassword";
            this._ctrPassword.Size = new System.Drawing.Size(234, 34);
            this._ctrPassword.TabIndex = 157;
            this._ctrPassword.UseSystemPasswordChar = true;
            this._ctrPassword.Visible = false;
            // 
            // _labelPassword
            // 
            this._labelPassword.AutoSize = true;
            this._labelPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._labelPassword.ForeColor = System.Drawing.Color.Black;
            this._labelPassword.Location = new System.Drawing.Point(288, 541);
            this._labelPassword.Name = "_labelPassword";
            this._labelPassword.Size = new System.Drawing.Size(97, 28);
            this._labelPassword.TabIndex = 158;
            this._labelPassword.Text = "Password:";
            this._labelPassword.Visible = false;
            // 
            // _labelLogin
            // 
            this._labelLogin.AutoSize = true;
            this._labelLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._labelLogin.ForeColor = System.Drawing.Color.Black;
            this._labelLogin.Location = new System.Drawing.Point(288, 494);
            this._labelLogin.Name = "_labelLogin";
            this._labelLogin.Size = new System.Drawing.Size(65, 28);
            this._labelLogin.TabIndex = 159;
            this._labelLogin.Text = "Login:";
            this._labelLogin.Visible = false;
            // 
            // _ctrLoginMessage
            // 
            this._ctrLoginMessage.AutoSize = true;
            this._ctrLoginMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctrLoginMessage.ForeColor = System.Drawing.Color.Red;
            this._ctrLoginMessage.Location = new System.Drawing.Point(649, 495);
            this._ctrLoginMessage.Name = "_ctrLoginMessage";
            this._ctrLoginMessage.Size = new System.Drawing.Size(162, 28);
            this._ctrLoginMessage.TabIndex = 160;
            this._ctrLoginMessage.Text = "Login is required.";
            this._ctrLoginMessage.Visible = false;
            // 
            // _iconAjaxConnect
            // 
            this._iconAjaxConnect.Image = global::QueryTalk.Mapper.Properties.Resources.ajax_loader;
            this._iconAjaxConnect.Location = new System.Drawing.Point(944, 321);
            this._iconAjaxConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._iconAjaxConnect.Name = "_iconAjaxConnect";
            this._iconAjaxConnect.Size = new System.Drawing.Size(37, 28);
            this._iconAjaxConnect.TabIndex = 164;
            this._iconAjaxConnect.TabStop = false;
            this._iconAjaxConnect.Visible = false;
            // 
            // _ctrTitle
            // 
            this._ctrTitle.AutoSize = true;
            this._ctrTitle.Font = new System.Drawing.Font("Segoe Print", 45F);
            this._ctrTitle.ForeColor = System.Drawing.Color.Black;
            this._ctrTitle.Location = new System.Drawing.Point(100, 61);
            this._ctrTitle.Name = "_ctrTitle";
            this._ctrTitle.Size = new System.Drawing.Size(1116, 157);
            this._ctrTitle.TabIndex = 167;
            this._ctrTitle.Text = "Connect to SQL Server";
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
            this._linkBack.TabIndex = 168;
            this._linkBack.TabStop = true;
            this._linkBack.Text = "Back";
            this._linkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkBack_LinkClicked);
            // 
            // _ctrFreeTrial
            // 
            this._ctrFreeTrial.AutoSize = true;
            this._ctrFreeTrial.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._ctrFreeTrial.ForeColor = System.Drawing.Color.Green;
            this._ctrFreeTrial.Location = new System.Drawing.Point(29, 15);
            this._ctrFreeTrial.Name = "_ctrFreeTrial";
            this._ctrFreeTrial.Size = new System.Drawing.Size(146, 45);
            this._ctrFreeTrial.TabIndex = 169;
            this._ctrFreeTrial.Text = "Free Trial";
            // 
            // _linkNext
            // 
            this._linkNext.AutoSize = true;
            this._linkNext.Font = new System.Drawing.Font("Segoe Print", 21F);
            this._linkNext.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkNext.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkNext.Location = new System.Drawing.Point(287, 649);
            this._linkNext.Name = "_linkNext";
            this._linkNext.Size = new System.Drawing.Size(715, 74);
            this._linkNext.TabIndex = 170;
            this._linkNext.TabStop = true;
            this._linkNext.Text = "Let\'s create QueryTalk database";
            this._linkNext.Visible = false;
            this._linkNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkNext_LinkClicked);
            // 
            // _ctrDone
            // 
            this._ctrDone.AutoSize = true;
            this._ctrDone.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._ctrDone.ForeColor = System.Drawing.Color.Green;
            this._ctrDone.Location = new System.Drawing.Point(804, 435);
            this._ctrDone.Name = "_ctrDone";
            this._ctrDone.Size = new System.Drawing.Size(83, 36);
            this._ctrDone.TabIndex = 171;
            this._ctrDone.Text = "Done.";
            this._ctrDone.Visible = false;
            // 
            // _linkFailed
            // 
            this._linkFailed.AutoSize = true;
            this._linkFailed.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._linkFailed.LinkColor = System.Drawing.Color.Red;
            this._linkFailed.Location = new System.Drawing.Point(804, 435);
            this._linkFailed.Name = "_linkFailed";
            this._linkFailed.Size = new System.Drawing.Size(88, 36);
            this._linkFailed.TabIndex = 172;
            this._linkFailed.TabStop = true;
            this._linkFailed.Text = "Failed!";
            this._linkFailed.Visible = false;
            this._linkFailed.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkFailed_LinkClicked);
            // 
            // _iconOK
            // 
            this._iconOK.Image = global::QueryTalk.Mapper.Properties.Resources.ok;
            this._iconOK.Location = new System.Drawing.Point(1119, 715);
            this._iconOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._iconOK.Name = "_iconOK";
            this._iconOK.Size = new System.Drawing.Size(85, 85);
            this._iconOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._iconOK.TabIndex = 173;
            this._iconOK.TabStop = false;
            this._iconOK.Visible = false;
            // 
            // _linkMainPage
            // 
            this._linkMainPage.AutoSize = true;
            this._linkMainPage.Font = new System.Drawing.Font("Segoe Print", 21F);
            this._linkMainPage.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkMainPage.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkMainPage.Location = new System.Drawing.Point(522, 649);
            this._linkMainPage.Name = "_linkMainPage";
            this._linkMainPage.Size = new System.Drawing.Size(252, 74);
            this._linkMainPage.TabIndex = 174;
            this._linkMainPage.TabStop = true;
            this._linkMainPage.Text = "Main page";
            this._linkMainPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkMainPage_LinkClicked);
            // 
            // Step3_ServerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._linkMainPage);
            this.Controls.Add(this._iconOK);
            this.Controls.Add(this._btnConnect);
            this.Controls.Add(this._ctrDone);
            this.Controls.Add(this._linkFailed);
            this.Controls.Add(this._ctrFreeTrial);
            this.Controls.Add(this._linkBack);
            this.Controls.Add(this._ctrTitle);
            this.Controls.Add(this._iconAjaxConnect);
            this.Controls.Add(this._ctrLoginMessage);
            this.Controls.Add(this._labelLogin);
            this.Controls.Add(this._labelPassword);
            this.Controls.Add(this._ctrPassword);
            this.Controls.Add(this._ctrLogin);
            this.Controls.Add(this._checkAuthentication);
            this.Controls.Add(this._comboServer);
            this.Controls.Add(this._ctrServerLabel);
            this.Controls.Add(this._iconAjaxLocalServers);
            this.Controls.Add(this._linkNext);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Step3_ServerUI";
            this.Size = new System.Drawing.Size(1308, 888);
            ((System.ComponentModel.ISupportInitialize)(this._iconAjaxLocalServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconAjaxConnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _btnConnect;
        private System.Windows.Forms.PictureBox _iconAjaxLocalServers;
        private System.Windows.Forms.Label _ctrServerLabel;
        private System.Windows.Forms.ComboBox _comboServer;
        private System.Windows.Forms.CheckBox _checkAuthentication;
        private System.Windows.Forms.TextBox _ctrLogin;
        private System.Windows.Forms.TextBox _ctrPassword;
        private System.Windows.Forms.Label _labelPassword;
        private System.Windows.Forms.Label _labelLogin;
        private System.Windows.Forms.Label _ctrLoginMessage;
        private System.Windows.Forms.PictureBox _iconAjaxConnect;
        private System.Windows.Forms.Label _ctrTitle;
        private System.Windows.Forms.LinkLabel _linkBack;
        private System.Windows.Forms.Label _ctrFreeTrial;
        private System.Windows.Forms.LinkLabel _linkNext;
        private System.Windows.Forms.Label _ctrDone;
        private System.Windows.Forms.LinkLabel _linkFailed;
        private System.Windows.Forms.PictureBox _iconOK;
        private System.Windows.Forms.LinkLabel _linkMainPage;
    }
}
