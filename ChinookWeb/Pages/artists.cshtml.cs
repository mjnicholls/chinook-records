using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using UWS.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ChinookWeb.Pages
{
 public class ArtistsModel : PageModel
 {

private Chinook db;
public ArtistsModel(Chinook injectedContext)
{
 db = injectedContext;
}

[BindProperty]
public Artist Artist { get; set; }
public IActionResult OnPost()
{
 if (ModelState.IsValid)
 {
 db.Artists.Add(Artist);
 db.SaveChanges();
 return RedirectToPage("/artists");
 }
 return Page();
}


 public IEnumerable<string> Artists { get; set; }
     public IEnumerable<string>  ArtistId { get; set; }
         public IEnumerable<string>  Name { get; set; }

         public IList<Artist> artists { get; set; }
 public void OnGet()
 {
     Artists = db.Artists.Select(s => s.Name);
  ArtistId = db.Artists.Select(s => s.ArtistId);
    Name = db.Artists.Select(s => s.Name);


 ViewData["Title"] = "Chinook Web Site - Artists";
 //Artists = new[] {
 //"AC/DC", "Accept", "Aerosmith"
 //};
  artists = db.Artists.ToList();
 }
 }

}