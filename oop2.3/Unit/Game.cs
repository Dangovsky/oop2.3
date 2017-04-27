using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Figures;
using oop2._3.Interfaces;
using oop2._3.Drawing;

namespace oop2._3.Unit
{
    class Game
    {
        private int turnCounter;
        private bool whiteTurn;
        private IPlayer whitePlayer;
        private IPlayer blackPlayer;
        private GameField gameField;
        private List<IFigure> deadFigures;
        private Queue<ICommand> comandHistory;
        private Queue<GameMemento> mementoHistory;
        private Drawer drawer;

        private GameMemento SetMemento()
        {
            GameMemento memento = new GameMemento(this);
            mementoHistory.Enqueue(memento);
            return memento;
        }

        public void LoadFromMemento()
        {
            
        }
    }
}
