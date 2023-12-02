using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM_game_ttt.Models
{
    public class WinnerResult
    {
        public string Player1_Name { get; }
        public string Player2_Name { get; }

        public int Player1_Result { get; set; }
        public int Player2_Result { get; set; }

        public WinnerResult(string Player1_Name, string Player2_Name, int Player1_Result, int Player2_Result)
        {
            this.Player1_Name = Player1_Name;
            this.Player2_Name = Player2_Name;
            this.Player1_Result = Player1_Result;
            this.Player2_Result = Player2_Result;
        }
    }
}
