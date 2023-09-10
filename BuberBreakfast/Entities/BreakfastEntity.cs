using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuberBreakfast.Entities
{
    public class BreakfastEntity
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        [NotMapped]
        public List<string> Savory { get; set; }
        [NotMapped]
        public List<string> Sweet { get; set; }
    }
}