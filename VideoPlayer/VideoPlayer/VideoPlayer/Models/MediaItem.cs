using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPlayer.Models
{
    public class MediaItem
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public string Thumbnail { get; set; }
        public int Duration { get; set; }
    }
}
