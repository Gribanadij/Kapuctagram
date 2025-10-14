namespace KAPUCTAgram
{
    partial class KAPUCTAgram
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
            this.MessageTB = new System.Windows.Forms.TextBox();
            this.SendB = new System.Windows.Forms.Button();
            this.ChatBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MessageTB
            // 
            this.MessageTB.AcceptsReturn = true;
            this.MessageTB.Location = new System.Drawing.Point(12, 377);
            this.MessageTB.Multiline = true;
            this.MessageTB.Name = "MessageTB";
            this.MessageTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MessageTB.Size = new System.Drawing.Size(695, 19);
            this.MessageTB.TabIndex = 0;
            this.MessageTB.TextChanged += new System.EventHandler(this.MessageTB_TextChanged);
            // 
            // SendB
            // 
            this.SendB.Location = new System.Drawing.Point(713, 373);
            this.SendB.Name = "SendB";
            this.SendB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SendB.Size = new System.Drawing.Size(75, 23);
            this.SendB.TabIndex = 1;
            this.SendB.Text = "Send";
            this.SendB.UseVisualStyleBackColor = true;
            // 
            // ChatBox
            // 
            this.ChatBox.Location = new System.Drawing.Point(12, 12);
            this.ChatBox.Multiline = true;
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ReadOnly = true;
            this.ChatBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatBox.Size = new System.Drawing.Size(776, 355);
            this.ChatBox.TabIndex = 2;
            // 
            // KAPUCTAgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.SendB);
            this.Controls.Add(this.MessageTB);
            this.Name = "KAPUCTAgram";
            this.Text = "KAPUCTAgram";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MessageTB;
        private System.Windows.Forms.Button SendB;
        private System.Windows.Forms.TextBox ChatBox;
    }
}