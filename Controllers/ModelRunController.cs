using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Happy_Analysis.Data;
using Happy_Analysis.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Happy_Analysis.Controllers
{
    public class ModelRunController : Controller
    {
        private readonly AnalysisContext _context;

        public ModelRunController(AnalysisContext context)
        {
            _context = context;
        }

        // GET: ModelRun
        public async Task<IActionResult> Index()
        {
            return View(await _context.Runs.ToListAsync());
        }

        // GET: ModelRun/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelRun = await _context.Runs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (modelRun == null)
            {
                return NotFound();
            }

            return View(modelRun);
        }

        // GET: ModelRun/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelRun = await _context.Runs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (modelRun == null)
            {
                return NotFound();
            }

            return View(modelRun);
        }

        // POST: ModelRun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelRun = await _context.Runs.FindAsync(id);
            _context.Runs.Remove(modelRun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
