namespace QueryTalk.Mapper
{
    partial class Step1_RepositoryUI
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
            this._ctrRepository = new System.Windows.Forms.TextBox();
            this._ctrValidationMessage = new System.Windows.Forms.Label();
            this._btnChoose = new System.Windows.Forms.Button();
            this._ctrTitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._linkNext = new System.Windows.Forms.LinkLabel();
            this._ctrFreeTrial = new System.Windows.Forms.Label();
            this._iconOK = new System.Windows.Forms.PictureBox();
            this._ctrDone = new System.Windows.Forms.Label();
            this._linkMainPage = new System.Windows.Forms.LinkLabel();
            this._linkBack = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this._iconOK)).BeginInit();
            this.SuspendLayout();
            // 
            // _ctrRepository
            // 
            this._ctrRepository.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._ctrRepository.Location = new System.Drawing.Point(241, 414);
            this._ctrRepository.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ctrRepository.Name = "_ctrRepository";
            this._ctrRepository.Size = new System.Drawing.Size(538, 42);
            this._ctrRepository.TabIndex = 8;
            // 
            // _ctrValidationMessage
            // 
            this._ctrValidationMessage.AutoSize = true;
            this._ctrValidationMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctrValidationMessage.Location = new System.Drawing.Point(253, 469);
            this._ctrValidationMessage.Name = "_ctrValidationMessage";
            this._ctrValidationMessage.Size = new System.Drawing.Size(191, 28);
            this._ctrValidationMessage.TabIndex = 9;
            this._ctrValidationMessage.Text = "(validation message)";
            // 
            // _btnChoose
            // 
            this._btnChoose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._btnChoose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnChoose.FlatAppearance.BorderSize = 0;
            this._btnChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnChoose.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this._btnChoose.ForeColor = System.Drawing.Color.White;
            this._btnChoose.Location = new System.Drawing.Point(809, 398);
            this._btnChoose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._btnChoose.Name = "_btnChoose";
            this._btnChoose.Size = new System.Drawing.Size(240, 81);
            this._btnChoose.TabIndex = 142;
            this._btnChoose.Text = "Choose folder";
            this._btnChoose.UseVisualStyleBackColor = false;
            this._btnChoose.Click += new System.EventHandler(this._btnChoose_Click);
            // 
            // _ctrTitle
            // 
            this._ctrTitle.AutoSize = true;
            this._ctrTitle.Font = new System.Drawing.Font("Segoe Print", 45F);
            this._ctrTitle.ForeColor = System.Drawing.Color.Black;
            this._ctrTitle.Location = new System.Drawing.Point(172, 49);
            this._ctrTitle.Name = "_ctrTitle";
            this._ctrTitle.Size = new System.Drawing.Size(956, 157);
            this._ctrTitle.TabIndex = 144;
            this._ctrTitle.Text = "Choose a repository";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe Print", 19F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(243, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(774, 67);
            this.label4.TabIndex = 146;
            this.label4.Text = "where all QueryTalk files will be stored";
            // 
            // _linkNext
            // 
            this._linkNext.AutoSize = true;
            this._linkNext.Font = new System.Drawing.Font("Segoe Print", 21F);
            this._linkNext.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkNext.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkNext.Location = new System.Drawing.Point(299, 649);
            this._linkNext.Name = "_linkNext";
            this._linkNext.Size = new System.Drawing.Size(703, 74);
            this._linkNext.TabIndex = 147;
            this._linkNext.TabStop = true;
            this._linkNext.Text = "Let\'s connect to the SQL server";
            this._linkNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkNext_LinkClicked);
            // 
            // _ctrFreeTrial
            // 
            this._ctrFreeTrial.AutoSize = true;
            this._ctrFreeTrial.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._ctrFreeTrial.ForeColor = System.Drawing.Color.Green;
            this._ctrFreeTrial.Location = new System.Drawing.Point(29, 15);
            this._ctrFreeTrial.Name = "_ctrFreeTrial";
            this._ctrFreeTrial.Size = new System.Drawing.Size(146, 45);
            this._ctrFreeTrial.TabIndex = 148;
            this._ctrFreeTrial.Text = "Free Trial";
            // 
            // _iconOK
            // 
            this._iconOK.Image = global::QueryTalk.Mapper.Properties.Resources.ok;
            this._iconOK.Location = new System.Drawing.Point(1119, 715);
            this._iconOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._iconOK.Name = "_iconOK";
            this._iconOK.Size = new System.Drawing.Size(85, 85);
            this._iconOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._iconOK.TabIndex = 149;
            this._iconOK.TabStop = false;
            this._iconOK.Visible = false;
            // 
            // _ctrDone
            // 
            this._ctrDone.AutoSize = true;
            this._ctrDone.BackColor = System.Drawing.Color.White;
            this._ctrDone.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._ctrDone.ForeColor = System.Drawing.Color.Green;
            this._ctrDone.Location = new System.Drawing.Point(819, 485);
            this._ctrDone.Name = "_ctrDone";
            this._ctrDone.Size = new System.Drawing.Size(83, 36);
            this._ctrDone.TabIndex = 150;
            this._ctrDone.Text = "Done.";
            this._ctrDone.Visible = false;
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
            this._linkMainPage.TabIndex = 151;
            this._linkMainPage.TabStop = true;
            this._linkMainPage.Text = "Main page";
            this._linkMainPage.Visible = false;
            this._linkMainPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkMainPage_LinkClicked);
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
            this._linkBack.TabIndex = 152;
            this._linkBack.TabStop = true;
            this._linkBack.Text = "Back";
            this._linkBack.Visible = false;
            this._linkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkBack_LinkClicked);
            // 
            // Step1_RepositoryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._linkBack);
            this.Controls.Add(this._ctrDone);
            this.Controls.Add(this._iconOK);
            this.Controls.Add(this._ctrFreeTrial);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._ctrTitle);
            this.Controls.Add(this._btnChoose);
            this.Controls.Add(this._ctrValidationMessage);
            this.Controls.Add(this._ctrRepository);
            this.Controls.Add(this._linkMainPage);
            this.Controls.Add(this._linkNext);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Step1_RepositoryUI";
            this.Size = new System.Drawing.Size(1293, 888);
            ((System.ComponentModel.ISupportInitialize)(this._iconOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox _ctrRepository;
        private System.Windows.Forms.Label _ctrValidationMessage;
        private System.Windows.Forms.Button _btnChoose;
        private System.Windows.Forms.Label _ctrTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel _linkNext;
        private System.Windows.Forms.Label _ctrFreeTrial;
        private System.Windows.Forms.PictureBox _iconOK;
        private System.Windows.Forms.Label _ctrDone;
        private System.Windows.Forms.LinkLabel _linkMainPage;
        private System.Windows.Forms.LinkLabel _linkBack;
    }
}
