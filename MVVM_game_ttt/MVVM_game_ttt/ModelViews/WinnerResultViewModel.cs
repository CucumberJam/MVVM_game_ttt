using MVVM_game_ttt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MVVM_game_ttt.ModelViews
{
    public class WinnerResultViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public WinnerResult WinnerResult { get; set; }
        public string Player1_Name
        {
            get { return WinnerResult.Player1_Name; }
        }

        public string Player2_Name
        {
            get { return WinnerResult.Player2_Name; }
        }
        public int Player1_Result
        {
            get { return WinnerResult.Player1_Result; }
            set
            {
                if (WinnerResult.Player1_Result != value)
                {
                    WinnerResult.Player1_Result = value;
                    OnPropertyChanged("Player1_Result");
                }
            }
        }
        public int Player2_Result
        {
            get { return WinnerResult.Player2_Result; }
            set
            {
                if (WinnerResult.Player2_Result != value)
                {
                    WinnerResult.Player2_Result = value;
                    OnPropertyChanged("Player2_Result");
                }
            }
        }
        public WinnerResultViewModel(string Player1_Name, string Player2_Name, int Player1_Result, int Player2_Result)
        {
            WinnerResult = new WinnerResult(Player1_Name, Player2_Name, Player1_Result, Player2_Result);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
