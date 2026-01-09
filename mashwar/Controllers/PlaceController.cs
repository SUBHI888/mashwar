using mashwar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace mashwar.Controllers
{
    public class PlaceController : Controller
    {
        private readonly AppDbContext _Db;
        private readonly IWebHostEnvironment _environment;

        public PlaceController(AppDbContext db, IWebHostEnvironment environment)
        {
            _Db = db;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _Db.places.Include(p => p.Users).ToListAsync();
            return View(data);
        }

       
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_Db.Users, "User_Id", "User_Name");
            ViewBag.Categories = new SelectList(_Db.categories, "CategoryName", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Place model, IFormFile File)
        {
            if (File != null)
                model.Place_Image = UploadImage(File);

            _Db.places.Add(model);
            _Db.SaveChanges();
            return RedirectToAction("Index");
        }

        private string UploadImage(IFormFile file)
        {
            string path = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string newName = fileName + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            using (var stream = new FileStream(Path.Combine(path, newName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Uploads/" + newName;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var place = _Db.places.Find(id);
            if (place == null) return NotFound();

            ViewBag.Users = new SelectList(_Db.Users, "User_Id", "User_Name", place.User_Id);
            ViewBag.Categories = new SelectList(_Db.categories, "CategoryName", "CategoryName", place.CategoryName);
            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Place model, IFormFile File)
        {
            var place = _Db.places.FirstOrDefault(p => p.PlaceID == model.PlaceID);
            if (place == null) return NotFound();

            place.Name = model.Name;
            place.Description = model.Description;
            place.Location = model.Location;
            place.Rating = model.Rating;
            place.PriceLevel = model.PriceLevel;
            place.User_Id = model.User_Id;
            place.CategoryName = model.CategoryName;

            if (File != null)
                place.Place_Image = UploadImage(File);

            _Db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var place = _Db.places.Include(p => p.Users).FirstOrDefault(p => p.PlaceID == id);
            if (place == null) return NotFound();
            return View(place);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var place = _Db.places.Find(id);
            if (place == null) return NotFound();

            _Db.places.Remove(place);
            _Db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PlacesByCategory(string city, string category)
        {
            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(category))
                return NotFound();

            var data = _Db.places
                .Where(p => p.Location.ToLower() == city.ToLower() && p.CategoryName.ToLower() == category.ToLower())
                .ToList();

            ViewBag.City = city;
            ViewBag.Category = category;

            return View(data);
        }
        public IActionResult MaanPlaces()
        {
            var places = _Db.places
                .Where(p => p.Location.ToLower() == "maan" &&
                            (p.CategoryName.ToLower() == "restaurant" ||
                             p.CategoryName.ToLower() == "cafe" ||
                             p.CategoryName.ToLower() == "hotel"))
                .ToList();

            return View(places); 
        }



        public IActionResult Amman() => View();
        public IActionResult Aqaba() => View();
        public IActionResult Maan() => View();
        public IActionResult Zarqa() => View();
    }
}
