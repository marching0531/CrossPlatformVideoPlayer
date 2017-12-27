using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Models;
using VideoPlayer.ViewModels;
using VideoPlayer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VideoPlayerLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemView : RelativeLayout
    {
        public ItemView(MediaItem item)
        {
            InitializeComponent();

            BindingContext = new ItemViewModel(item);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            WidthRequest = height * 1.5f;
            InvalidateMeasure();
        }

        private void OnItemClicked(object sender, EventArgs e)
        {
            PlayerPage player = new PlayerPage();

            player.Disappearing += (s, ev) => FocusButton.Focus();

            Navigation.PushAsync(player);
        }
    }
}