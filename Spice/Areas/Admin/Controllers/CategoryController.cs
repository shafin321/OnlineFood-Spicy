using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spice.Interface;

namespace Spice.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }
        public IActionResult Index()
        {
            var model = _category.GetAll();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var model = _category.Delete(id);

            return View("index");
        }
    }
}