using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Happy_Analysis.Models
{
    public class ModelRun
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Model Name")]
        public string Name { get; set; }
        [Display(Name = "Ref. Point")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal ReferencePoint { get; set; }
        [Column(TypeName = "decimal(6,5)")]
        public decimal F1_Score { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Run Date")]
        public DateTime Run_Date { get; set; }
        [NotMapped]
        public List<int> RunOutput { get; set; }
        [NotMapped]
        public List<int> Control_Y { get; set; }
    }
}
