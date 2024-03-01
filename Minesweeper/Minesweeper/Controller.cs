using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    class Cell : PictureBox
    {
        public bool Bomb = false;
        public int Number = 0;
        public bool Revealed = false;
    }

    //TODO
    //Add a way to get a great amount of empty cells opened
    //Make it so you cant use the board anymore if you died.
    //Timer?

    class Controller
    {
        //Declare variables
        static int measure;
        static int size;
        static int bomb_percentage;
        static Color backcolor;
        public static Cell[,] grid;
        public static Random RNG;
        public static int[] neigboring_x = { -1, 0, 1, -1, 1, -1, 0, 1 };
        public static int[] neigboring_y = { -1, -1, -1, 0, 0, 1, 1, 1 };
        //Create a soundplayer for the Sound effects.
        public static SoundPlayer musicPlayer;

        public static Panel panel;
        public static Form RockPaperScisssorForm;
        public static bool rockPaperScissorsPlayed = false;


        /// <summary>
        /// 9x9 for Easy
        /// 18x18 for Medium
        /// 36x36 for Hard
        /// Creates a Minesweeper game board based on the specified parameters.
        /// Initializes the game board, sets up the bomb positions, and calculates the numbers for non-bomb cells.
        /// </summary>
        internal static void CreateBoard(Panel pnl_Board, int size, int measure, int bomb_percentage, Color backcolor)
        {
            Controller.panel = pnl_Board;
            Controller.size = size;
            Controller.measure = measure;
            Controller.bomb_percentage = bomb_percentage;
            Controller.backcolor = backcolor;   
            grid = new Cell[size, size];
            RNG = new Random();

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    grid[row, column] = new Cell();
                    grid[row, column].Size = new Size(measure, measure);
                    grid[row, column].Location = new Point(column * measure, row * measure);
                    grid[row, column].BorderStyle = BorderStyle.FixedSingle;
                    grid[row, column].Font = new Font(grid[row, column].Font, FontStyle.Bold);
                    grid[row, column].MouseClick += Cell_MouseClick;
                    grid[row, column].SizeMode = PictureBoxSizeMode.Zoom;
                    grid[row, column].Image = null;

                    panel.Controls.Add(grid[row, column]);
                }
            }

            //Get the percentage of the bomb based length and width
            int bombAmount = size * size * bomb_percentage / 100;


            for (int i = 0; i <= bombAmount; i++)
            {
                int random1 = RNG.Next(0, size);
                int random2 = RNG.Next(0, size);
                grid[random1, random2].Bomb = true;
            }

            // Set the numbers for non-bomb cells after placing bombs
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    if (!grid[row, column].Bomb)
                    {
                        int bombCount = CountNeighboringBombs(row, column, size);
                        if (bombCount > 0)
                        {
                            //Its not possible to use variable name in a resource call so i have to use a switch case
                            grid[row, column].Number = bombCount;   
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Counts the number of neighboring bombs for a given cell on the Minesweeper game board.
        /// </summary>
        private static int CountNeighboringBombs(int row, int column, int gridSize)
        {
            int bombCount = 0;

            // Define the eight possible neighboring positions


            for (int i = 0; i < 8; i++)
            {
                int currentRow = row + neigboring_x[i];
                int currentColumn = column + neigboring_y[i];

                // Check if the neighboring position is within the grid bounds
                if (currentRow >= 0 && currentRow < gridSize && currentColumn >= 0 && currentColumn < gridSize)
                {
                    // Check if the neighboring cell contains a bomb
                    if (grid[currentRow, currentColumn].Bomb)
                    {
                        bombCount++;
                    }
                }
            }

            return bombCount;
        }

        /// <summary>
        /// Handles the mouse click event on a cell in a Minesweeper game.
        /// Reveals the content of the clicked cell based on the mouse button clicked (left or right).
        /// If the cell contains a bomb, it provides the player with a second chance through a Rock, Paper, Scissors game.
        /// If the cell contains a number, it reveals the number; otherwise, it reveals an empty cell and its adjacent empty cells.
        /// </summary>
        /// <algo>
        /// 1. Get the current clicked cell from the event sender.
        /// 2. Calculate the row and column of the clicked cell based on its location and the cell size (measure).
        /// 3. Play a click sound using a SoundPlayer.
        /// 4. If the left mouse button is clicked:
        ///    a. Check if the cell contains a bomb:
        ///       - If this is the case show a message box offering a second chance through Rock, Paper, Scissors.
        ///       - If the player accepts the second chance it shows the Rock, Paper, Scissors pop-up.
        ///       - If the player declines the second chance, play losing sounds and disable further clicks on the bombed cell.
        ///    b. If the cell contains a number, reveal the number.
        ///    c. If the cell is empty, reveal it and its adjacent empty cells using the RevealEmptyCell method.
        /// 5. If the right mouse button is clicked, set the cell to display a flag image.
        /// </algo>
        private static void Cell_MouseClick(object sender, MouseEventArgs e)
        {
            //Get the current cell
            Cell clickedCell = (Cell)sender; 

            // Get the row and column of the clicked cell
            int row = clickedCell.Location.Y / measure;
            int column = clickedCell.Location.X / measure;
            DialogResult result = DialogResult.No;
            SoundPlayer musicPlayer = new SoundPlayer(Resources.click);
            musicPlayer.Play();
            if (e.Button == MouseButtons.Left)
            {
                if(grid[row, column].Bomb == true)
                {
                    // Play losing sound and show rock, paper scissors pop-up
                    musicPlayer = new SoundPlayer(Resources.lose_minesweeper);
                    musicPlayer.Play();
                    grid[row, column].BackColor = backcolor;
                    grid[row, column].Image = Resources.Mine;
                    // Play Rock, Paper, Scissors for a second chance (if you haven't played it already)

                    if (!rockPaperScissorsPlayed) 
                    {
                        result = MessageBox.Show("Oh no! You hit a bomb! Do you want to get a second chance by playing a game of Rock, Paper, Scissors?", "You blew up :(", MessageBoxButtons.YesNo);

                    }
                    musicPlayer = new SoundPlayer(Resources.click);
                    musicPlayer.Play();
                    if (result == DialogResult.Yes && !rockPaperScissorsPlayed)
                    {
                        ShowRockPaperScissorsPopup();
                        rockPaperScissorsPlayed = true;
                    }
                    else
                    {
                        musicPlayer = new SoundPlayer(Resources.lose_minesweeper);
                        musicPlayer.Play();
                        MessageBox.Show("Better luck next time", "You blew up :(");
                        musicPlayer = new SoundPlayer(Resources.lose_flowergarden_medium);
                        musicPlayer.Play();
                        panel.Controls.Clear();
                        rockPaperScissorsPlayed = false;
                    }

                }
                if (grid[row,column].Number > 0)
                {
                    // If the cell has a number, reveal the number and stop
                    grid[row, column].Image = GetNumberImage(grid[row, column].Number);
                    grid[row, column].BackColor = backcolor;
                }
                else
                {
                    // If the cell is empty, reveal it and adjacent cells
                    RevealEmptyCell(row, column);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                grid[row, column].BackColor = backcolor;
                grid[row, column].Image = Resources.Flag;
                //Create a music player.
                musicPlayer = new SoundPlayer(Resources.click);
                musicPlayer.Play();
            }
        }

        /// <summary>
        /// Shows a Rock, Paper, Scissors pop-up form for a second chance after hitting a bomb.
        /// The pop-up form displays images of Rock, Paper, and Scissors as pictureboxes with clickevents, allowing the player to make a choice.
        /// The result is determined by comparing the player's choice with the computer's randomly generated choice.
        /// If the player wins or ties, they can continue playing Minesweeper; otherwise, game over :(.
        /// </summary>
        /// <algo>
        /// 1. Create a new form for the Rock, Paper, Scissors pop-up.
        /// 2. Create a TableLayoutPanel within the form to organize Rock, Paper, and Scissors images in a grid.
        /// 3. Create picture boxes for Rock, Paper, and Scissors, set their properties, and attach click events to each.
        /// 4. Add picture boxes to the TableLayoutPanel.
        /// 5. Add the TableLayoutPanel to the pop-up form.
        /// 6. Show the pop-up form to the player.
        /// </algo>
        private static void ShowRockPaperScissorsPopup()
        {
            // Create a form to host the pop-up
            RockPaperScisssorForm = new Form();
            RockPaperScisssorForm.Text = "Rock, Paper, Scissors";
            RockPaperScisssorForm.StartPosition = FormStartPosition.CenterScreen;
            RockPaperScisssorForm.AutoSize = false;
            RockPaperScisssorForm.ClientSize = new Size(330, 140);

            // Create a TableLayoutPanel to organize the images in a grid
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.BorderStyle = BorderStyle.FixedSingle; 

            // Create three picture boxes for Rock, Paper, and Scissors
            PictureBox picRock = new PictureBox();
            picRock.Image = Resources.Rock; 
            picRock.SizeMode = PictureBoxSizeMode.Zoom;
            picRock.Click += (sender, e) => CheckResult("Rock");
            picRock.BorderStyle = BorderStyle.FixedSingle;
            picRock.Dock = DockStyle.Fill;
            picRock.BackColor = backcolor;

            PictureBox picPaper = new PictureBox();
            picPaper.Image = Resources.Paper; 
            picPaper.SizeMode = PictureBoxSizeMode.Zoom;
            picPaper.Click += (sender, e) => CheckResult("Paper");
            picPaper.BorderStyle = BorderStyle.FixedSingle;
            picPaper.Dock = DockStyle.Fill;
            picPaper.BackColor = backcolor;

            PictureBox picScissor = new PictureBox();
            picScissor.Image = Resources.Scissor; 
            picScissor.SizeMode = PictureBoxSizeMode.Zoom;
            picScissor.Click += (sender, e) => CheckResult("Scissors");
            picScissor.BorderStyle = BorderStyle.FixedSingle;
            picScissor.Dock = DockStyle.Fill;
            picScissor.BackColor = backcolor;

            // Add picture boxes to the form
            tableLayoutPanel.Controls.Add(picRock);
            tableLayoutPanel.Controls.Add(picPaper);
            tableLayoutPanel.Controls.Add(picScissor);

            // Add TableLayoutPanel to the form
            RockPaperScisssorForm.Controls.Add(tableLayoutPanel);

            // Show the pop-up form
            RockPaperScisssorForm.ShowDialog();
        }

        /// <summary>
        /// Checks the result of the Rock, Paper, Scissors game.
        /// Compares the player's choice with the computer's randomly generated choice.
        /// Plays appropriate sound effects based on the result and displays a message box with the outcome.
        /// If the player wins or ties, they can continue playing Minesweeper; otherwise, the game exits.
        /// </summary>
        private static void CheckResult(string playerChoice)
        {
            SoundPlayer musicPlayer = new SoundPlayer(Resources.click);
            musicPlayer.Play();
            string[] choices = { "Rock", "Paper", "Scissors" };
            string computerChoice = choices[RNG.Next(choices.Length)];

            MessageBox.Show($"You chose: {playerChoice}\nThe computer chose: {computerChoice}", "Result");

            // Determine the winner
            if (playerChoice == computerChoice)
            {
                musicPlayer = new SoundPlayer(Resources.Win);
                musicPlayer.Play();
                MessageBox.Show("It's a tie! So you can continue playing Minesweeper!", "Result");
            }
            else if (
                (playerChoice == "Rock" && computerChoice == "Scissors") ||
                (playerChoice == "Paper" && computerChoice == "Rock") ||
                (playerChoice == "Scissors" && computerChoice == "Paper")
            )
            {
                musicPlayer = new SoundPlayer(Resources.Win);
                musicPlayer.Play();
                MessageBox.Show("You win! You can continue playing Minesweeper!", "Result");
            }
            else
            {
                musicPlayer = new SoundPlayer(Resources.lose_flowergarden_medium);
                musicPlayer.Play();
                MessageBox.Show("Computer wins! Better luck next time.", "Result");
                panel.Controls.Clear(); 
                rockPaperScissorsPlayed = false;
            }

            // Close the pop-up form after the result is shown
            RockPaperScisssorForm.Close();
        }

        /// <summary>
        /// Returns the image corresponding to a given number.
        /// Used to set the image of numbered cells in the Minesweeper grid.
        /// (Because apparently pictureboxes dont have a .Text.
        /// </summary>
        private static Image GetNumberImage(int number)
{
        // Use a switch case to return the appropriate number image
        switch (number)
        {
            case 1: return Resources.one;
            case 2: return Resources.two;
            case 3: return Resources.three;
            case 4: return Resources.four;
            case 5: return Resources.five;
            case 6: return Resources.six;
            case 7: return Resources.seven;
            case 8: return Resources.eight;
            default: return null;
        }
}
        /// <summary>
        /// Reveals an empty cell and its adjacent empty cells recursively in the Minesweeper grid.
        /// If the cell contains a number, reveals only the current cell.
        /// </summary>
        /// <algo>
        /// 1. Check if the cell is already revealed; if yes, stop recursion to avoid infinite loop.
        /// 2. Reveal the clicked empty cell by setting its visibility to true and changing its backcolor.
        /// 3. Disable further clicks on the revealed cell.
        /// 4. Mark the cell as revealed to avoid infinite recursion.
        /// 5. Iterate through the eight possible neighboring positions:
        ///    a. Calculate the new row and column for each neighboring cell.
        ///    b. If the neighboring cell is not revealed:
        ///       - If the adjacent cell has a number, reveal it and stop recursion.
        ///       - If the adjacent cell is empty, recursively reveal adjacent empty cells.
        ///          (Continue recursion with the RevealEmptyCell method.)
        /// 6. If the cell contains a number, reveal the number and stop.
        /// </algo>
        private static void RevealEmptyCell(int row, int column)
        {
            // If the cell is already revealed, stop the recursion
            if (grid[row, column].Revealed)
                return;

            // Reveal the clicked empty cell
            grid[row, column].Visible = true;

            // Color the cell with backcolor
            grid[row, column].BackColor = backcolor;

            // Disable further clicks on the revealed cell
            grid[row, column].MouseClick -= Cell_MouseClick;

            // Mark the cell as revealed to avoid infinite recursion
            grid[row, column].Revealed = true;


            for (int i = 0; i < 8; i++)
            {
                int newRow = row + neigboring_x[i];
                int newColumn = column + neigboring_y[i];

                if (newRow >= 0 && newRow < size && newColumn >= 0 && newColumn < size)
                {
                    if (!grid[newRow, newColumn].Revealed && !grid[newRow, newColumn].Bomb)
                    {
                        // Recursively reveal adjacent cells
                        if (grid[newRow, newColumn].Number == 0)
                        {
                            RevealEmptyCell(newRow, newColumn);
                        }
                        else
                        {
                            // If the adjacent cell has a number, reveal it
                            grid[newRow, newColumn].Image = GetNumberImage(grid[newRow, newColumn].Number);
                            grid[newRow, newColumn].Visible = true;
                            grid[newRow, newColumn].Revealed = true;
                            grid[newRow, newColumn].BackColor = backcolor;
                            grid[newRow, newColumn].MouseClick -= Cell_MouseClick;
                        }
                    }
                }
            }
        }
    }
}
