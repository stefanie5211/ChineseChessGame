using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    public class Player
    {
        private string playerName;
        private string playerIPAddress;
        private string playerState;

        public string PlayerName 
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public string PlayerIPAddress 
        {
            get { return playerIPAddress; }
            set { playerIPAddress = value; } 
        }

        public string PlayerState
        {
            get { return playerState; }
            set { playerState = value; } 
        }
    }
}
