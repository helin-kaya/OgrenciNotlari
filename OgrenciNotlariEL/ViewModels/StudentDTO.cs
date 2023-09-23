using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciNotlariEL.ViewModels
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public DateTime InsertedDate { get; set; }
        [StringLength(50,ErrorMessage="Öğrenci adı max 50 karakter olmalı")]
        [MinLength(2, ErrorMessage = "Öğrenci adı min 2 karakter olmalı")]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage = "Öğrenci soyadı max 50 karakter olmalı")]
        [MinLength(2, ErrorMessage = "Öğrenci soyadı min 2 karakter olmalı")]
        public int Surname { get; set; }
    }
}
