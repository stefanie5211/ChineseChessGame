using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ChineseChess
{
    public class ChessBoard
    {
        private Dictionary<Button, Grid> buttonToGrid;
        private GameMainWindow gameMainWindow;
        private ChessPiece chessPiece;
        private Grid[,] grids;

        public ChessBoard(GameMainWindow gameMainWindow, ChessPiece chessPiece)
        {
            this.gameMainWindow = gameMainWindow;
            this.chessPiece = chessPiece;

            buttonToGrid = new Dictionary<Button, Grid>();
            grids = new Grid[11,10];
        }

        public Dictionary<Button, Grid> ButtonToGrid
        {
            get { return buttonToGrid; }
        }

        public Grid[,] GridS
        {
            get { return grids; }
        }

        public void InitializeGrids()
        {
            grids[1, 1] = gameMainWindow.wrapPanelGrid1_1;
            grids[1, 2] = gameMainWindow.wrapPanelGrid1_2;
            grids[1, 3] = gameMainWindow.wrapPanelGrid1_3;
            grids[1, 4] = gameMainWindow.wrapPanelGrid1_4;
            grids[1, 5] = gameMainWindow.wrapPanelGrid1_5;
            grids[1, 6] = gameMainWindow.wrapPanelGrid1_6;
            grids[1, 7] = gameMainWindow.wrapPanelGrid1_7;
            grids[1, 8] = gameMainWindow.wrapPanelGrid1_8;
            grids[1, 9] = gameMainWindow.wrapPanelGrid1_9;

            grids[2, 1] = gameMainWindow.wrapPanelGrid2_1;
            grids[2, 2] = gameMainWindow.wrapPanelGrid2_2;
            grids[2, 3] = gameMainWindow.wrapPanelGrid2_3;
            grids[2, 4] = gameMainWindow.wrapPanelGrid2_4;
            grids[2, 5] = gameMainWindow.wrapPanelGrid2_5;
            grids[2, 6] = gameMainWindow.wrapPanelGrid2_6;
            grids[2, 7] = gameMainWindow.wrapPanelGrid2_7;
            grids[2, 8] = gameMainWindow.wrapPanelGrid2_8;
            grids[2, 9] = gameMainWindow.wrapPanelGrid2_9;

            grids[3, 1] = gameMainWindow.wrapPanelGrid3_1;
            grids[3, 2] = gameMainWindow.wrapPanelGrid3_2;
            grids[3, 3] = gameMainWindow.wrapPanelGrid3_3;
            grids[3, 4] = gameMainWindow.wrapPanelGrid3_4;
            grids[3, 5] = gameMainWindow.wrapPanelGrid3_5;
            grids[3, 6] = gameMainWindow.wrapPanelGrid3_6;
            grids[3, 7] = gameMainWindow.wrapPanelGrid3_7;
            grids[3, 8] = gameMainWindow.wrapPanelGrid3_8;
            grids[3, 9] = gameMainWindow.wrapPanelGrid3_9;

            grids[4, 1] = gameMainWindow.wrapPanelGrid4_1;
            grids[4, 2] = gameMainWindow.wrapPanelGrid4_2;
            grids[4, 3] = gameMainWindow.wrapPanelGrid4_3;
            grids[4, 4] = gameMainWindow.wrapPanelGrid4_4;
            grids[4, 5] = gameMainWindow.wrapPanelGrid4_5;
            grids[4, 6] = gameMainWindow.wrapPanelGrid4_6;
            grids[4, 7] = gameMainWindow.wrapPanelGrid4_7;
            grids[4, 8] = gameMainWindow.wrapPanelGrid4_8;
            grids[4, 9] = gameMainWindow.wrapPanelGrid4_9;

            grids[5, 1] = gameMainWindow.wrapPanelGrid5_1;
            grids[5, 2] = gameMainWindow.wrapPanelGrid5_2;
            grids[5, 3] = gameMainWindow.wrapPanelGrid5_3;
            grids[5, 4] = gameMainWindow.wrapPanelGrid5_4;
            grids[5, 5] = gameMainWindow.wrapPanelGrid5_5;
            grids[5, 6] = gameMainWindow.wrapPanelGrid5_6;
            grids[5, 7] = gameMainWindow.wrapPanelGrid5_7;
            grids[5, 8] = gameMainWindow.wrapPanelGrid5_8;
            grids[5, 9] = gameMainWindow.wrapPanelGrid5_9;

            grids[6, 1] = gameMainWindow.wrapPanelGrid6_1;
            grids[6, 2] = gameMainWindow.wrapPanelGrid6_2;
            grids[6, 3] = gameMainWindow.wrapPanelGrid6_3;
            grids[6, 4] = gameMainWindow.wrapPanelGrid6_4;
            grids[6, 5] = gameMainWindow.wrapPanelGrid6_5;
            grids[6, 6] = gameMainWindow.wrapPanelGrid6_6;
            grids[6, 7] = gameMainWindow.wrapPanelGrid6_7;
            grids[6, 8] = gameMainWindow.wrapPanelGrid6_8;
            grids[6, 9] = gameMainWindow.wrapPanelGrid6_9;

            grids[7, 1] = gameMainWindow.wrapPanelGrid7_1;
            grids[7, 2] = gameMainWindow.wrapPanelGrid7_2;
            grids[7, 3] = gameMainWindow.wrapPanelGrid7_3;
            grids[7, 4] = gameMainWindow.wrapPanelGrid7_4;
            grids[7, 5] = gameMainWindow.wrapPanelGrid7_5;
            grids[7, 6] = gameMainWindow.wrapPanelGrid7_6;
            grids[7, 7] = gameMainWindow.wrapPanelGrid7_7;
            grids[7, 8] = gameMainWindow.wrapPanelGrid7_8;
            grids[7, 9] = gameMainWindow.wrapPanelGrid7_9;

            grids[8, 1] = gameMainWindow.wrapPanelGrid8_1;
            grids[8, 2] = gameMainWindow.wrapPanelGrid8_2;
            grids[8, 3] = gameMainWindow.wrapPanelGrid8_3;
            grids[8, 4] = gameMainWindow.wrapPanelGrid8_4;
            grids[8, 5] = gameMainWindow.wrapPanelGrid8_5;
            grids[8, 6] = gameMainWindow.wrapPanelGrid8_6;
            grids[8, 7] = gameMainWindow.wrapPanelGrid8_7;
            grids[8, 8] = gameMainWindow.wrapPanelGrid8_8;
            grids[8, 9] = gameMainWindow.wrapPanelGrid8_9;

            grids[9, 1] = gameMainWindow.wrapPanelGrid9_1;
            grids[9, 2] = gameMainWindow.wrapPanelGrid9_2;
            grids[9, 3] = gameMainWindow.wrapPanelGrid9_3;
            grids[9, 4] = gameMainWindow.wrapPanelGrid9_4;
            grids[9, 5] = gameMainWindow.wrapPanelGrid9_5;
            grids[9, 6] = gameMainWindow.wrapPanelGrid9_6;
            grids[9, 7] = gameMainWindow.wrapPanelGrid9_7;
            grids[9, 8] = gameMainWindow.wrapPanelGrid9_8;
            grids[9, 9] = gameMainWindow.wrapPanelGrid9_9;

            grids[10, 1] = gameMainWindow.wrapPanelGrid10_1;
            grids[10, 2] = gameMainWindow.wrapPanelGrid10_2;
            grids[10, 3] = gameMainWindow.wrapPanelGrid10_3;
            grids[10, 4] = gameMainWindow.wrapPanelGrid10_4;
            grids[10, 5] = gameMainWindow.wrapPanelGrid10_5;
            grids[10, 6] = gameMainWindow.wrapPanelGrid10_6;
            grids[10, 7] = gameMainWindow.wrapPanelGrid10_7;
            grids[10, 8] = gameMainWindow.wrapPanelGrid10_8;
            grids[10, 9] = gameMainWindow.wrapPanelGrid10_9;
        }

        public void InitializeButtonToGrid()
        {
            buttonToGrid.Clear();

            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackRookButtonRight"], gameMainWindow.wrapPanelGrid1_1);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackKnightButtonRight"], gameMainWindow.wrapPanelGrid1_2);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackBishopButtonRight"], gameMainWindow.wrapPanelGrid1_3);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackGuardButtonRight"], gameMainWindow.wrapPanelGrid1_4);

            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackKingButton"], gameMainWindow.wrapPanelGrid1_5);

            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackGuardButtonLeft"], gameMainWindow.wrapPanelGrid1_6);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackBishopButtonLeft"], gameMainWindow.wrapPanelGrid1_7);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackKnightButtonLeft"], gameMainWindow.wrapPanelGrid1_8);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackRookButtonLeft"], gameMainWindow.wrapPanelGrid1_9);

            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackCannonButtonRight"], gameMainWindow.wrapPanelGrid3_2);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackCannonButtonLeft"], gameMainWindow.wrapPanelGrid3_8);

            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackPawnButton5"], gameMainWindow.wrapPanelGrid4_1);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackPawnButton4"], gameMainWindow.wrapPanelGrid4_3);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackPawnButton3"], gameMainWindow.wrapPanelGrid4_5);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackPawnButton2"], gameMainWindow.wrapPanelGrid4_7);
            buttonToGrid.Add(chessPiece.BlackPieceButtons["blackPawnButton1"], gameMainWindow.wrapPanelGrid4_9);

            /////////////////////////////////////////////////////////////////////////////////////////////////

            buttonToGrid.Add(chessPiece.RedPieceButtons["redRookButtonLeft"], gameMainWindow.wrapPanelGrid10_1);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redKnightButtonLeft"], gameMainWindow.wrapPanelGrid10_2);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redBishopButtonLeft"], gameMainWindow.wrapPanelGrid10_3);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redGuardButtonLeft"], gameMainWindow.wrapPanelGrid10_4);

            buttonToGrid.Add(chessPiece.RedPieceButtons["redKingButton"], gameMainWindow.wrapPanelGrid10_5);

            buttonToGrid.Add(chessPiece.RedPieceButtons["redGuardButtonRight"], gameMainWindow.wrapPanelGrid10_6);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redBishopButtonRight"], gameMainWindow.wrapPanelGrid10_7);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redKnightButtonRight"], gameMainWindow.wrapPanelGrid10_8);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redRookButtonRight"], gameMainWindow.wrapPanelGrid10_9);

            buttonToGrid.Add(chessPiece.RedPieceButtons["redCannonButtonLeft"], gameMainWindow.wrapPanelGrid8_2);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redCannonButtonRight"], gameMainWindow.wrapPanelGrid8_8);

            buttonToGrid.Add(chessPiece.RedPieceButtons["redPawnButton1"], gameMainWindow.wrapPanelGrid7_1);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redPawnButton2"], gameMainWindow.wrapPanelGrid7_3);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redPawnButton3"], gameMainWindow.wrapPanelGrid7_5);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redPawnButton4"], gameMainWindow.wrapPanelGrid7_7);
            buttonToGrid.Add(chessPiece.RedPieceButtons["redPawnButton5"], gameMainWindow.wrapPanelGrid7_9);
        }
    }
}
