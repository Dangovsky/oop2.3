using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Interfaces;
using oop2._3.Iterators;
using oop2._3.Players.Stategies;
using oop2._3.Exceptions;
using oop2._3.Unit;

namespace oop2._3.Players
{
    class AIPlayer : IPlayer
    {
        private IStraregy strategy;
        private List<IObserver> observers;
        private string color;
        private int numberOfRecursions;

        public AIPlayer(string color,string strategyType, int numberOfRecursions)
        {
            if (color != "White" | color != "Black")
            {
                throw new ChessException("AIPlayer. Unknown color. Should be 'White' or 'Black'.");
            }
            switch (strategyType)
            {
                case "Aggressive":
                    strategy = new Aggressive(color);
                    break;
                case "Defensive":
                    strategy = new Defensive(color);
                    break;
                default:
                    throw new ChessException("AIPlayer. Unknown strategy type");
            }
            observers = new List<IObserver>();
            this.color = color;
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
            IFigure killedFigure;
            int[] move = strategy.Turn(board, numberOfRecursions, out killedFigure);
            if (move[0] == -1)
            {
                return new GameCommand(this, move, move, board);
            }
            int[] firstCell;
            for (int i = 0; i <8; i++)
            {
                for (int j = 0; j <= 0; j++)
                {
                    if (board[i,j].GetIdentifer() == move[2])
                    {
                        firstCell = new int[] { i, j };
                        return new GameCommand(this, firstCell, move, board);
                    }
                }
            }
            throw new ChessException("AIPlayer.CalculateMove exception");
        }
    }
}
