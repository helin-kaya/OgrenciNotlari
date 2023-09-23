using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgrenciNotlariEL.IdentityModels;

namespace OgrenciNotlariEL.Entities
{
    [Table("USERFORGOTPASSWORDSTOKENS")]
    public class UserForgotPasswordTokens : BaseEntity
    {
        public string UserId { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public DateTime? TokenUsedDate { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

    }
}
