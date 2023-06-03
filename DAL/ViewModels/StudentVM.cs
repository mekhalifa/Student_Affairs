using DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class StudentVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    
        public string Photo { get; set; }
        [Required]
        public int? ClassID { get; set; }

    }
}