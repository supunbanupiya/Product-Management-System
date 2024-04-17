using CurdWithDDL.DAL;
using CurdWithDDL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurdWithDDL.Controllers
{
   
    public class CategoryController : Controller
    {
        private readonly MyAppDbContext _context;
        public CategoryController(MyAppDbContext context)
        {
            _context = context;
        }
        //Get:Category
        public async Task<IActionResult> Index()
        {
            return  _context.Categories != null ?
                View(await _context.Categories.ToListAsync()) :
                Problem("Entity set 'MyAppDbContext.Categories' is null");
        }
        //Get:Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id==null || _context.Categories==null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FirstOrDefaultAsync(m=>m.Id==id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //Get : Category/Create
        public IActionResult Create()
        {
            return View();
        }
        //Post: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if(ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        //Get : Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null || _context.Categories==null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if(category==null)
            {
                return NotFound();
            }
            return View(category);
        }
        //Post : Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if(id!=category.Id) 
            { 
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        //Get : Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null||_context.Categories==null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if(category == null)
            {
                return NotFound();
            }
           return View(category) ;
        }
        //Post : Category/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.Categories==null)
            {
                return Problem("Entity set 'MyAppDbContext.Categories' is null");
            }
            var category = await _context.Categories.FindAsync(id);
            if(category!= null)
            {
                _context.Categories.Remove(category);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
      
    }
}
