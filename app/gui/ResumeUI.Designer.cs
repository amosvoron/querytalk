namespace QueryTalk.Mapper
{
    partial class ResumeUI
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
            this._messageBox = new System.Windows.Forms.RichTextBox();
            this._ctrExcluded = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._ctrFreeTrial = new System.Windows.Forms.Label();
            this._linkBack = new System.Windows.Forms.LinkLabel();
            this._ctrTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _messageBox
            // 
            this._messageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._messageBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._messageBox.Location = new System.Drawing.Point(216, 232);
            this._messageBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._messageBox.Name = "_messageBox";
            this._messageBox.Size = new System.Drawing.Size(911, 176);
            this._messageBox.TabIndex = 5;
            this._messageBox.Text = "";
            // 
            // _ctrExcluded
            // 
            this._ctrExcluded.BackColor = System.Drawing.Color.Gainsboro;
            this._ctrExcluded.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._ctrExcluded.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._ctrExcluded.Location = new System.Drawing.Point(29, 34);
            this._ctrExcluded.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ctrExcluded.Name = "_ctrExcluded";
            this._ctrExcluded.Size = new System.Drawing.Size(666, 245);
            this._ctrExcluded.TabIndex = 7;
            this._ctrExcluded.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this._ctrExcluded);
            this.panel1.Location = new System.Drawing.Point(216, 424);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 311);
            this.panel1.TabIndex = 8;
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
            this._linkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkBack_LinkClicked);
            // 
            // _ctrTitle
            // 
            this._ctrTitle.AutoSize = true;
            this._ctrTitle.BackColor = System.Drawing.Color.Transparent;
            this._ctrTitle.Font = new System.Drawing.Font("Segoe Print", 45F);
            this._ctrTitle.ForeColor = System.Drawing.Color.Black;
            this._ctrTitle.Location = new System.Drawing.Point(262, 49);
            this._ctrTitle.Name = "_ctrTitle";
            this._ctrTitle.Size = new System.Drawing.Size(812, 157);
            this._ctrTitle.TabIndex = 194;
            this._ctrTitle.Text = "Excluded objects";
            // 
            // ResumeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._ctrTitle);
            this.Controls.Add(this._ctrFreeTrial);
            this.Controls.Add(this._linkBack);
            this.Controls.Add(this._messageBox);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ResumeUI";
            this.Size = new System.Drawing.Size(1451, 888);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox _messageBox;
        private System.Windows.Forms.RichTextBox _ctrExcluded;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label _ctrFreeTrial;
        private System.Windows.Forms.LinkLabel _linkBack;
        private System.Windows.Forms.Label _ctrTitle;
    }
}
