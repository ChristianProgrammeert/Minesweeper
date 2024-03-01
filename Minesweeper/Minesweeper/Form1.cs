using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public int size;
        public int measure;
        public int bomb_percentage;
        public Color backcolor;
        public static SoundPlayer musicPlayer;

        private void btn_Start_Click(object sender, EventArgs e)
        {
            musicPlayer = new SoundPlayer(Resources.click);
            //Let the music loop.
            musicPlayer.Play();

            if (btn_Difficulty.Text != "Difficulty")
            {
                musicPlayer = new SoundPlayer(Resources.start);
                //Let the music loop.
                musicPlayer.Play();
                pnl_Board.Controls.Clear();
                Controller.CreateBoard(pnl_Board, size, measure, bomb_percentage, backcolor);
            }
            else
            {

                MessageBox.Show("Please select a difficulty and press start again");
            }

        }

        /// <summary>
        /// Choose the difficulty.
        /// </summary>
        /// <algo>
        /// Difficulty
        /// </algo>

        private void btn_Difficulty_Click(object sender, EventArgs e)
        {
            musicPlayer = new SoundPlayer(Resources.click);
            //Let the music loop.
            musicPlayer.Play();
            switch (btn_Difficulty.Text)
            {
                default:
                    {
                        //Button changes (next difficulty)
                        btn_Difficulty.Text = "Easy";
                        btn_Difficulty.BackColor = Color.LawnGreen;

                        //Easy values
                        size = 9;
                        measure = 60;
                        bomb_percentage = 10;
                        backcolor = Color.LightGreen;
                        break;
                    }
                case "Easy":
                    {
                        btn_Difficulty.Text = "Medium";
                        btn_Difficulty.BackColor = Color.Yellow;

                        size = 18;
                        measure = 30;
                        bomb_percentage = 20;
                        backcolor = Color.LightYellow;
                        break;
                    }
                case "Medium":
                    {
                        btn_Difficulty.Text = "Hard";
                        btn_Difficulty.BackColor = Color.DarkViolet;

                        size = 24;
                        measure = 20;
                        bomb_percentage = 30;
                        backcolor = Color.Violet;
                        break;
                    }
                case "Hard":
                    {
                        btn_Difficulty.Text = "Easy";
                        btn_Difficulty.BackColor = Color.LawnGreen;

                        size = 9;
                        measure = 60;
                        bomb_percentage = 10;
                        backcolor = Color.LightGreen;
                        break;
                    }
            }
        }
    }
}
