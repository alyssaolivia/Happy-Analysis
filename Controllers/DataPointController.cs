using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Happy_Analysis.Data;
using Happy_Analysis.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Happy_Analysis.Controllers
{
    public class DataPointController : Controller
    {
        private readonly AnalysisContext _context;

        public DataPointController(AnalysisContext context)
        {
            _context = context;
        }

        // GET: DataPoint
        public async Task<IActionResult> Index()
        {
            //LoadDataPoints();
            return View(await _context.DataPoints.ToListAsync());
        }

        // GET: DataPoint/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPoint = await _context.DataPoints
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dataPoint == null)
            {
                return NotFound();
            }

            return View(dataPoint);
        }

        // GET: DataPoint/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataPoint/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Y,X1,X2,X3,X4,X5,X6")] DataPoint dataPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataPoint);
        }

        // GET: DataPoint/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPoint = await _context.DataPoints.FindAsync(id);
            if (dataPoint == null)
            {
                return NotFound();
            }
            return View(dataPoint);
        }

        // POST: DataPoint/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,X1,X2,X3,X4,X5,X6")] DataPoint dataPoint)
        {
            if (id != dataPoint.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataPointExists(dataPoint.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dataPoint);
        }

        // GET: DataPoint/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPoint = await _context.DataPoints
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dataPoint == null)
            {
                return NotFound();
            }

            return View(dataPoint);
        }

        // POST: DataPoint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataPoint = await _context.DataPoints.FindAsync(id);
            _context.DataPoints.Remove(dataPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataPointExists(int id)
        {
            return _context.DataPoints.Any(e => e.ID == id);
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([Bind("file")]IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "File upload was unsuccessful";
                return Content("File not selected");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            LoadDataPoints(path);

            return RedirectToAction("Index");
        }

    public void LoadDataPoints(string path)
    {
            if (System.IO.File.Exists(path))
            {
                string[] data = System.IO.File.ReadAllLines(path);
                int lineCount = 0;
                foreach (string line in data)
                {
                    if (lineCount != 0)//assumption: data has headers so skip them
                    {
                        string[] row = line.Split(new char[] { ',' });
                        DataPoint dataPoint = new DataPoint
                        {
                            Y = int.Parse(row[0]),
                            X1 = int.Parse(row[1]),
                            X2 = int.Parse(row[2]),
                            X3 = int.Parse(row[3]),
                            X4 = int.Parse(row[4]),
                            X5 = int.Parse(row[5]),
                            X6 = int.Parse(row[6])
                        };
                        if (ModelState.IsValid)
                        {
                            _context.Add(dataPoint);
                            _context.SaveChanges();
                        }
                    }
                    lineCount++;
                }
            }
        }
    }
}
