using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OgrenciNotlariEL.IdentityModels
{
    public class AppRole : IdentityRole
    {
        [StringLength(400)]
        public string? Description { get; set; }
        public DateTime InsertedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
