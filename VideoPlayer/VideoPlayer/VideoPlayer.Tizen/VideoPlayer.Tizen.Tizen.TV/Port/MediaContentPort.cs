using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Content.MediaContent;
using VideoPlayer.Models;
using System.IO;

namespace VideoPlayer.Tizen.Tizen.TV.Port
{
    public class MediaContentPort : IMediaContentAPIs
    {
        private MediaDatabase mediaDatabase;

        private MediaInfoCommand mediaInfoCommand;

        private static readonly string VIDEO_FILTER = "MEDIA_TYPE=1";

        public MediaContentPort()
        {
            mediaDatabase = new MediaDatabase();
            mediaDatabase.Connect();

            mediaInfoCommand = new MediaInfoCommand(mediaDatabase);
        }

        private async Task<string> GetThumbnailPathAsync(VideoInfo information)
        {
            var path = information.ThumbnailPath;

            if (String.IsNullOrEmpty(path))
            {
                try
                {
                    path = await mediaInfoCommand.CreateThumbnailAsync(information.Id);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            return path;
        }

        public async Task<IEnumerable<MediaItem>> GetAllVideoItemListAsync()
        {
            var itemList = new List<MediaItem>();
            var selectArguments = new SelectArguments();

            selectArguments.FilterExpression = VIDEO_FILTER;

            try
            {
                var reader = mediaInfoCommand.SelectMedia(selectArguments);
                
                while (reader.Read())
                {
                    var videoInformation = reader.Current as VideoInfo;
                    itemList.Add(
                        new MediaItem()
                        {
                            Title = videoInformation.Title,
                            Path = videoInformation.Path,
                            Thumbnail = await GetThumbnailPathAsync(videoInformation),
                            Duration = videoInformation.Duration
                        });
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                return itemList;
            }

            return itemList;
        }
    }
}
