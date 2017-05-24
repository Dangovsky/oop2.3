using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Figures;
using oop2._3.Interfaces;
using oop2._3.Drawing;
using oop2._3.Exceptions;

namespace oop2._3.Unit
{
    public class Game
    {
        internal int turnCounter;
        internal bool isGoing;
        internal bool whiteTurn;
        internal IPlayer whitePlayer;
        internal IPlayer blackPlayer;
        internal IFigure[,] board;
        internal List<IFigure> deadFigures;
        internal Queue<ICommand> commandHistory;
        internal Queue<GameMemento> mementoHistory;
        internal Drawer drawer;
        internal CounterSingleton counterSingleton;

        public Game()
        {

        }

        private GameMemento SaveToMemento()
        {
            mementoHistory.Enqueue(new GameMemento(this));
            return mementoHistory.Peek();
        }

        private void LoadFromMemento()
        {
            GameMemento memento = mementoHistory.Dequeue();
            turnCounter = memento.state.turnCounter;
            whiteTurn = memento.state.whiteTurn;
            board = memento.state.board;
            deadFigures = memento.state.deadFigures;
        }

        private void NextMove()
        {
            if (whiteTurn)
            {
                commandHistory.Enqueue(whitePlayer.CalculateMove(board));
            }
            else
            {
                commandHistory.Enqueue(blackPlayer.CalculateMove(board));
            }
            IFigure killedFigure;
            SaveToMemento();
            try
            {
                board = commandHistory.Peek().Execute(ref whiteTurn, out killedFigure);
            }
            catch (WinException we)
            {
                Stop();
                throw we;
            }
            deadFigures.Add(killedFigure);
            whiteTurn = !whiteTurn;
        }

        public void CancelMove()
        {
            Stop();
            LoadFromMemento();
            commandHistory.Dequeue();
            Start();
        }

        public void Start()
        {            
            while(isGoing)
            {
                NextMove();
                turnCounter++;
            }
        }

        public void Stop()
        {
            isGoing = false;
        }

        public void Save(string path)
        {

        }


    }
}
