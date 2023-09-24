using System.ComponentModel.DataAnnotations;
using OgrenciNotlariEL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciNotlariPL.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int LessonId { get; set; }
        public  Lesson? Lesson { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        //Abdullah teşekkür ederiz
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Parolalar en az 8 karakter olmalı ve aşağıdakilerden 4'ünden 3'ünü içermelidir: büyük harf ( A-Z ), küçük harf ( a-z ), sayı ( 0-9 ) ve özel karakter ( ör. !@#$% ^ & * )")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string PasswordConfirm { get; set; }

        public string Username { get; set; }
    }
}
