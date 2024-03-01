
namespace Minesweeper
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
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Difficulty = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Instructions = new System.Windows.Forms.Label();
            this.pnl_Board = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Start.Location = new System.Drawing.Point(3, 3);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(145, 145);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Difficulty
            // 
            this.btn_Difficulty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Difficulty.Location = new System.Drawing.Point(3, 154);
            this.btn_Difficulty.Name = "btn_Difficulty";
            this.btn_Difficulty.Size = new System.Drawing.Size(145, 145);
            this.btn_Difficulty.TabIndex = 2;
            this.btn_Difficulty.Text = "Difficulty";
            this.btn_Difficulty.UseVisualStyleBackColor = true;
            this.btn_Difficulty.Click += new System.EventHandler(this.btn_Difficulty_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btn_Start, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Difficulty, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnl_Board, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Instructions, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(757, 606);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lbl_Instructions
            // 
            this.lbl_Instructions.AutoSize = true;
            this.lbl_Instructions.Location = new System.Drawing.Point(3, 302);
            this.lbl_Instructions.Name = "lbl_Instructions";
            this.lbl_Instructions.Size = new System.Drawing.Size(110, 39);
            this.lbl_Instructions.TabIndex = 3;
            this.lbl_Instructions.Text = "Controls:\r\nLeft click: CheckTile\r\nRight click: Place flag\r\n";
            this.lbl_Instructions.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnl_Board
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pnl_Board, 4);
            this.pnl_Board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Board.Location = new System.Drawing.Point(154, 3);
            this.pnl_Board.Name = "pnl_Board";
            this.tableLayoutPanel1.SetRowSpan(this.pnl_Board, 4);
            this.pnl_Board.Size = new System.Drawing.Size(600, 600);
            this.pnl_Board.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 606);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(773, 645);
            this.MinimumSize = new System.Drawing.Size(773, 645);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Difficulty;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_Instructions;
        private System.Windows.Forms.Panel pnl_Board;
    }
}

