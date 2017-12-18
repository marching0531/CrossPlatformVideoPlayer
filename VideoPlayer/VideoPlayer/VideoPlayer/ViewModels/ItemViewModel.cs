using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Models;
using VideoPlayerLite.ViewModels;

namespace VideoPlayer.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        private MediaItem videoItem;

        public MediaItem VideoItem
        {
            get => videoItem;

            set
            {
                videoItem = value;
                InvokePropertyChanged();
            }
        }

        public ItemViewModel(MediaItem item)
        {
            VideoItem = item;
        }
    }
}
