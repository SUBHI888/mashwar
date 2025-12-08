using mashwar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace mashwar.Controllers
{
    public class PlaceController : Controller
    {
        public readonly AppDbContext _Db;
        private IWebHostEnvironment _environment;
        public PlaceController(AppDbContext db, IWebHostEnvironment environment)
        {
            _Db = db;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _Db.places.Include(e => e.Users).ToListAsync();
            return View("index", data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.App = new SelectList(_Db.Users, "User_Id", "User_Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Place model, IFormFile File)
        {
            ViewBag.App = new SelectList(_Db.Users, "User_Id", "User_Name");
            model.Place_Image = Uploade_Image(File);
            _Db.places.Add(model);
            _Db.SaveChanges();
            return RedirectToAction("Index");
        }
        private string Uploade_Image(IFormFile CourseInput)
        {
            string wwwPath = _environment.WebRootPath;
            string contentPath = this._environment.ContentRootPath;

            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileNameWithoutExtension(CourseInput.FileName);
            string newName = fileName + Guid.NewGuid().ToString() + Path.GetExtension(CourseInput.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, newName), FileMode.Create))
            {
                CourseInput.CopyTo(stream);
                //    uploadedFiles.Add(fileName);
                //  ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
            }

            return "\\Uploads\\" + newName;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var place = _Db.places.Include(e =>e.Users).FirstOrDefault(x => x.PlaceID == id);
            if (place == null)
            {
                return NotFound();
            }
                
                return View("Delete", place);





            }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm_Delete(int id)
        {
            var pla = _Db.places.Find(id);
            if (pla == null)
            {
                return NotFound();
            }
            _Db.places.Remove(pla);
            _Db.SaveChanges();
            return RedirectToAction("index");


        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _Db.places.Find(id);
            if (data == null) { return NotFound(); }
            ViewBag.part = new SelectList(_Db.Users, "User_Id", "User_Name",data.User_Id);
            return View("Edit", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Place model)
        {
            //if (ModelState.IsValid)
            {
                var place_data = _Db.places.FirstOrDefault(e => e.PlaceID == model.PlaceID);
                if (place_data == null) { return NotFound(); }
                place_data.Name = model.Name;
                place_data.Description = model.Description;
                place_data.Location = model.Location;
                place_data.Rating = model.Rating;
                place_data.User_Id = model.User_Id;
                

                _Db.SaveChanges();
                return RedirectToAction("index");

            }
            //ViewBag.part = new SelectList(_Db.Users, "User_Id", "User_Name", model.User_Id);
            //return View(model);
        }
        public IActionResult Amman()
        {
            return View();
        }
       public IActionResult Maan()
        {
            return View();
        }
        public IActionResult Aqaba()
        {
            return View();
        }
      public IActionResult zarqa()
        {
            return View();
        }
        public IActionResult ResturantAmman()
        {
            return View();
        }
        public IActionResult CafeesAmman() 
        {
            return View();
        }
        public IActionResult HotelAmman()
        {
            return View();
        }
        public IActionResult EntertainmentAmman()
        {  return View();}
        public IActionResult ShoppingCentersAmman()
        { return View();}
        public IActionResult TouristPlacesAmman()
        { return View(); }
    }
}


