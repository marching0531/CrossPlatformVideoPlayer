using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VideoPlayer.Models
{ 
    public sealed class MediaItemProvider
    {
            private static readonly Lazy<MediaItemProvider> lazy = new Lazy<MediaItemProvider>(() => new MediaItemProvider());
            public static MediaItemProvider Instance { get => lazy.Value; }

        private static IMediaContentAPIs mediaContentAPIs;

        private IEnumerable<MediaItem> itemList;
        public IEnumerable<MediaItem> ItemList { get => itemList; }

        private MediaItemProvider()
        {
            if(DependencyService.Get<IMediaContentAPIs>() != null)
            {
                mediaContentAPIs = DependencyService.Get<IMediaContentAPIs>();
            }
        }

        public async Task Update()
        {
            itemList = await mediaContentAPIs.GetAllVideoItemListAsync();
        }


    }
}
