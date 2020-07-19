using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Happy_Analysis.Data;
using Happy_Analysis.Models;
using System.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Happy_Analysis.Controllers
{
    public class DataModelController : Controller
    {
        private readonly AnalysisContext _context;
        decimal precision = 0;
        decimal recall = 0;
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>
        {
            {"0,0",0 },
            {"0,1",0 },
            {"1,0",0 },
            {"1,1",0 }
        };
        DataModel dataModel { get; set; }
        public DataModelController(AnalysisContext context)
        {
            _context = context;
        }

        // GET: DataModel
        public async Task<IActionResult> Index()
        {
            //LoadDataModels();
            return View(await _context.Models.ToListAsync());
        }

        // GET: DataModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataModel = await _context.Models
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dataModel == null)
            {
                return NotFound();
            }

            return View(dataModel);
        }

        // GET: DataModel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Percentage1,Percentage2,Percentage3,Percentage4,Percentage5,Percentage6,Created")] DataModel dataModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataModel);
        }

        // GET: DataModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataModel = await _context.Models.FindAsync(id);
            if (dataModel == null)
            {
                return NotFound();
            }
            return View(dataModel);
        }

        // POST: DataModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Percentage1,Percentage2,Percentage3,Percentage4,Percentage5,Percentage6,Created")] DataModel dataModel)
        {
            if (id != dataModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataModelExists(dataModel.ID))
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
            return View(dataModel);
        }

        // GET: DataModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataModel = await _context.Models
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dataModel == null)
            {
                return NotFound();
            }

            return View(dataModel);
        }

        // POST: DataModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataModel = await _context.Models.FindAsync(id);
            _context.Models.Remove(dataModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataModelExists(int id)
        {
            return _context.Models.Any(e => e.ID == id);
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([Bind("file")] IFormFile file)
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
            LoadDataModels(path);

            return RedirectToAction("Index");
        }

        public void LoadDataModels(string path)
        {
            string[] data = System.IO.File.ReadAllLines(path);
            foreach (string line in data)
            {
                string[] row = line.Split(new char[] { ',' });
                DataModel dataModel = new DataModel
                {
                    Name = row[0],
                    Percentage1 = decimal.Parse(row[1]),
                    Percentage2 = decimal.Parse(row[2]),
                    Percentage3 = decimal.Parse(row[3]),
                    Percentage4 = decimal.Parse(row[4]),
                    Percentage5 = decimal.Parse(row[5]),
                    Percentage6 = decimal.Parse(row[6]),
                    Created = DateTime.Now
                };
                if (ModelState.IsValid)
                {
                    _context.Models.Add(dataModel);
                    _context.SaveChanges();
                }
            }
        }

        public IActionResult Execute(int id)
        {
            dataModel = _context.Models.First(x => x.ID == id);
            ModelRun model = new ModelRun
            {
                F1_Score = 0,
                Run_Date = DateTime.Now
            };
            model.Name = dataModel.Name;
            if (dataModel == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Execute([Bind("Name,ReferencePoint,F1_Score,Run_Date,RunOutput,Control_Y")] ModelRun model)
        {
            model.Control_Y = new List<int>();
            model.RunOutput = new List<int>();
            dataModel = _context.Models.First(x => x.Name == model.Name);
            List<DataPoint> dataPoints = _context.DataPoints.ToList();
            foreach (DataPoint point in dataPoints)
            {
                decimal value = point.X1 * dataModel.Percentage1;
                value += point.X2 * dataModel.Percentage2;
                value += point.X3 * dataModel.Percentage3;
                value += point.X4 * dataModel.Percentage4;
                value += point.X5 * dataModel.Percentage5;
                value += point.X6 * dataModel.Percentage6;
                model.Control_Y.Add(point.Y);
                if (value >= model.ReferencePoint)
                {
                    model.RunOutput.Add(1);
                }
                else
                {
                    model.RunOutput.Add(0);
                }
            }
            for (int i = 0; i < model.RunOutput.Count; i++)
            {
                string entry = model.Control_Y[i].ToString() + "," + model.RunOutput[i].ToString();
                if (keyValuePairs.ContainsKey(entry))
                {
                    keyValuePairs[entry] += 1;
                }
            }

            model.F1_Score = Math.Round(CalculateF1Score(), 2);

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ModelRun");
            }

            return View(model);
        }

        private decimal CalculateF1Score()
        {
            int truePositive = keyValuePairs["1,1"];
            int actualPositive = keyValuePairs["0,1"] + truePositive;
            int predictedPositive = keyValuePairs["1,0"] + truePositive;

            precision = truePositive / (decimal)predictedPositive;
            recall = truePositive / (decimal)actualPositive;
            return 2 * ((precision * recall) / (precision + recall));
        }

        public IActionResult ExecuteAll()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExecuteAll([Bind("ReferencePoint")] ModelRun modelRun)
        {
            List<int> target = new List<int>();
            List<DataModel> dataModels = _context.Models.ToList();
            List<DataPoint> dataPoints = _context.DataPoints.ToList();

            foreach (DataPoint point in dataPoints)
            {
                target.Add(point.Y);
            }

            foreach (DataModel model in dataModels)
            {
                ModelRun prime = new ModelRun
                {
                    ReferencePoint = modelRun.ReferencePoint,
                    Control_Y = target,
                    RunOutput = new List<int>(),
                    Name = model.Name,
                    Run_Date = DateTime.Now,
                };

                foreach (DataPoint point in dataPoints)
                {
                    decimal value = point.X1 * model.Percentage1;
                    value += point.X2 * model.Percentage2;
                    value += point.X3 * model.Percentage3;
                    value += point.X4 * model.Percentage4;
                    value += point.X5 * model.Percentage5;
                    value += point.X6 * model.Percentage6;
                    
                    if (value >= prime.ReferencePoint)
                    {
                        prime.RunOutput.Add(1);
                    }
                    else
                    {
                        prime.RunOutput.Add(0);
                    }

                    int last = prime.RunOutput.Count - 1;
                    string entry = prime.Control_Y[last].ToString() + "," + prime.RunOutput[last].ToString();
                    if (keyValuePairs.ContainsKey(entry))
                    {
                        keyValuePairs[entry] += 1;
                    }
                }

                prime.F1_Score = Math.Round(CalculateF1Score(), 2);
                if (ModelState.IsValid)
                {
                    _context.Add(prime);
                    await _context.SaveChangesAsync();
                }
                
                //reset counters
                resetDictionaryCount();
            }

            return RedirectToAction("Index", "ModelRun");
        }

        private void resetDictionaryCount()
        {
            List<string> list = keyValuePairs.Keys.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                string key = list[i];
                keyValuePairs[key] = 0;
            }
        }
    }
}
