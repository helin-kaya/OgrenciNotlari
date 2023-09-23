using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OgrenciNotlariEL.Entities;

namespace OgrenciNotlariEL.IdentityModels
{
    public class AppUser : IdentityUser
    {
        [StringLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int LessonId { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
        //Gender
        //Height

    }
}
