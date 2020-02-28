namespace QueryTalk.Mapper
{
    partial class NotifyForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._message = new System.Windows.Forms.RichTextBox();
            this._title = new System.Windows.Forms.Label();
            this._btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _message
            // 
            this._message.BackColor = System.Drawing.Color.White;
            this._message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._message.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._message.ForeColor = System.Drawing.Color.Red;
            this._message.Location = new System.Drawing.Point(66, 124);
            this._message.Name = "_message";
            this._message.ReadOnly = true;
            this._message.Size = new System.Drawing.Size(667, 147);
            this._message.TabIndex = 4;
            this._message.Text = "(description)";
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.BackColor = System.Drawing.Color.Transparent;
            this._title.Font = new System.Drawing.Font("Segoe Print", 27F);
            this._title.ForeColor = System.Drawing.Color.Red;
            this._title.Location = new System.Drawing.Point(53, 26);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(582, 78);
            this._title.TabIndex = 24;
            this._title.Text = "Application has crashed.";
            // 
            // _btnClose
            // 
            this._btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this._btnClose.FlatAppearance.BorderSize = 0;
            this._btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClose.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnClose.ForeColor = System.Drawing.Color.White;
            this._btnClose.Location = new System.Drawing.Point(321, 301);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(163, 65);
            this._btnClose.TabIndex = 134;
            this._btnClose.Text = "Close";
            this._btnClose.UseVisualStyleBackColor = false;
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
            // 
            // NotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 413);
            this.Controls.Add(this._btnClose);
            this.Controls.Add(this._title);
            this.Controls.Add(this._message);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NotifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QueryTalker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox _message;
        private System.Windows.Forms.Label _title;
        private System.Windows.Forms.Button _btnClose;
    }
}