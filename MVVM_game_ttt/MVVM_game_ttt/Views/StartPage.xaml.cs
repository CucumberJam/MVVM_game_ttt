using MVVM_game_ttt.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM_game_ttt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            BindingContext = new StartViewModel() { Navigation = this.Navigation };
        }
        private void btnPlay_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            StackLayout container = btn.Parent.FindByName("container") as StackLayout;
            Label header = btn.Parent.FindByName("welcomeLabel") as Label;
            header.Text = "List of players: ";
            container.IsVisible = true;
            btn.IsVisible = false;
        }
        private void Button_Start_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Label header = btn.Parent. FindByName("winnersList_head") as Label;
            header.IsVisible = true;
        }
    }
}

       