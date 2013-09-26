using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChessServer
{
    class VSGroup
    {
        private Client playerA;
        private Client playerB;
        private Client currentTurnPlayer;
        private bool playerAReady;
        private bool playerBReady;

        public VSGroup()
        {
            playerA = null;
            playerB = null;
            currentTurnPlayer = playerA;
            playerAReady = false;
            playerBReady = false;
        }

        public bool PlayerAReady
        {
            get { return playerAReady; }
            set { playerAReady = value; }
        }

        public bool PlayerBReady
        {
            get { return playerBReady; }
            set { playerBReady = value; }
        }

        public Client PlayerA
        {
            get { return playerA; }
            set { playerA = value; }
        }

        public Client PlayerB
        {
            get { return playerB; }
            set { playerB = value; }
        }

        public Client CurrentTurnPlayer
        {
            get { return currentTurnPlayer; }
            set { currentTurnPlayer = value; }
        }

        public bool CanStart()
        {
            if (playerAReady && playerBReady)
            {
                return true;
            }
            return false;
        }
    }
}
