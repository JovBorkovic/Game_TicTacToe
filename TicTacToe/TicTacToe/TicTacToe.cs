using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1Jovanborkovic
{
    public partial class TicTacToe : Form
    {

        public TicTacToe()
        {
            InitializeComponent();
            if (player == 0) {
                Random number = new Random();
                player = number.Next(0, 1);
            }
        }

        private string[,] game = new string[3, 3];
        private int gamesWonX = 0;
        private int gamesWonO = 0;
        private int scratchGames = 0;
        private int winningPlayer;

        //This decides the player
        //0 is for O
        //1 is for X
        private int player;
        //current player playing string
        string playing = "";


        //create a string for the player currently playing
        private string CreateString(int player)
        {
            string person = "";

            if(player == 1)
            {
                TSStatus.Text = "Player X is playing";
                lblStatus.Text = TSStatus.Text;
                person = "X";

            }
            else if (player == 0)
            {
                TSStatus.Text = "Player O is playing";
                lblStatus.Text = TSStatus.Text;
                person = "O";
            }
            else if( player == 2)// when the game is finished Player = 2;
            {
                TSStatus.Text = "The game is over, to play another game select the 'Clear' button in the bottom right of the window";
            }


            return person;
        }

        //Switching players && Checking for a winner before players switch
        private int switchTurns(int player)
        {
            if(CheckWinner(game) == true)
            {
                foreach(Control c in Controls)
                {
                    if (c is Button)
                    {
                        if(c.Name != "btnClear" && c.Name != "btnClose")
                        {
                            c.Enabled = false;
                        }
                    }
                }
                winningPlayer = player;
                return player = 2;
            }

            if(player == 1)
            {
                player = 0;
            }
            else if(player == 0)
            {
                player = 1;
            }

            return player;
        }

        //Check the winner and or scratch games
        private bool CheckWinner(string[,] arr)
        {
            bool isWinner;
            if (arr[0, 0] == "X" && arr[0, 1] == "X" && arr[0, 2] == "X" ||
                arr[0, 0] == "X" && arr[1, 1] == "X" && arr[2, 2] == "X" ||
                arr[1, 1] == "X" && arr[2, 0] == "X" && arr[0, 2] == "X" ||
                arr[1, 0] == "X" && arr[1, 1] == "X" && arr[1, 2] == "X" ||
                arr[2, 0] == "X" && arr[2, 1] == "X" && arr[2, 2] == "X" ||
                arr[0, 0] == "X" && arr[1, 0] == "X" && arr[2, 0] == "X" ||
                arr[0, 1] == "X" && arr[1, 1] == "X" && arr[2, 1] == "X" ||
                arr[0, 2] == "X" && arr[1, 2] == "X" && arr[2, 2] == "X")
            { 
                MessageBox.Show("Winner is X", "You WON!", MessageBoxButtons.OK);
                isWinner = true;
                gamesWonX += 1;
                lblWinsX.Text = gamesWonX.ToString();
                return isWinner;
            }

            if (arr[0, 0] == "O" && arr[0, 1] == "O" && arr[0, 2] == "O" ||
                arr[0, 0] == "O" && arr[1, 1] == "O" && arr[2, 2] == "O" ||
                arr[1, 1] == "O" && arr[2, 0] == "O" && arr[0, 2] == "O" ||
                arr[1, 0] == "O" && arr[1, 1] == "O" && arr[1, 2] == "O" ||
                arr[2, 0] == "O" && arr[2, 1] == "O" && arr[2, 2] == "O" ||
                arr[0, 0] == "O" && arr[1, 0] == "O" && arr[2, 0] == "O" ||
                arr[0, 1] == "O" && arr[1, 1] == "O" && arr[2, 1] == "O" ||
                arr[0, 2] == "O" && arr[1, 2] == "O" && arr[2, 2] == "O")
            {
                MessageBox.Show("Winner is O", "You WON!", MessageBoxButtons.OK);
                isWinner = true;
                gamesWonO += 1;
                lblWinsO.Text = gamesWonO.ToString();
                return isWinner;
            }

            Button[] buttons = this.Controls.OfType<Button>().ToArray();


            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < buttons.Length; j++) {
                    if (buttons[j] != btnClear && buttons[j] != btnClose)
                    {
                        if (buttons[j].Enabled)
                        {
                            isWinner = false;
                            return isWinner;
                        }
                    }
                }

                MessageBox.Show("No one is a winner, shame on you", "Scratch Game", MessageBoxButtons.OK);
                isWinner = true;
                scratchGames += 1;
                lblScratch.Text = scratchGames.ToString();
                return isWinner;

            }

            return isWinner = false;
        }

        //resetting all the buttons
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                if( c is Button)
                {
                    if (c.Name != "btnClear" && c.Name != "btnClose") {
                        c.Text = "?";
                        c.Enabled = Enabled;
                        Array.Clear(game, 0, 9);
                    }
                }
            }

            if(player == 2)
            {
                if(winningPlayer == 0)
                {
                    player = 1;
                }
                else if(winningPlayer == 1)
                {
                    player = 0;
                }

                CreateString(player);
            }
        }

        //click event of top left button
        private void btnTL_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);
            btnTL.Text = playing;
            btnTL.Enabled = false;
            game[0, 0] = playing;
            player = switchTurns(player);
            CreateString(player);
        }

        //click event for top middle button
        private void btnTM_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);
            btnTM.Text = playing;
            btnTM.Enabled = false;
            game[0, 1] = playing;
            player = switchTurns(player);
            CreateString(player);
        }


        //click event for top right button
        private void btnTR_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);
            btnTR.Text = playing;
            btnTR.Enabled = false;
            game[0, 2] = playing;
            player = switchTurns(player);
            CreateString(player);

        }   
        
        //click event for middle left button
        private void btnML_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);
            btnML.Text = playing;
            btnML.Enabled = false;
            game[1, 0] = playing;
            player = switchTurns(player);
            CreateString(player);

        }

        //click event for middle button
        private void btnM_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);
            btnM.Text = playing;
            btnM.Enabled = false;
            game[1, 1] = playing;
            player = switchTurns(player);
            CreateString(player);
        }

        //click event for middle right button
        private void btnMR_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);

            btnMR.Text = playing;
            btnMR.Enabled = false;
            game[1,2] = playing;
            player = switchTurns(player);
            CreateString(player);

        }

        //click event for bottom right button
        private void btnBR_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);

            btnBR.Text = playing;
            btnBR.Enabled = false;
            game[2,2] = playing;
            player = switchTurns(player);
            CreateString(player);

        }

        //click event for bottom middle button
        private void btnBM_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);

            btnBM.Text = playing;
            btnBM.Enabled = false;
            game[2, 1] = playing;
            player = switchTurns(player);
            CreateString(player);

        }

        //click event for bottom left button
        private void btnBL_Click(object sender, EventArgs e)
        {
            playing = CreateString(player);
            btnBL.Text = playing;
            btnBL.Enabled = false;
            game[2, 0] = playing;
            player = switchTurns(player);
            CreateString(player);

        }



        //Closing event for TictacToeGame
        private void TicTacToe_FormClosed(object sender, FormClosedEventArgs e)
        {
            string mess = "Amount of wins from X: " + gamesWonX + "\n" + "Games won by O: " + gamesWonO + "\n" + "Amount of scratched games: " + scratchGames;

            DialogResult result = MessageBox.Show(mess, "Message", MessageBoxButtons.OK);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Would you like to close the game?", "End Game", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
