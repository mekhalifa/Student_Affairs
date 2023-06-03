using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
