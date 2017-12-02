using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Models;
using VideoPlayerLite.ViewModels;
using Xamarin.Forms;

namespace VideoPlayer.ViewModels
{
    public class ListPageViewModel : ViewModelBase
    {
        private IEnumerable<MediaItem> videoList;

        public IEnumerable<MediaItem> VideoList
        {
            get => videoList;
            private set
            {
                videoList = value;
                InvokePropertyChanged();
            }
        }

        private Command updateVideoListCommand;

        public Command UpdateVideoListCommand
        {
            get => updateVideoListCommand;

            set
            {
                updateVideoListCommand = value;
                InvokePropertyChanged();
            }
        }

        private async void UpdateItemList()
        {
            await MediaItemProvider.Instance.Update();
            VideoList = MediaItemProvider.Instance.ItemList;
        }

        public ListPageViewModel()
        {
            UpdateVideoListCommand = new Command(() => UpdateItemList());
        }
    }
}
