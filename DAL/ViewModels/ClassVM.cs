using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ClassVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
