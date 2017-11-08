namespace SoftwareEngineering_project
{
    partial class CreateGame
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
            this.labelH = new System.Windows.Forms.Label();
            this.labelsmallh = new System.Windows.Forms.Label();
            this.labelW = new System.Windows.Forms.Label();
            this.BoardHeightTextBox = new System.Windows.Forms.TextBox();
            this.BoardTeamHeightTextBox = new System.Windows.Forms.TextBox();
            this.BoardWidthTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.tableLayoutPanel1.Controls.Add(this.BoardWidthTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.BoardTeamHeightTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelH, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelsmallh, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelW, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.BoardHeightTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.OkButton, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(333, 173);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelH
            // 
            this.labelH.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelH.AutoSize = true;
            this.labelH.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelH.Location = new System.Drawing.Point(20, 9);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(83, 24);
            this.labelH.TabIndex = 0;
            this.labelH.Text = "Height: ";
            // 
            // labelsmallh
            // 
            this.labelsmallh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelsmallh.AutoSize = true;
            this.labelsmallh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelsmallh.Location = new System.Drawing.Point(22, 52);
            this.labelsmallh.Name = "labelsmallh";
            this.labelsmallh.Size = new System.Drawing.Size(80, 24);
            this.labelsmallh.TabIndex = 1;
            this.labelsmallh.Text = "height: ";
            // 
            // labelW
            // 
            this.labelW.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelW.AutoSize = true;
            this.labelW.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelW.Location = new System.Drawing.Point(24, 95);
            this.labelW.Name = "labelW";
            this.labelW.Size = new System.Drawing.Size(75, 24);
            this.labelW.TabIndex = 2;
            this.labelW.Text = "Width: ";
            // 
            // BoardHeightTextBox
            // 
            this.BoardHeightTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BoardHeightTextBox.Location = new System.Drawing.Point(127, 11);
            this.BoardHeightTextBox.Name = "BoardHeightTextBox";
            this.BoardHeightTextBox.Size = new System.Drawing.Size(203, 20);
            this.BoardHeightTextBox.TabIndex = 3;
            // 
            // BoardTeamHeightTextBox
            // 
            this.BoardTeamHeightTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BoardTeamHeightTextBox.Location = new System.Drawing.Point(127, 54);
            this.BoardTeamHeightTextBox.Name = "BoardTeamHeightTextBox";
            this.BoardTeamHeightTextBox.Size = new System.Drawing.Size(203, 20);
            this.BoardTeamHeightTextBox.TabIndex = 4;
            // 
            // BoardWidthTextBox
            // 
            this.BoardWidthTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BoardWidthTextBox.Location = new System.Drawing.Point(127, 97);
            this.BoardWidthTextBox.Name = "BoardWidthTextBox";
            this.BoardWidthTextBox.Size = new System.Drawing.Size(203, 20);
            this.BoardWidthTextBox.TabIndex = 5;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.OkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OkButton.Location = new System.Drawing.Point(255, 139);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CreateGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 197);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateGame";
            this.Text = "New Game";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox BoardWidthTextBox;
        private System.Windows.Forms.TextBox BoardTeamHeightTextBox;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.Label labelsmallh;
        private System.Windows.Forms.Label labelW;
        private System.Windows.Forms.TextBox BoardHeightTextBox;
        private System.Windows.Forms.Button OkButton;
    }
}

