using MVVM_game_ttt.ModelViews;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM_game_ttt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private CellViewModel CellViewModel { get; set; }
        public GamePage(CellViewModel vm)   
        {
            InitializeComponent();
            CellViewModel = vm; 
            BindingContext = CellViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        void slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if(CellViewModel.GameViewModel.CellWidth != null) 
                CellViewModel.GameViewModel.CellWidth = (e.NewValue * 100).ToString();
        }
    }
}