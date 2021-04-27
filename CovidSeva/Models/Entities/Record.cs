using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CovidSeva.Models.Entities
{
    [Table("record")]
    public class Record
    {
        [Key]
        public long Id { get; set; }
        public ServiceType ServiceType { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(15)]
        public string Contact { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }
        public int StateId { get; set; }
        [MaxLength(30)]
        public string StateName { get; set; }
        public int CityId { get; set; }
        [MaxLength(30)]
        public string CityName { get; set; }
        [MaxLength(300)]
        public string Remarks { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.UtcNow.AddMinutes(330);
    }
}