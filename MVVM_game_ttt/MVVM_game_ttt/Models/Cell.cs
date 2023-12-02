using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MVVM_game_ttt.Models
{
    public class Cell
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public bool IsCellOccupied()
        {

            if (this.Name.Equals("X") || this.Name.Equals("O"))
            {
                return true;
            }
            else return false;
        }

        public void Mark(string markOfPlayer, Color colorOfPlayer)
        {
            this.Name = markOfPlayer;
            this.Color = colorOfPlayer;
        }
    }
}
