namespace QueryTalk.Mapper
{
    partial class AutoConnectUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoConnectUI));
            this._ctrTitle = new System.Windows.Forms.Label();
            this._linkCancel = new System.Windows.Forms.LinkLabel();
            this._iconAjax = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._ctrFreeTrial = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._iconAjax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _ctrTitle
            // 
            this._ctrTitle.AutoSize = true;
            this._ctrTitle.Font = new System.Drawing.Font("Segoe Print", 45F);
            this._ctrTitle.ForeColor = System.Drawing.Color.Black;
            this._ctrTitle.Location = new System.Drawing.Point(438, 85);
            this._ctrTitle.Name = "_ctrTitle";
            this._ctrTitle.Size = new System.Drawing.Size(262, 131);
            this._ctrTitle.TabIndex = 28;
            this._ctrTitle.Text = "Hello!";
            // 
            // _linkCancel
            // 
            this._linkCancel.AutoSize = true;
            this._linkCancel.Font = new System.Drawing.Font("Segoe UI", 15F);
            this._linkCancel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._linkCancel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this._linkCancel.Location = new System.Drawing.Point(438, 396);
            this._linkCancel.Name = "_linkCancel";
            this._linkCancel.Size = new System.Drawing.Size(260, 35);
            this._linkCancel.TabIndex = 134;
            this._linkCancel.TabStop = true;
            this._linkCancel.Text = "Cancel the connecting";
            this._linkCancel.Visible = false;
            this._linkCancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkCancel_LinkClicked);
            // 
            // _iconAjax
            // 
            this._iconAjax.Image = ((System.Drawing.Image)(resources.GetObject("_iconAjax.Image")));
            this._iconAjax.Location = new System.Drawing.Point(805, 329);
            this._iconAjax.Name = "_iconAjax";
            this._iconAjax.Size = new System.Drawing.Size(33, 22);
            this._iconAjax.TabIndex = 136;
            this._iconAjax.TabStop = false;
            this._iconAjax.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 19F);
            this.label2.Location = new System.Drawing.Point(180, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(725, 45);
            this.label2.TabIndex = 138;
            this.label2.Text = "Please wait. You\'re being connected to our server.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 19F);
            this.label3.Location = new System.Drawing.Point(298, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(463, 45);
            this.label3.TabIndex = 139;
            this.label3.Text = "This may take a few moments...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe Print", 55F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(134, 467);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(835, 161);
            this.label4.TabIndex = 140;
            this.label4.Text = "Simple is better!";
            // 
            // _ctrFreeTrial
            // 
            this._ctrFreeTrial.AutoSize = true;
            this._ctrFreeTrial.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._ctrFreeTrial.ForeColor = System.Drawing.Color.Green;
            this._ctrFreeTrial.Location = new System.Drawing.Point(26, 12);
            this._ctrFreeTrial.Name = "_ctrFreeTrial";
            this._ctrFreeTrial.Size = new System.Drawing.Size(123, 38);
            this._ctrFreeTrial.TabIndex = 149;
            this._ctrFreeTrial.Text = "Free Trial";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QueryTalk.Mapper.Properties.Resources.logo_250;
            this.pictureBox1.Location = new System.Drawing.Point(880, 659);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 150;
            this.pictureBox1.TabStop = false;
            // 
            // AutoConnectUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._ctrFreeTrial);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._iconAjax);
            this.Controls.Add(this._ctrTitle);
            this.Controls.Add(this._linkCancel);
            this.Name = "AutoConnectUI";
            this.Size = new System.Drawing.Size(1248, 708);
            ((System.ComponentModel.ISupportInitialize)(this._iconAjax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label _ctrTitle;
        private System.Windows.Forms.LinkLabel _linkCancel;
        private System.Windows.Forms.PictureBox _iconAjax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _ctrFreeTrial;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
