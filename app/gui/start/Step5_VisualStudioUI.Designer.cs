namespace QueryTalk.Mapper
{
    partial class Step5_VisualStudioUI
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
            this._ctrFreeTrial = new System.Windows.Forms.Label();
            this._ctrTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._btnOpenVS = new System.Windows.Forms.Button();
            this._linkNext = new System.Windows.Forms.LinkLabel();
            this._linkBack = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // _ctrFreeTrial
            // 
            this._ctrFreeTrial.AutoSize = true;
            this._ctrFreeTrial.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._ctrFreeTrial.ForeColor = System.Drawing.Color.Green;
            this._ctrFreeTrial.Location = new System.Drawing.Point(29, 15);
            this._ctrFreeTrial.Name = "_ctrFreeTrial";
            this._ctrFreeTrial.Size = new System.Drawing.Size(146, 45);
            this._ctrFreeTrial.TabIndex = 192;
            this._ctrFreeTrial.Text = "Free Trial";
            // 
            // _ctrTitle
            // 
            this._ctrTitle.AutoSize = true;
            this._ctrTitle.BackColor = System.Drawing.Color.Transparent;
            this._ctrTitle.Font = new System.Drawing.Font("Segoe Print", 45F);
            this._ctrTitle.ForeColor = System.Drawing.Color.Black;
            this._ctrTitle.Location = new System.Drawing.Point(124, 49);
            this._ctrTitle.Name = "_ctrTitle";
            this._ctrTitle.Size = new System.Drawing.Size(1060, 157);
            this._ctrTitle.TabIndex = 193;
            this._ctrTitle.Text = "Let\'s open the project";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 19F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(290, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(665, 67);
            this.label1.TabIndex = 195;
            this.label1.Text = "of code examples in Visual Studio";
            // 
            // _btnOpenVS
            // 
            this._btnOpenVS.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._btnOpenVS.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnOpenVS.FlatAppearance.BorderSize = 0;
            this._btnOpenVS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnOpenVS.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this._btnOpenVS.ForeColor = System.Drawing.Color.White;
            this._btnOpenVS.Location = new System.Drawing.Point(515, 398);
            this._btnOpenVS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._btnOpenVS.Name = "_btnOpenVS";
            this._btnOpenVS.Size = new System.Drawing.Size(240, 81);
            this._btnOpenVS.TabIndex = 196;
            this._btnOpenVS.Text = "Open it!";
            this._btnOpenVS.UseVisualStyleBackColor = false;
            this._btnOpenVS.Click += new System.EventHandler(this._btnOpenVS_Click);
            // 
            // _linkNext
            // 
            this._linkNext.AutoSize = true;
            this._linkNext.Font = new System.Drawing.Font("Segoe Print", 21F);
            this._linkNext.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkNext.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkNext.Location = new System.Drawing.Point(220, 649);
            this._linkNext.Name = "_linkNext";
            this._linkNext.Size = new System.Drawing.Size(855, 74);
            this._linkNext.TabIndex = 197;
            this._linkNext.TabStop = true;
            this._linkNext.Text = "Last step: Let\'s finally do the mapping";
            this._linkNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkNext_LinkClicked);
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
            this._linkBack.TabIndex = 198;
            this._linkBack.TabStop = true;
            this._linkBack.Text = "Back";
            this._linkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkBack_LinkClicked);
            // 
            // Step5_VisualStudioUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._linkBack);
            this.Controls.Add(this._linkNext);
            this.Controls.Add(this._btnOpenVS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._ctrTitle);
            this.Controls.Add(this._ctrFreeTrial);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Step5_VisualStudioUI";
            this.Size = new System.Drawing.Size(1308, 888);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label _ctrFreeTrial;
        private System.Windows.Forms.Label _ctrTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _btnOpenVS;
        private System.Windows.Forms.LinkLabel _linkNext;
        private System.Windows.Forms.LinkLabel _linkBack;
    }
}
