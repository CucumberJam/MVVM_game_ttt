using MVVM_game_ttt.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVM_game_ttt.ModelViews
{
    public class StartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<PlayerViewModel> Players { get; set; }

        public ObservableCollection<WinnerResultViewModel> Winners { get; set; }
        public ICommand CreatePlayerCommand { protected set; get; }
        public ICommand SavePlayerCommand { protected set; get; }
        public ICommand DeletePlayerCommand { protected set; get; }
        public ICommand StartGameCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        PlayerViewModel selectedPlayer;
        public INavigation Navigation { get; set; }
        public GameViewModel gameViewModel;
        public CellViewModel cellViewModel;

        public bool showBtnStart = false;
        public bool showBtnAddNewPlayer = true;
        public bool showBtnWinners = false;

        public bool ShowBtnStart
        {
            get { return showBtnStart; }
            set
            {
                if (showBtnStart != value)
                {
                    showBtnStart = value;
                    OnPropertyChanged("ShowBtnStart");
                }
            }
        }
        public bool ShowBtnAddNewPlayer
        {
            get { return showBtnAddNewPlayer; }
            set
            {
                if (showBtnAddNewPlayer != value)
                {
                    showBtnAddNewPlayer = value;
                    OnPropertyChanged("ShowBtnAddNewPlayer");
                }
            }
        }
        public bool ShowBtnWinners
        {
            get { return showBtnWinners; }
            set
            {
                if (showBtnWinners != value)
                {
                    showBtnWinners = value;
                    OnPropertyChanged("ShowBtnWinners");
                }
            }
        }
        
        public StartViewModel()
        {
            Players = new ObservableCollection<PlayerViewModel>();
            Winners = new ObservableCollection<WinnerResultViewModel>();
            CreatePlayerCommand = new Command(CreatePlayer);
            SavePlayerCommand = new Command(SavePlayer);
            DeletePlayerCommand = new Command(DeletePlayer);
            StartGameCommand = new Command(StartGame);
            BackCommand = new Command(Back);
        }
        public PlayerViewModel SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                if (selectedPlayer != value)
                {
                    PlayerViewModel editingPlayer = value;
                    selectedPlayer = null;
                    OnPropertyChanged("SelectedPlayer");
                    Navigation.PushAsync(new PlayerPage(editingPlayer));
                }
            }
        }
        private void CreatePlayer()
        {
            Navigation.PushAsync(new PlayerPage(new PlayerViewModel() { ListViewModel = this }));
        }
        private void SavePlayer(object playerObject)
        {
            PlayerViewModel player = playerObject as PlayerViewModel;
            if (player != null && player.IsValid && !Players.Contains(player))
            {
                Players.Add(player);
                ShowBtns();
            }
            Back();
        }
        private void DeletePlayer(object playerObject)
        {
            PlayerViewModel player = playerObject as PlayerViewModel;
            if (player != null)
            {
                Players.Remove(player);
                ShowBtns();
            }
            Back();
        }
        private async void StartGame()
        {
            if (Players.Count == 2)
            {
                CheckPlayers(Players);
                gameViewModel = new GameViewModel(Players, Winners) 
                { Navigation = Navigation };
                cellViewModel = new CellViewModel() { GameViewModel = gameViewModel };
                await Navigation.PushAsync(new GamePage(cellViewModel));
                
            }
            else return;
            
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void ShowBtns()
        {
            if (Players.Count == 2)
            {
                ShowBtnStart = true;
                ShowBtnAddNewPlayer = false;
                ShowBtnWinners = true;
            }
            else
            {
                ShowBtnStart = false;
                ShowBtnAddNewPlayer = true;
            }
        }
        private void CheckPlayers(ObservableCollection<PlayerViewModel> Players)
        {
            if (Players[0].Mark == Players[1].Mark)
            {
                Players[0].Mark = "X";
                Players[1].Mark = "O";
            }

        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
