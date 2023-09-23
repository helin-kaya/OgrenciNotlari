using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgrenciNotlariEL.IdentityModels;

namespace OgrenciNotlariEL.Entities
{
    public class UserForgotPasswordsHistorical : BaseEntity
    {
        public string UserId { get; set; }
        public string Password { get; set; } // şimdilik hashlemeyeceğim! Sonra bu hashlenme mekanizması düşünülmeli...
        public bool IsDeleted { get; set; }


        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

    }
}
