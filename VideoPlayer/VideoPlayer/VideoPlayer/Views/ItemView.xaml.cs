using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Models;
using VideoPlayer.ViewModels;
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
    }
}