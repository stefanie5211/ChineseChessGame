using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ChineseChess
{
    public class ChessPiece
    {
        private GameMainWindow gameMainWindow;
        private Dictionary<string, Button> redPieceButtons;
        private Dictionary<string, Button> blackPieceButtons;

        public ChessPiece(GameMainWindow gameMainWindow)
        {
            redPieceButtons = new Dictionary<string, Button>();
            blackPieceButtons = new Dictionary<string, Button>();
            this.gameMainWindow = gameMainWindow;
        }

        public Dictionary<string, Button> RedPieceButtons
        {
            get { return redPieceButtons; }
        }

        public Dictionary<string, Button> BlackPieceButtons
        {
            get { return blackPieceButtons; }
        }

        public void InitializeRedPieces()
        {
            redPieceButtons.Clear();

            redPieceButtons.Add("redRookButtonLeft", gameMainWindow.redRookButtonLeft);
            redPieceButtons.Add("redKnightButtonLeft", gameMainWindow.redKnightButtonLeft);
            redPieceButtons.Add("redBishopButtonLeft", gameMainWindow.redBishopButtonLeft);
            redPieceButtons.Add("redGuardButtonLeft", gameMainWindow.redGuardButtonLeft);

            redPieceButtons.Add("redKingButton", gameMainWindow.redKingButton);

            redPieceButtons.Add("redRookButtonRight", gameMainWindow.redRookButtonRight);
            redPieceButtons.Add("redKnightButtonRight", gameMainWindow.redKnightButtonRight);
            redPieceButtons.Add("redBishopButtonRight", gameMainWindow.redBishopButtonRight);
            redPieceButtons.Add("redGuardButtonRight", gameMainWindow.redGuardButtonRight);

            redPieceButtons.Add("redCannonButtonLeft", gameMainWindow.redCannonButtonLeft);
            redPieceButtons.Add("redCannonButtonRight", gameMainWindow.redCannonButtonRight);

            redPieceButtons.Add("redPawnButton1", gameMainWindow.redPawnButton1);
            redPieceButtons.Add("redPawnButton2", gameMainWindow.redPawnButton2);
            redPieceButtons.Add("redPawnButton3", gameMainWindow.redPawnButton3);
            redPieceButtons.Add("redPawnButton4", gameMainWindow.redPawnButton4);
            redPieceButtons.Add("redPawnButton5", gameMainWindow.redPawnButton5);
        }

        public void InitializeBlackPieces()
        {
            blackPieceButtons.Clear();

            blackPieceButtons.Add("blackRookButtonLeft", gameMainWindow.blackRookButtonLeft);
            blackPieceButtons.Add("blackKnightButtonLeft", gameMainWindow.blackKnightButtonLeft);
            blackPieceButtons.Add("blackBishopButtonLeft", gameMainWindow.blackBishopButtonLeft);
            blackPieceButtons.Add("blackGuardButtonLeft", gameMainWindow.blackGuardButtonLeft);

            blackPieceButtons.Add("blackKingButton", gameMainWindow.blackKingButton);

            blackPieceButtons.Add("blackRookButtonRight", gameMainWindow.blackRookButtonRight);
            blackPieceButtons.Add("blackKnightButtonRight", gameMainWindow.blackKnightButtonRight);
            blackPieceButtons.Add("blackBishopButtonRight", gameMainWindow.blackBishopButtonRight);
            blackPieceButtons.Add("blackGuardButtonRight", gameMainWindow.blackGuardButtonRight);

            blackPieceButtons.Add("blackCannonButtonLeft", gameMainWindow.blackCannonButtonLeft);
            blackPieceButtons.Add("blackCannonButtonRight", gameMainWindow.blackCannonButtonRight);

            blackPieceButtons.Add("blackPawnButton1", gameMainWindow.blackPawnButton1);
            blackPieceButtons.Add("blackPawnButton2", gameMainWindow.blackPawnButton2);
            blackPieceButtons.Add("blackPawnButton3", gameMainWindow.blackPawnButton3);
            blackPieceButtons.Add("blackPawnButton4", gameMainWindow.blackPawnButton4);
            blackPieceButtons.Add("blackPawnButton5", gameMainWindow.blackPawnButton5);
        }
    }
}
