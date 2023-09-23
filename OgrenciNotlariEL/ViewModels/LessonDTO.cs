using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciNotlariEL.ViewModels
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public DateTime InsertedDate { get; set; }
        [StringLength(50, ErrorMessage = "Ders adı mak 50 karakter olmalıdır!")]
        [MinLength(3, ErrorMessage = "Ders adı min 3 karakter olmalıdır!")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

    }
}
