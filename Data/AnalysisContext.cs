using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Happy_Analysis.Models;

namespace Happy_Analysis.Data
{
    public class AnalysisContext : DbContext
    {
        public AnalysisContext(DbContextOptions<AnalysisContext> options)
            : base(options)
        {
        }
        public DbSet<DataPoint> DataPoints { get; set; }
        public DbSet<DataModel> Models { get; set; }
        public DbSet<ModelRun> Runs { get; set; }
    }
}
