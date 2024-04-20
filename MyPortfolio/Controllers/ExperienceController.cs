using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize(Roles = "admin")]
    public class ExperienceController : Controller
    {
        private readonly MyPortfolioContext context;
        public ExperienceController(MyPortfolioContext _context)
        {
            context = _context;
        }
        
        public IActionResult ExperienceList()
        {
            var values= context.Experiences.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            context.Experiences.Add(experience);
            context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }
        public IActionResult DeleteExperience(int id)
        {
            var value = context.Experiences.Find(id);
            context.Experiences.Remove(value);
            context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }
        [HttpGet]
        public IActionResult UpdateExperience(int id) // wqeqweq
        {
			var value = context.Experiences.Find(id); 
			return View(value); // Experience nesnesi UpdateExperience sayfasına gönderilir.
		}
        [HttpPost] 
        public IActionResult UpdateExperience(Experience experience) // Güncelleme işlemi için HttpPost metodu oluşturulur.
        {
            context.Experiences.Update(experience); // Update metodu ile güncelleme işlemi yapılır. 			
            context.SaveChanges(); // Değişiklikler veritabanına kaydedilir.
			return RedirectToAction("ExperienceList"); // ExperienceList sayfasına yönlendirme yapılır.
		}
       
        
    }
}
