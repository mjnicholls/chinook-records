using System;
using System.Collections.Generic;

namespace UWS.Shared
{
    public class Album
    {
         public string AlbumId { get; set; }
         public string Title { get; set; }
          public string ArtistId { get; set; }

 // related entities
 //public virtual Artist Artist { get; set; }
  //public ICollection<Track> Tracks { get; set; }

    }
}