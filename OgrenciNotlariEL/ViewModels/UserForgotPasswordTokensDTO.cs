using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgrenciNotlariEL.IdentityModels;

namespace OgrenciNotlariEL.ViewModels
{
    public class UserForgotPasswordTokensDTO
    {
        public int Id { get; set; }
        public DateTime InsertedDate { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public DateTime? TokenUsedDate { get; set; }

        public AppUser? User { get; set; }

    }
}
