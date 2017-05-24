using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using oop2._3.Interfaces;
using oop2._3.Unit;

namespace oop2._3.Drawing
{
    class Drawer : IObserver
    {
        static private PictureBox pictureBox;
        private Image clearBackgroung;
        private int pixelsForOneCell;
        private Queue<DrawCommand> drawCommands;

        public Drawer(PictureBox pictureBoxBoard)
        {
            pictureBox = pictureBoxBoard;
            drawCommands = new Queue<DrawCommand>();
            pixelsForOneCell = 60;            
        }

        public void HandleEvent(ICommand command)
        {
            drawCommands.Enqueue(new DrawCommand("HandleEvent", command, this));
        }

        internal void DoHandleEvent(GameCommand command)
        {         
            Bitmap bitmap = new Bitmap(pictureBox.Image);
            for (int i = 0; i < pixelsForOneCell; i++)
            {
                for (int j = 0; i < pixelsForOneCell; i++)
                {
                    bitmap.SetPixel(i + pixelsForOneCell * command.firstCell[0], j + pixelsForOneCell * command.firstCell[1], Color.Empty);
                    bitmap.SetPixel(i + pixelsForOneCell * command.secondCell[0], j + pixelsForOneCell * command.secondCell[1], Color.Empty);
                }
            }
            pictureBox.Image = bitmap;
            bitmap.Dispose();
            Graphics g = pictureBox.CreateGraphics();
            g.DrawImageUnscaled(command.board[command.firstCell[0], command.firstCell[1]].GetImage(), command.secondCell[0] * pixelsForOneCell, command.secondCell[1] * pixelsForOneCell);
            g.Dispose();
        }

        public void DrawBoard(IFigure[,] board)
        {
            drawCommands.Enqueue(new DrawCommand("DrawBoard", board, this));
        }

        internal void DoDrawBoard(IFigure[,] board)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.Clear(Color.White);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i + j / 2 == 0)
                    {
                        g.FillRectangle(Brushes.LightGray, i * pixelsForOneCell, j * pixelsForOneCell, pixelsForOneCell, pixelsForOneCell);
                    }
                }
            }            
            pictureBox.BackgroundImage = pictureBox.Image;
            clearBackgroung = pictureBox.BackgroundImage;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i,j]!=null)
                    {
                        g.DrawImage(board[i, j].GetImage(), i * pixelsForOneCell, j * pixelsForOneCell);
                    }
                }
            }
            g.Dispose();
        }

        public void Draw()
        {
            while (drawCommands.Count != 0)
            {
                drawCommands.Dequeue().Execute();
            }
        }

        public void HighLite(List<int[]> CellsCoordinates)
        {
            drawCommands.Enqueue(new DrawCommand("HighLite", CellsCoordinates, this));
        }

        internal void DoHighLite(List<int[]> CellsCoordinates)
        {
            Bitmap bitmap = new Bitmap(pictureBox.BackgroundImage);
            foreach (int[] cell in CellsCoordinates)
            {
                for (int i = 0; i < pixelsForOneCell; i++)
                {
                    for (int j = 0; j < pixelsForOneCell; j++)
                    {
                        bitmap.SetPixel(i + pixelsForOneCell * cell[0], j + pixelsForOneCell * cell[1], Color.Aquamarine);
                    }
                }                
            }
            pictureBox.BackgroundImage = bitmap;
            bitmap.Dispose();
        }

        public void DeHighLite()
        {
            drawCommands.Enqueue(new DrawCommand("DeHighLite",null, this));
        }

        internal void DoDeHighLite()
        {
            pictureBox.BackgroundImage = clearBackgroung;
        }
    }
}
