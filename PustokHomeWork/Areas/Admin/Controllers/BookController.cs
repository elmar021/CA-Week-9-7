using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokHomeWork.Data;
using PustokHomeWork.Models;

namespace PustokHomeWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly AppDbContext _dbContext;
        public BookController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View(_dbContext.Books.Include(book => book.Tags).ToList());
        }

        [HttpPost]
        public IActionResult UpdateBook(BookModel book)
        {

            ViewBag.Authors = _dbContext.Authors.ToList();
            ViewBag.Tags = _dbContext.Tags.ToList();

            ModelState.Remove("Author");
            ModelState.Remove("Tags");


            if (book.TagIds == null)
            {
                ModelState.AddModelError("TagIds", "Invalid Tags");
                return View("Create");
            }

            if (ModelState.IsValid)
            {


                BookModel? old = _dbContext.Books.Include(x=>x.Tags).Include(x=>x.Author).FirstOrDefault(x => x.Id == book.Id);
                
                if(old != null)
                {
             

                _dbContext.SaveChanges();
                }
                else
                {
                    return BadRequest();
                }



                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public IActionResult Update(int id)
        {
            ViewBag.Authors = _dbContext.Authors.ToList();
            ViewBag.Tags = _dbContext.Tags.ToList();

            BookModel? model = _dbContext.Books.FirstOrDefault(x => x.Id == id);

            if (model != null)
            {
                return View(model);
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult Create()
        {
            ViewBag.Authors = _dbContext.Authors.ToList();
            ViewBag.Tags = _dbContext.Tags.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            BookModel? model = _dbContext.Books.FirstOrDefault(x => x.Id == id);

            if(model != null)
            {

                _dbContext.Books.Remove(model);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateBook(BookModel newBook)
        {
            ViewBag.Authors = _dbContext.Authors.ToList();
            ViewBag.Tags = _dbContext.Tags.ToList();

            ModelState.Remove("Author");
            ModelState.Remove("Tags");


            if (ModelState.IsValid)
            {
                if(newBook.TagIds == null)
                {
                    ModelState.AddModelError("TagIds", "Invalid Tags");
                     return View("Create");
                }

                if (!_dbContext.Authors.ToList().Any(x => x.Id == newBook.AuthorId))
                {
                    ModelState.AddModelError("AuthorId", "Invalid Author");
                    return View("Create");
                }


        

                _dbContext.Books.Add(newBook);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {

                return View("Create");
            }

        }


        
    }
}
