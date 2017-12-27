using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Models;
using VideoPlayer.ViewModels;
using Xamarin.Forms;

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

        private Command setNewVideoCommand;

        public Command SetNewVideoCommand
        {
            get => setNewVideoCommand;
            set
            {
                setNewVideoCommand = value;
                InvokePropertyChanged();
            }
        }

        public ItemViewModel(MediaItem item)
        {
            VideoItem = item;
            SetNewVideoCommand = new Command(() => MediaItemProvider.Instance.SelectedItem = VideoItem);
        }
    }
}
