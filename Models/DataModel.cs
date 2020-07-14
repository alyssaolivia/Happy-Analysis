using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Happy_Analysis.Models
{
    public class DataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Display(Name = "X1")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal Percentage1 { get; set; }
        [Display(Name = "X2")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal Percentage2 { get; set; }
        [Display(Name = "X3")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal Percentage3 { get; set; }
        [Display(Name = "X4")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal Percentage4 { get; set; }
        [Display(Name = "X5")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal Percentage5 { get; set; }
        [Display(Name = "X6")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal Percentage6 { get; set; }
        public DateTime Created { get; set; }
    }
}
