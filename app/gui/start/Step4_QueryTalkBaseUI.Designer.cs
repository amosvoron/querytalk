namespace QueryTalk.Mapper
{
    partial class Step4_QueryTalkBaseUI
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
            this._btnCreate = new System.Windows.Forms.Button();
            this._iconAjax = new System.Windows.Forms.PictureBox();
            this._ctrTitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._linkBack = new System.Windows.Forms.LinkLabel();
            this._linkDont = new System.Windows.Forms.LinkLabel();
            this._linkNext = new System.Windows.Forms.LinkLabel();
            this._ctrDone = new System.Windows.Forms.Label();
            this._linkFailed = new System.Windows.Forms.LinkLabel();
            this._ctrCreateLater = new System.Windows.Forms.Label();
            this._iconOK = new System.Windows.Forms.PictureBox();
            this._linkShowDatabase = new System.Windows.Forms.LinkLabel();
            this._ctrFreeTrial = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._iconAjax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconOK)).BeginInit();
            this.SuspendLayout();
            // 
            // _btnCreate
            // 
            this._btnCreate.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._btnCreate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnCreate.FlatAppearance.BorderSize = 0;
            this._btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCreate.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this._btnCreate.ForeColor = System.Drawing.Color.White;
            this._btnCreate.Location = new System.Drawing.Point(515, 398);
            this._btnCreate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._btnCreate.Name = "_btnCreate";
            this._btnCreate.Size = new System.Drawing.Size(258, 81);
            this._btnCreate.TabIndex = 133;
            this._btnCreate.Text = "Create";
            this._btnCreate.UseVisualStyleBackColor = false;
            this._btnCreate.Click += new System.EventHandler(this._btnCreate_Click);
            // 
            // _iconAjax
            // 
            this._iconAjax.Image = global::QueryTalk.Mapper.Properties.Resources.ajax_loader;
            this._iconAjax.Location = new System.Drawing.Point(736, 375);
            this._iconAjax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._iconAjax.Name = "_iconAjax";
            this._iconAjax.Size = new System.Drawing.Size(37, 28);
            this._iconAjax.TabIndex = 164;
            this._iconAjax.TabStop = false;
            this._iconAjax.Visible = false;
            // 
            // _ctrTitle
            // 
            this._ctrTitle.AutoSize = true;
            this._ctrTitle.BackColor = System.Drawing.Color.Transparent;
            this._ctrTitle.Font = new System.Drawing.Font("Segoe Print", 45F);
            this._ctrTitle.ForeColor = System.Drawing.Color.Black;
            this._ctrTitle.Location = new System.Drawing.Point(127, 49);
            this._ctrTitle.Name = "_ctrTitle";
            this._ctrTitle.Size = new System.Drawing.Size(1071, 157);
            this._ctrTitle.TabIndex = 178;
            this._ctrTitle.Text = "Create QueryTalkBase";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe Print", 19F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(261, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(770, 67);
            this.label4.TabIndex = 179;
            this.label4.Text = "the database used in all code examples";
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
            this._linkBack.TabIndex = 180;
            this._linkBack.TabStop = true;
            this._linkBack.Text = "Back";
            this._linkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkBack_LinkClicked);
            // 
            // _linkDont
            // 
            this._linkDont.AutoSize = true;
            this._linkDont.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._linkDont.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkDont.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkDont.Location = new System.Drawing.Point(471, 609);
            this._linkDont.Name = "_linkDont";
            this._linkDont.Size = new System.Drawing.Size(352, 36);
            this._linkDont.TabIndex = 183;
            this._linkDont.TabStop = true;
            this._linkDont.Text = "Well, I don\'t want to create it!";
            this._linkDont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkDont_LinkClicked);
            // 
            // _linkNext
            // 
            this._linkNext.AutoSize = true;
            this._linkNext.Font = new System.Drawing.Font("Segoe Print", 21F);
            this._linkNext.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkNext.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkNext.Location = new System.Drawing.Point(258, 649);
            this._linkNext.Name = "_linkNext";
            this._linkNext.Size = new System.Drawing.Size(808, 74);
            this._linkNext.TabIndex = 185;
            this._linkNext.TabStop = true;
            this._linkNext.Text = "Let\'s check the code in Visual Studio";
            this._linkNext.Visible = false;
            this._linkNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkNext_LinkClicked);
            // 
            // _ctrDone
            // 
            this._ctrDone.AutoSize = true;
            this._ctrDone.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._ctrDone.ForeColor = System.Drawing.Color.Green;
            this._ctrDone.Location = new System.Drawing.Point(522, 485);
            this._ctrDone.Name = "_ctrDone";
            this._ctrDone.Size = new System.Drawing.Size(83, 36);
            this._ctrDone.TabIndex = 186;
            this._ctrDone.Text = "Done.";
            this._ctrDone.Visible = false;
            // 
            // _linkFailed
            // 
            this._linkFailed.AutoSize = true;
            this._linkFailed.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._linkFailed.LinkColor = System.Drawing.Color.Red;
            this._linkFailed.Location = new System.Drawing.Point(522, 485);
            this._linkFailed.Name = "_linkFailed";
            this._linkFailed.Size = new System.Drawing.Size(88, 36);
            this._linkFailed.TabIndex = 187;
            this._linkFailed.TabStop = true;
            this._linkFailed.Text = "Failed!";
            this._linkFailed.Visible = false;
            this._linkFailed.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkFailed_LinkClicked);
            // 
            // _ctrCreateLater
            // 
            this._ctrCreateLater.AutoSize = true;
            this._ctrCreateLater.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctrCreateLater.ForeColor = System.Drawing.Color.Red;
            this._ctrCreateLater.Location = new System.Drawing.Point(290, 616);
            this._ctrCreateLater.Name = "_ctrCreateLater";
            this._ctrCreateLater.Size = new System.Drawing.Size(687, 28);
            this._ctrCreateLater.TabIndex = 188;
            this._ctrCreateLater.Text = "It is strongly recommended to create it now. (You can also postpone for later.)";
            this._ctrCreateLater.Visible = false;
            // 
            // _iconOK
            // 
            this._iconOK.Image = global::QueryTalk.Mapper.Properties.Resources.ok;
            this._iconOK.Location = new System.Drawing.Point(1119, 715);
            this._iconOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._iconOK.Name = "_iconOK";
            this._iconOK.Size = new System.Drawing.Size(85, 85);
            this._iconOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._iconOK.TabIndex = 189;
            this._iconOK.TabStop = false;
            this._iconOK.Visible = false;
            // 
            // _linkShowDatabase
            // 
            this._linkShowDatabase.AutoSize = true;
            this._linkShowDatabase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._linkShowDatabase.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkShowDatabase.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkShowDatabase.Location = new System.Drawing.Point(785, 442);
            this._linkShowDatabase.Name = "_linkShowDatabase";
            this._linkShowDatabase.Size = new System.Drawing.Size(148, 28);
            this._linkShowDatabase.TabIndex = 190;
            this._linkShowDatabase.TabStop = true;
            this._linkShowDatabase.Text = "Check database";
            this._linkShowDatabase.Visible = false;
            this._linkShowDatabase.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkSeeDatabase_LinkClicked);
            // 
            // _ctrFreeTrial
            // 
            this._ctrFreeTrial.AutoSize = true;
            this._ctrFreeTrial.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._ctrFreeTrial.ForeColor = System.Drawing.Color.Green;
            this._ctrFreeTrial.Location = new System.Drawing.Point(29, 15);
            this._ctrFreeTrial.Name = "_ctrFreeTrial";
            this._ctrFreeTrial.Size = new System.Drawing.Size(146, 45);
            this._ctrFreeTrial.TabIndex = 191;
            this._ctrFreeTrial.Text = "Free Trial";
            // 
            // Step4_QueryTalkBaseUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._iconOK);
            this.Controls.Add(this._ctrFreeTrial);
            this.Controls.Add(this._linkShowDatabase);
            this.Controls.Add(this._linkNext);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._ctrTitle);
            this.Controls.Add(this._linkBack);
            this.Controls.Add(this._btnCreate);
            this.Controls.Add(this._iconAjax);
            this.Controls.Add(this._linkDont);
            this.Controls.Add(this._ctrCreateLater);
            this.Controls.Add(this._ctrDone);
            this.Controls.Add(this._linkFailed);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Step4_QueryTalkBaseUI";
            this.Size = new System.Drawing.Size(1308, 888);
            ((System.ComponentModel.ISupportInitialize)(this._iconAjax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _btnCreate;
        private System.Windows.Forms.PictureBox _iconAjax;
        private System.Windows.Forms.Label _ctrTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel _linkBack;
        private System.Windows.Forms.LinkLabel _linkDont;
        private System.Windows.Forms.LinkLabel _linkNext;
        private System.Windows.Forms.Label _ctrDone;
        private System.Windows.Forms.LinkLabel _linkFailed;
        private System.Windows.Forms.Label _ctrCreateLater;
        private System.Windows.Forms.PictureBox _iconOK;
        private System.Windows.Forms.LinkLabel _linkShowDatabase;
        private System.Windows.Forms.Label _ctrFreeTrial;
    }
}
