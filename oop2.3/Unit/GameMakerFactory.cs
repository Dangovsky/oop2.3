using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using oop2._3.Interfaces;
using oop2._3.Figures;
using oop2._3.Players;
using oop2._3.Exceptions;

namespace oop2._3.Unit
{
    class GameMakerFactory
    {
        public Game CreateNewGame(string whitePlayerStrategy, string blackPlayerStrategy, int numberOfRecursions, PictureBox pictureBoxBoard)
        {
            Game game = new Game();
            game.turnCounter = 0;
            game.whiteTurn = true;
            game.drawer = new Drawing.Drawer(pictureBoxBoard);
            if (whitePlayerStrategy == "Aggressive" || whitePlayerStrategy == "Defenive")
            {
                game.whitePlayer = new AIPlayer("White", whitePlayerStrategy, numberOfRecursions);
            }
            if (whitePlayerStrategy == "Human")
            {
                game.whitePlayer = new HumanPlayerAdapter("White", game.drawer);
            }
            if (blackPlayerStrategy == "Aggressive" || blackPlayerStrategy == "Defenive")
            {
                game.blackPlayer = new AIPlayer("White", blackPlayerStrategy, numberOfRecursions);
            }
            if (blackPlayerStrategy == "Human")
            {
                game.blackPlayer = new HumanPlayerAdapter("Black", game.drawer);
            }
            if (game.blackPlayer == null || game.whitePlayer == null)
            {
                throw new ChessException("GameMakerFactory.CreateNewGame exception");
            }
            game.counterSingleton = new CounterSingleton();
            game.board = new IFigure[8, 8] { { new Rook("White",game.counterSingleton.GetIdentifer()), new Knight("White",game.counterSingleton.GetIdentifer()), new Bishop("White",game.counterSingleton.GetIdentifer()), new Queen("White",game.counterSingleton.GetIdentifer()), new King("White",game.counterSingleton.GetIdentifer()), new Bishop("White",game.counterSingleton.GetIdentifer()), new Knight("White",game.counterSingleton.GetIdentifer()), new Rook("White",game.counterSingleton.GetIdentifer())},
                                             { new Pawn("White",game.counterSingleton.GetIdentifer()), new Pawn("White",game.counterSingleton.GetIdentifer()), new Pawn("White",game.counterSingleton.GetIdentifer()), new Pawn("White",game.counterSingleton.GetIdentifer()), new Pawn("White",game.counterSingleton.GetIdentifer()), new Pawn("White",game.counterSingleton.GetIdentifer()), new Pawn("White",game.counterSingleton.GetIdentifer()), new Pawn("White",game.counterSingleton.GetIdentifer())},
                                             { null,null,null,null,null,null,null,null },
                                             { null,null,null,null,null,null,null,null },
                                             { null,null,null,null,null,null,null,null },
                                             { null,null,null,null,null,null,null,null },
                                             { new Pawn("Black",game.counterSingleton.GetIdentifer()), new Pawn("Black",game.counterSingleton.GetIdentifer()), new Pawn("Black",game.counterSingleton.GetIdentifer()), new Pawn("Black",game.counterSingleton.GetIdentifer()), new Pawn("Black",game.counterSingleton.GetIdentifer()), new Pawn("Black",game.counterSingleton.GetIdentifer()), new Pawn("Black",game.counterSingleton.GetIdentifer()), new Pawn("Black",game.counterSingleton.GetIdentifer())},
                                             { new Rook("Black",game.counterSingleton.GetIdentifer()), new Knight("Black",game.counterSingleton.GetIdentifer()), new Bishop("Black",game.counterSingleton.GetIdentifer()), new Queen("Black",game.counterSingleton.GetIdentifer()), new King("Black",game.counterSingleton.GetIdentifer()), new Bishop("Black",game.counterSingleton.GetIdentifer()), new Knight("Black",game.counterSingleton.GetIdentifer()), new Rook("Black",game.counterSingleton.GetIdentifer())}};
            game.deadFigures = new List<IFigure>();
            game.commandHistory = new Queue<ICommand>();
            game.mementoHistory = new Queue<GameMemento>();          
            return game; 
        }

        public Game LoadGame(string path)
        {
            
        }
    }
}
