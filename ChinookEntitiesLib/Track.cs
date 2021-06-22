using System.Collections.Generic;
using System;

namespace UWS.Shared
{
    public class Track
    {
         public string TrackId { get; set; }
 public string Name { get; set; }
  public string AlbumId { get; set; }
 public string Composer { get; set; }
 public int Milliseconds { get; set; }
 public int UnitPrice { get; set; }

 // related entities
 //public Album Album { get; set; }
  //public Artist Artist{ get; set; }

    }
}