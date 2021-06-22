using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using UWS.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ChinookWeb.Pages
{
 public class AlbumsModel : PageModel
 {

private Chinook db;
public AlbumsModel(Chinook injectedContext)
{
 db = injectedContext;
}

[BindProperty]
public Album Album { get; set; }
public IActionResult OnPost()
{
 if (ModelState.IsValid)
 {
 db.Albums.Add(Album);
 db.SaveChanges();
 return RedirectToPage("/Albums");
 }
 return Page();
}


 public IEnumerable<string> Albums { get; set; }


public IList<Album> albums { get; set; }
 public void OnGet()
 {
     Albums = db.Albums.Select(s => s.Title);
  

 ViewData["Title"] = "Chinook Web Site - Albums";
 //Albums = new[] {
 //"AC/DC", "Accept", "Aerosmith"
 //};
 albums = db.Albums.ToList();
 }
 }

}