using MVVM_game_ttt.Models;
using System.ComponentModel;
using System.Drawing;
namespace MVVM_game_ttt.ModelViews
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        StartViewModel listViewModel;
        public Player Player { get; set; }

        public PlayerViewModel()
        {
            Player = new Player();
        }
        public StartViewModel ListViewModel
        {
            get { return listViewModel; }
            set
            {
                if(listViewModel != value)
                {
                    listViewModel = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }
        public string Name
        {
            get { return Player.Name; }
            set
            {
                if (Player.Name != value)
                {
                    Player.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Mark
        {
            get { return Player.Mark; }
            set
            {
                if (Player.Mark != value)
                {
                    Player.Mark = value;
                    OnPropertyChanged("Mark");
                }
            }

        }
        public Color Color
        {
            get { return Player.Color; }
            set
            {
                if (Player.Color != value)
                {
                    Player.Color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(Name.Trim()));
            }

        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
