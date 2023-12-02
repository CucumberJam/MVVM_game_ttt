using MVVM_game_ttt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVM_game_ttt.ModelViews
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        GameViewModel gameViewModel;
        public Models.Cell Cell { get; private set; }
        public ICommand IChangeFieldCommand { protected set; get; }

        public CellViewModel()
        {
            Cell = new Models.Cell();
            IChangeFieldCommand = new Command(ChangeField);
        }
        public void ChangeField(object cellObj)
        {
            CellViewModel cell = cellObj as CellViewModel;
            if(cell != null) gameViewModel.ChangeCell(cell.Name);

        }
        public GameViewModel GameViewModel
        {
            get { return gameViewModel; }
            set
            {
                if (gameViewModel != value)
                {
                    gameViewModel = value;
                    OnPropertyChanged("GameViewModel");
                }
            }
        }
        public string Name
        {
            get { return Cell.Name; }
            set
            {
                if (Cell.Name != value)
                {
                    Cell.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public System.Drawing.Color Color
        {
            get { return Cell.Color; }
            set
            {
                if (Cell.Color != value)
                {
                    Cell.Color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        public bool IsCellOccupied()
        {
            return Cell.IsCellOccupied();
        }
        public void Mark(string mark, System.Drawing.Color color)
        {
            Cell.Mark(mark, color);
            OnPropertyChanged("Name");
            OnPropertyChanged("Color");
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
