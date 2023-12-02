using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace MVVM_game_ttt.Models
{
    public class Player
    {
        private static int countForColor = 0;
        private static int countForMark = 0;

        private Color[] arrayColors = { Color.Blue, Color.LightBlue, Color.Pink, Color.Orange, Color.DarkSlateBlue, Color.Yellow, Color.Purple };
        public string Name { get; set; }

        private string mark;
        public string Mark
        {
            get { return mark; }
            set { mark = (countForMark++ % 2 == 0) ? "X" : "O"; }
        }
        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = arrayColors[Checked(ref countForColor)]; }
        }
        public Player()
        {
            mark = (countForMark++ % 2 == 0) ? "X" : "O";
            color = arrayColors[Checked(ref countForColor)];
        }

        private int Checked(ref int count)
        {
            count++;
            return (count == (arrayColors.Length - 1)) ? 0 : count;
        }

    }
}
