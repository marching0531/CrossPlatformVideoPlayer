using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPlayer.Models
{
    public interface IMediaContentAPIs
    {
        Task<IEnumerable<MediaItem>> GetAllVideoItemListAsync();
    }
}
