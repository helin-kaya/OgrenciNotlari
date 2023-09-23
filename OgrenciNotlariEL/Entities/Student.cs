using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciNotlariEL.Entities
{
    [Table("STUDENT")]
    public class Student:BaseEntity
    {
        [StringLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [StringLength(50)]
        [MinLength(2)]
        public int Surname { get; set; }
    }
}
