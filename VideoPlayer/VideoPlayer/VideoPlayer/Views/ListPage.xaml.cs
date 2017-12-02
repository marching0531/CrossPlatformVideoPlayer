using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VideoPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private bool isLoaded;

        public static readonly BindableProperty VideoListProperty = BindableProperty.Create("VideoList", typeof(IEnumerable<MediaItem>), typeof(ListPage), null);

        public IEnumerable<MediaItem> VideoList
        {
            get { return (IEnumerable<MediaItem>)GetValue(VideoListProperty); }
        }

        public static readonly BindableProperty UpdateVideoListCommandProperty = BindableProperty.Create("UpdateVideoLisstCommand", typeof(Command), typeof(ListPage), null);

        public Command UpdateVideoListCommand
        {
            get { return (Command)GetValue(UpdateVideoListCommandProperty); }
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VideoListProperty.PropertyName)
            {
                UpdateGridItems();
            }
        }

        private void UpdateGridItems()
        {
            var index = 0;

            GridView.Children.Clear();

            foreach (var item in VideoList)
            {
                GridView.Children.Add(new Button() { Text = item.Title }, index / 3, index % 3);
                index++;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!isLoaded)
            {
                UpdateVideoListCommand.Execute(null);
                isLoaded = true;
            }
        }

        public ListPage()
        {
            InitializeComponent();
            isLoaded = false;
            PropertyChanged += OnPropertyChanged;
        }
    }
}