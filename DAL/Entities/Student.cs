using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Entities
{

    public  class Student
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(250)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public string Photo { get; set; }

        public int? ClassID { get; set; }

        public virtual Class Class { get; set; }
    }
}
