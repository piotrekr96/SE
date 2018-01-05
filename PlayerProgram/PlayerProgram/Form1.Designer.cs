namespace PlayerProgram
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GetGamesBOX = new System.Windows.Forms.ComboBox();
            this.GetGamesButton = new System.Windows.Forms.Button();
            this.JoinButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.GetGamesBOX, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.GetGamesButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.JoinButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(325, 175);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // GetGamesBOX
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.GetGamesBOX, 2);
            this.GetGamesBOX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GetGamesBOX.FormattingEnabled = true;
            this.GetGamesBOX.Location = new System.Drawing.Point(3, 90);
            this.GetGamesBOX.Name = "GetGamesBOX";
            this.GetGamesBOX.Size = new System.Drawing.Size(319, 21);
            this.GetGamesBOX.TabIndex = 1;
            // 
            // GetGamesButton
            // 
            this.GetGamesButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GetGamesButton.Location = new System.Drawing.Point(43, 32);
            this.GetGamesButton.Name = "GetGamesButton";
            this.GetGamesButton.Size = new System.Drawing.Size(75, 23);
            this.GetGamesButton.TabIndex = 0;
            this.GetGamesButton.Text = "GetGames";
            this.GetGamesButton.UseVisualStyleBackColor = true;
            this.GetGamesButton.Click += new System.EventHandler(this.GetGamesButton_Click);
            // 
            // JoinButton
            // 
            this.JoinButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.JoinButton.Location = new System.Drawing.Point(206, 32);
            this.JoinButton.Name = "JoinButton";
            this.JoinButton.Size = new System.Drawing.Size(75, 23);
            this.JoinButton.TabIndex = 2;
            this.JoinButton.Text = "Join Game";
            this.JoinButton.UseVisualStyleBackColor = true;
            this.JoinButton.Click += new System.EventHandler(this.JoinButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 175);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button GetGamesButton;
        private System.Windows.Forms.ComboBox GetGamesBOX;
        private System.Windows.Forms.Button JoinButton;
    }
}

