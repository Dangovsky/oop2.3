using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using oop2._3.Unit;
using oop2._3.Players;

namespace oop2._3
{
    public partial class GameControlForm : Form
    {
        private Game game;
        private GameMakerFactory factory;

        public GameControlForm()
        {
            InitializeComponent();
            game = factory.CreateNewGame("Human", "Aggressive", 1, pictureBoxBoard);
            //game = factory.CreateNewGame("Human", "Defensive", 1, pictureBoxBoard);      
            if (game.whitePlayer.ToString() == "Human")
            {
                game.whitePlayer = (HumanPlayerAdapter)game.whitePlayer;
            }
            if (game.blackPlayer.ToString() == "Human")
            {
                game.blackPlayer = (HumanPlayerAdapter)game.blackPlayer;
            }
            game.Start();
        }

        private void GameControlForm_Load(object sender, EventArgs e)
        {
            game.drawer.DrawBoard(game.board);
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = factory.CreateNewGame("Human", "Aggressive", 1, pictureBoxBoard);
            //game = factory.CreateNewGame("Human", "Defensive", 1, pictureBoxBoard);
            if (game.whitePlayer.ToString() == "Human")
            {
                game.whitePlayer = (HumanPlayerAdapter)game.whitePlayer;
            }
            if (game.blackPlayer.ToString() == "Human")
            {
                game.blackPlayer = (HumanPlayerAdapter)game.blackPlayer;
            }
            game.Start();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (game.whitePlayer.ToString() == "Human")
            {
                game.whitePlayer.pictureBoxBoard_MouseDown(sender, e);
            }
            if (game.blackPlayer.ToString() == "Human")
            {
                game.blackPlayer.pictureBoxBoard_MouseDown(sender, e);
            }
        }

        private void pictureBoxBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.whitePlayer.ToString() == "Human")
            {
                game.whitePlayer.pictureBoxBoard_MouseMove(sender, e);
            }
            if (game.blackPlayer.ToString() == "Human")
            {
                game.blackPlayer.pictureBoxBoard_MouseMove(sender, e);
            }
        }

        private void pictureBoxBoard_MouseUp(object sender, MouseEventArgs e)
        {
            if (game.whitePlayer.ToString() == "Human")
            {
                game.whitePlayer.pictureBoxBoard_MouseUp(sender, e);
            }
            if (game.blackPlayer.ToString() == "Human")
            {
                game.blackPlayer.pictureBoxBoard_MouseUp(sender, e);
            }
        }

        private void pictureBoxBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (game.whitePlayer.ToString() == "Human")
            {
                game.whitePlayer.pictureBoxBoard_MouseClick(sender, e);
            }
            if (game.blackPlayer.ToString() == "Human")
            {
                game.blackPlayer.pictureBoxBoard_MouseClick(sender, e);
            }
        }

        private void GameControlForm_Paint(object sender, PaintEventArgs e)
        {
            game.drawer.Draw();
        }

        private void previousMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.CancelMove();
        }
    }
}

