namespace QueryTalk.Mapper
{
    partial class IntroUI
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
            this.label2 = new System.Windows.Forms.Label();
            this._btnGetStarted = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._ctrFreeTrial = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 45F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(382, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(529, 157);
            this.label2.TabIndex = 26;
            this.label2.Text = "Let\'s start";
            // 
            // _btnGetStarted
            // 
            this._btnGetStarted.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._btnGetStarted.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnGetStarted.FlatAppearance.BorderSize = 0;
            this._btnGetStarted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnGetStarted.Font = new System.Drawing.Font("Segoe Print", 19F, System.Drawing.FontStyle.Bold);
            this._btnGetStarted.ForeColor = System.Drawing.Color.White;
            this._btnGetStarted.Location = new System.Drawing.Point(497, 525);
            this._btnGetStarted.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._btnGetStarted.Name = "_btnGetStarted";
            this._btnGetStarted.Size = new System.Drawing.Size(281, 94);
            this._btnGetStarted.TabIndex = 41;
            this._btnGetStarted.Text = "Let\'s start!";
            this._btnGetStarted.UseVisualStyleBackColor = false;
            this._btnGetStarted.Click += new System.EventHandler(this._btnGetStarted_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.label4.Location = new System.Drawing.Point(182, 325);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 36);
            this.label4.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19F);
            this.label1.Location = new System.Drawing.Point(360, 331);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(556, 51);
            this.label1.TabIndex = 139;
            this.label1.Text = "We\'ll do some preparation stuff.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 19F);
            this.label8.Location = new System.Drawing.Point(475, 388);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(325, 51);
            this.label8.TabIndex = 140;
            this.label8.Text = "It won\'t take long!";
            // 
            // _ctrFreeTrial
            // 
            this._ctrFreeTrial.AutoSize = true;
            this._ctrFreeTrial.Font = new System.Drawing.Font("Segoe Print", 13F);
            this._ctrFreeTrial.ForeColor = System.Drawing.Color.Green;
            this._ctrFreeTrial.Location = new System.Drawing.Point(29, 15);
            this._ctrFreeTrial.Name = "_ctrFreeTrial";
            this._ctrFreeTrial.Size = new System.Drawing.Size(146, 45);
            this._ctrFreeTrial.TabIndex = 150;
            this._ctrFreeTrial.Text = "Free Trial";
            // 
            // IntroUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._ctrFreeTrial);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._btnGetStarted);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "IntroUI";
            this.Size = new System.Drawing.Size(1286, 888);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _btnGetStarted;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label _ctrFreeTrial;
    }
}
