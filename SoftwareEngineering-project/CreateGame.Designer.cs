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
            this.BoardWidthTextBox = new System.Windows.Forms.TextBox();
            this.BoardTeamHeightTextBox = new System.Windows.Forms.TextBox();
            this.labelH = new System.Windows.Forms.Label();
            this.labelsmallh = new System.Windows.Forms.Label();
            this.labelW = new System.Windows.Forms.Label();
            this.BoardHeightTextBox = new System.Windows.Forms.TextBox();
            this.numberOfGoals = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.tableLayoutPanel1.Controls.Add(this.BoardWidthTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.BoardTeamHeightTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelH, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelsmallh, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelW, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.BoardHeightTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.OkButton, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.numberOfGoals, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(348, 234);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // BoardWidthTextBox
            // 
            this.BoardWidthTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BoardWidthTextBox.Location = new System.Drawing.Point(133, 105);
            this.BoardWidthTextBox.Name = "BoardWidthTextBox";
            this.BoardWidthTextBox.Size = new System.Drawing.Size(203, 20);
            this.BoardWidthTextBox.TabIndex = 5;
            // 
            // BoardTeamHeightTextBox
            // 
            this.BoardTeamHeightTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BoardTeamHeightTextBox.Location = new System.Drawing.Point(133, 59);
            this.BoardTeamHeightTextBox.Name = "BoardTeamHeightTextBox";
            this.BoardTeamHeightTextBox.Size = new System.Drawing.Size(203, 20);
            this.BoardTeamHeightTextBox.TabIndex = 4;
            // 
            // labelH
            // 
            this.labelH.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelH.AutoSize = true;
            this.labelH.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelH.Location = new System.Drawing.Point(23, 11);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(83, 24);
            this.labelH.TabIndex = 0;
            this.labelH.Text = "Height: ";
            this.labelH.Click += new System.EventHandler(this.labelH_Click);
            // 
            // labelsmallh
            // 
            this.labelsmallh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelsmallh.AutoSize = true;
            this.labelsmallh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelsmallh.Location = new System.Drawing.Point(25, 57);
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
            this.labelW.Location = new System.Drawing.Point(27, 103);
            this.labelW.Name = "labelW";
            this.labelW.Size = new System.Drawing.Size(75, 24);
            this.labelW.TabIndex = 2;
            this.labelW.Text = "Width: ";
            // 
            // BoardHeightTextBox
            // 
            this.BoardHeightTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BoardHeightTextBox.Location = new System.Drawing.Point(133, 13);
            this.BoardHeightTextBox.Name = "BoardHeightTextBox";
            this.BoardHeightTextBox.Size = new System.Drawing.Size(203, 20);
            this.BoardHeightTextBox.TabIndex = 3;
            this.BoardHeightTextBox.TextChanged += new System.EventHandler(this.BoardHeightTextBox_TextChanged);
            // 
            // numberOfGoals
            // 
            this.numberOfGoals.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numberOfGoals.Location = new System.Drawing.Point(133, 151);
            this.numberOfGoals.Name = "numberOfGoals";
            this.numberOfGoals.Size = new System.Drawing.Size(203, 20);
            this.numberOfGoals.TabIndex = 7;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.OkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OkButton.Location = new System.Drawing.Point(270, 197);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(15, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nr goals: ";
            // 
            // CreateGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 276);
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
        private System.Windows.Forms.TextBox numberOfGoals;
        private System.Windows.Forms.Label label2;
    }
}

