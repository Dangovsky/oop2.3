using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using oop2._3.Interfaces;
using oop2._3.Iterators;
using oop2._3.Unit;
using oop2._3.Exceptions;
using oop2._3.Drawing;

namespace oop2._3.Players
{
    class HumanPlayerAdapter: GameControlForm, IPlayer
    {
        private List<IObserver> observers;
        private bool isActive;
        private bool mouseDownOnPictureBox;
        private string color;
        private int[] chosenCell;
        private int pixelsForOneCell;
        private IFigure[,] board;
        private List<int[]> enemyPosibleMoves;
        private GameCommand command;
        private Drawer drawer;

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        public HumanPlayerAdapter(string color, Drawer drawer)
        {
            this.drawer = drawer;
            isActive = false;
            chosenCell = new int[] { -1, -1 };
            pixelsForOneCell = 60;
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(ICommand gameCommand)
        {
            IteratorForList<IObserver> iterator = new IteratorForList<IObserver>(observers);
            while (iterator.HasNext())
            {
                iterator.Next().HandleEvent(gameCommand);
            }
        }

        public GameCommand CalculateMove(IFigure[,] board)
        {           
            chosenCell = new int[] { -1, -1 };
            this.board = board;
            List<int[]> PosibleMoves;
            PosibleMovesCalculator(board, out PosibleMoves, out enemyPosibleMoves);
            int[] kingUnderCheck = Check(board, enemyPosibleMoves);
            if (kingUnderCheck[0] != -1)
            {
                foreach (int[] move in PosibleMoves)
                {
                    IFigure[,] boardAfterMove = MakeMove(board, move);
                    List<int[]> enemyPosibleMovesAfterMove;
                    PosibleMovesCalculator(boardAfterMove, out enemyPosibleMovesAfterMove);
                    if (Check(boardAfterMove, enemyPosibleMovesAfterMove)[0] != -1)
                    {
                        PosibleMoves.Remove(move);
                    }
                }
                if (PosibleMoves.Count == 0)
                {
                    return new GameCommand(this, new int[] { -1, -1 }, new int[] { -1, -1 }, board);
                }
            }
            isActive = true;
            while (isActive)
            {

            }
            return command;
        }

        internal void pictureBoxBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (isActive)
            {
                int[] IndexesOfClikedCell = ConvertCoordinatesToBoardIndexes(e.X, e.Y);
                if (chosenCell[0] != -1)
                {
                    List<int[]> posibleMovesForChosenCell = board[chosenCell[0], chosenCell[1]].PosibleMoves(chosenCell, board);
                    if (posibleMovesForChosenCell.Exists(delegate (int[] move) { return move[0] == IndexesOfClikedCell[0] && move[1] == IndexesOfClikedCell[1]; }))
                    {
                        int[] posibleMove = new int[] { IndexesOfClikedCell[0], IndexesOfClikedCell[1], board[IndexesOfClikedCell[0], IndexesOfClikedCell[1]].GetIdentifer() };
                        IFigure[,] boardAfterMove = MakeMove(board, posibleMove);
                        List<int[]> enemyPosibleMovesAfterMove;
                        PosibleMovesCalculator(boardAfterMove, out enemyPosibleMovesAfterMove);
                        if (Check(boardAfterMove, enemyPosibleMovesAfterMove)[0] != -1)
                        {
                            return;
                        }
                        command = new GameCommand(this, chosenCell, IndexesOfClikedCell, board);
                        isActive = false;
                        return;
                    }
                }
                if (board[IndexesOfClikedCell[0], IndexesOfClikedCell[1]] != null && board[IndexesOfClikedCell[0], IndexesOfClikedCell[1]].GetColor() == color)
                {
                    List<int[]> posibleMovesForThisCell = board[IndexesOfClikedCell[0], IndexesOfClikedCell[1]].PosibleMoves(IndexesOfClikedCell, board);
                    RemoveCheckMoves(ref posibleMovesForThisCell);
                    chosenCell = IndexesOfClikedCell;
                    drawer.HighLite(posibleMovesForThisCell);
                    return;
                }
                if (board[IndexesOfClikedCell[0], IndexesOfClikedCell[1]] == null)
                {
                    drawer.DeHighLite();
                }
            }
        }

        internal void pictureBoxBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (isActive)
            {
                mouseDownOnPictureBox = true;
                chosenCell = ConvertCoordinatesToBoardIndexes(e.X, e.Y);
            }
        }

        internal void pictureBoxBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (isActive && mouseDownOnPictureBox)
            {
                if (board[chosenCell[0], chosenCell[1]] != null && board[chosenCell[0], chosenCell[1]].GetColor() == color)
                {
                    List<int[]> posibleMovesForThisCell = board[chosenCell[0], chosenCell[1]].PosibleMoves(chosenCell, board);
                    RemoveCheckMoves(ref posibleMovesForThisCell);
                    drawer.HighLite(posibleMovesForThisCell);
                }
            }
        }

        internal void pictureBoxBoard_MouseUp(object sender, MouseEventArgs e)
        {
            if (isActive && mouseDownOnPictureBox)
            {
                mouseDownOnPictureBox = false;
                int[] IndexesOfClikedCell = ConvertCoordinatesToBoardIndexes(e.X, e.Y);
                List<int[]> posibleMovesForChosenCell = board[chosenCell[0], chosenCell[1]].PosibleMoves(chosenCell, board);
                if (posibleMovesForChosenCell.Exists(delegate (int[] move) { return move[0] == IndexesOfClikedCell[0] && move[1] == IndexesOfClikedCell[1]; }))
                {
                    int[] posibleMove = new int[] { IndexesOfClikedCell[0], IndexesOfClikedCell[1], board[IndexesOfClikedCell[0], IndexesOfClikedCell[1]].GetIdentifer() };
                    IFigure[,] boardAfterMove = MakeMove(board, posibleMove);
                    List<int[]> enemyPosibleMovesAfterMove;
                    PosibleMovesCalculator(boardAfterMove, out enemyPosibleMovesAfterMove);
                    if (Check(boardAfterMove, enemyPosibleMovesAfterMove)[0] != -1)
                    {
                        drawer.DeHighLite();
                        return;
                    }
                    command = new GameCommand(this, chosenCell, IndexesOfClikedCell, board);
                    drawer.DeHighLite();
                    isActive = false;
                    return;
                }
                drawer.DeHighLite();
            }
        }
        

        private void RemoveCheckMoves(ref List<int[]> Moves)
        {
            foreach (int[] move in Moves)
            {
                IFigure[,] boardAfterMove = MakeMove(board, move);
                List<int[]> enemyPosibleMovesAfterMove;
                PosibleMovesCalculator(boardAfterMove, out enemyPosibleMovesAfterMove);
                if (Check(boardAfterMove, enemyPosibleMovesAfterMove)[0] != -1)
                {
                    Moves.Remove(move);
                }
            }
        }

        private void PosibleMovesCalculator(IFigure[,] board, out List<int[]> posibleMoves, out List<int[]> enemyPosibleMoves)
        {
            posibleMoves = new List<int[]>();
            enemyPosibleMoves = new List<int[]>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j <8; j++)
                {
                    if (board[i, j] == null)
                    {
                        continue;
                    }
                    if (board[i, j].GetColor() == color)
                    {
                        posibleMoves.AddRange(board[i, j].PosibleMoves(new int[] { i, j }, board));
                        continue;
                    }
                    enemyPosibleMoves.AddRange(board[i, j].PosibleMoves(new int[] { i, j }, board));
                }
            }
        }

        private void PosibleMovesCalculator(IFigure[,] board, out List<int[]> enemyPosibleMoves)
        {
            enemyPosibleMoves = new List<int[]>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == null)
                    {
                        continue;
                    }
                    if (board[i, j].GetColor() != color)
                    {
                        enemyPosibleMoves.AddRange(board[i, j].PosibleMoves(new int[] { i, j }, board));
                    }
                }
            }
        }

        private int[] Check(IFigure[,] board, List<int[]> enemyPosibleMoves)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((board[i, j] != null) && (board[i, j].ToString() == "King") && (board[i, j].GetColor() == color))
                    {
                        if (enemyPosibleMoves.Contains(new int[] { i, j }))
                        {
                            return new int[] { i, j };
                        }
                    }
                }
            }
            return new int[] { -1, -1 };
        }
        
        private IFigure[,] MakeMove(IFigure[,] board, int[] move)
        {
            IFigure[,] boardAfterMove = board;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].GetIdentifer() == move[2])
                    {
                        board[move[0], move[1]] = board[i, j];
                        board[i, j] = null;
                        return board;
                    }
                }
            }
            throw new ChessException("HumanPlayerAdapter.MakeMove exception.");
        }

        private int[] ConvertCoordinatesToBoardIndexes(int x, int y)
        {
            return new int[] { x / (pixelsForOneCell + 1), y / (pixelsForOneCell + 1) }; // чтобы избежать ошибки 480/60 = 8, выход за границы массива
        }
    }
}
