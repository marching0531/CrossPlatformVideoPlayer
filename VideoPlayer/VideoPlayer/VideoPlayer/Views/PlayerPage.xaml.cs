using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VideoPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public static readonly BindableProperty FetchVideoCommandProperty = BindableProperty.Create("FetchVideoCommand", typeof(Command), typeof(PlayerPage), null);
        public Command FetchVideoCommand
        {
            get => (Command)GetValue(FetchVideoCommandProperty);
        }

        public PlayerPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FetchVideoCommand.Execute(null);
        }
    }
}