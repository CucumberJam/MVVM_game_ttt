using MVVM_game_ttt.Models;
using MVVM_game_ttt.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace MVVM_game_ttt.ModelViews
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PlayerViewModel> Players;
        public ObservableCollection<CellViewModel> Cells { get; set; }
        public ObservableCollection<WinnerResultViewModel> Winners { get; set; }

        public ICommand IChangeFieldCommand { protected set; get; }

        public ICommand RestartCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        public int index = 0;
        public GameViewModel(ObservableCollection<PlayerViewModel> players, ObservableCollection<WinnerResultViewModel> winners)
        {
            Cells = InitializeCells();
            Players = players;
            Winners = winners;
            IChangeFieldCommand = new Command(ChangeField);
            RestartCommand = new Command(Restart);
            header = "Ходит " + Players[index].Name;
        }
        private string header;
        public string Header
        {
            get { return header; }
            set
            {
                if (header != value)
                {
                    header = value;
                    OnPropertyChanged("Header");
                }
            }
        }

        private string footer = "";
        public string Footer
        {
            get { return footer; }
            set
            {
                if (footer != value)
                {
                    footer = value;
                    OnPropertyChanged("Footer");
                }
            }
        }

        private string cellWidth = "140";
        public string CellWidth
        {
            get { return cellWidth; }
            set
            {
                if (cellWidth != value)
                {
                    cellWidth = value;
                    OnPropertyChanged("CellWidth");
                }
            }
        }
        public void ChangeField(object cellObj)
        {
            //Button btnChoice = sender as Button;
            CellViewModel cell = cellObj as CellViewModel;
            int choice = int.Parse(cell.Name);
            if (Cells[choice - 1].IsCellOccupied())
            {
                Footer = "Ячейка занята, попробуйте другую";
                return;
            }
            else
            {
                Cells[choice - 1].Mark(Players[index].Mark, Players[index].Color);
                int check = Check();
                if (check == 1)
                {
                    Footer = "Выиграл  " + Players[index].Name;
                }
                else if (check == -1)
                {
                    Footer = "Победила дружба!";
                }
                else
                {
                    index = TurnPlayer(index);
                    Header = "Ходит " + Players[index].Name;
                }
            }
        }
        public void ChangeCell(string text)
        {
            int choice = int.Parse(text);
            if (Cells[choice - 1].IsCellOccupied())
            {
                Footer = "Ячейка занята, попробуйте другую";
                return;
            }
            else
            {
                Cells[choice - 1].Mark(Players[index].Mark, Players[index].Color);

                int check = Check();
                if (check == 1)
                {
                    Footer = "Выиграл  " + Players[index].Name + " !";
                    WinnerResultViewModel winnerRow = new WinnerResultViewModel(Players[index].Name, Players[TurnPlayer(index)].Name, 1, 0);
                    CheckWinnerRows(winnerRow, Winners);
                }
                else if (check == -1)
                {
                    Footer = "Победила дружба!";
                }
                else
                {
                    index = TurnPlayer(index);
                    Header = "Ходит " + Players[index].Name;
                }
            }
        }
        private void CheckWinnerRows(WinnerResultViewModel winnerRow, ObservableCollection<WinnerResultViewModel> winners)
        {
            WinnerResultViewModel selectedWinner = null;
            try
            {
                selectedWinner = winners.First(p =>
                            winnerRow.Player1_Name == p.Player1_Name || winnerRow.Player1_Name == p.Player2_Name);
            }
            catch
            {
                winners.Add(winnerRow);
            }
            finally
            {
                if (selectedWinner != null)
                {
                    //Означает что Имя первого игрока совпало с Именем первого игрока из строки в списке победителей как и имя вторых
                    if (selectedWinner.Player2_Name == winnerRow.Player2_Name)
                    {
                        int index = winners.IndexOf(selectedWinner);
                        winners[index].Player1_Result += winnerRow.Player2_Result;
                        winners[index].Player2_Result += winnerRow.Player2_Result;

                    }
                    //Означает что Имя первого игрока совпало с Именем второго игрока из строки в списке победителей как и имя второго игрока с Именем первого игрока из строки в списке победителей
                    else if (selectedWinner.Player1_Name == winnerRow.Player2_Name)
                    {
                        int index = winners.IndexOf(selectedWinner);
                        winners[index].Player1_Result += winnerRow.Player2_Result;
                        winners[index].Player2_Result += winnerRow.Player1_Result;

                    }
                    // Означает что только имя первого игрока совпало с одним именем из строки, но не второе.
                    else
                    {
                        winners.Add(winnerRow);
                    }
                }
            }
        }
        private void Restart()
        {

            Navigation.PopToRootAsync();
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private int TurnPlayer(int index)
        {
            return (index == 1) ? 0 : 1;
        }
        private ObservableCollection<CellViewModel> InitializeCells()
        {
            ObservableCollection<CellViewModel> array = new ObservableCollection<CellViewModel>();
            for (int i = 0; i < 9; i++)
            {
                CellViewModel cell = new CellViewModel();
                cell.Name = (i + 1).ToString();
                cell.Color = Color.Gray;
                cell.GameViewModel = this;
                array.Add(cell);
            }
            return array;
        }
        public int Check()
        {
            if (hasWinner())
            {
                return 1;
            }
            else if (isDraw())
            {
                return -1;
            }
            else return 0;
        }
        private bool hasWinner()
        {
            return (isHorizontalWinner() || isVerticalWinner() || isDiagonalWinner());
        }
        private bool isHorizontalWinner()
        {
            return isLineWin(0, 1, 2) ||
                isLineWin(3, 4, 5) ||
                isLineWin(6, 7, 8);
        }
        private bool isVerticalWinner()
        {
            return isLineWin(0, 3, 6) || isLineWin(1, 4, 7) ||
                isLineWin(2, 5, 8);
        }
        private bool isDiagonalWinner()
        {
            return isLineWin(0, 4, 8) || isLineWin(2, 4, 6);
        }
        private bool isLineWin(int cellFirst, int cellSecond, int cellThird)
        {
            return Cells[cellFirst].Name == Cells[cellSecond].Name &&
                Cells[cellSecond].Name == Cells[cellThird].Name;
        }
        public bool isDraw()
        {
            for (int i = 1; i <= 9; i++)
            {
                if (Cells[i - 1].Name.Equals(i.ToString()))
                {
                    return false;
                };
            }
            return true;
        }

    }
}
