using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CovidSeva.Models.Entities
{
    [Table("record")]
    public class Record
    {
        private static readonly IReadOnlyDictionary<int, string> ResourceType = new Dictionary<int, string>
        {
            {(int)ServiceType.Oxygen, "Oxygen" },
            {(int)ServiceType.Plasma, "Plasma" },
            {(int)ServiceType.Medicine, "Medicines" },
        };

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

        [NotMapped]
        public string Data
        {
            get
            {
                var typeName = ResourceType[(int)ServiceType];
                var data = typeName + " Available - Contact: " + Contact;
                if (string.IsNullOrWhiteSpace(Name))
                {
                    data += " Provider Name: " + Name;
                }
                if (string.IsNullOrWhiteSpace(StateName))
                {
                    data += ". State Name: " + StateName;
                }
                if (string.IsNullOrWhiteSpace(CityName))
                {
                    data += ". City Name: " + CityName;
                }
                if (string.IsNullOrWhiteSpace(Address))
                {
                    data += ". Address: " + Address;
                }
                if (string.IsNullOrWhiteSpace(Remarks))
                {
                    data += ". Additional Info: " + Remarks;
                }
                return data;
            }
        }
    }
}