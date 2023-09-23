using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgrenciNotlariEL.Entities;

namespace OgrenciNotlariEL.ViewModels
{
    public class StudentLessonScoreDTO
    {
        public int Id { get; set; }
        public DateTime InsertedDate { get; set; }
        public int Score { get; set; }
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public  LessonDTO? Lesson { get; set; }

        public  StudentDTO? Student { get; set; }
    }
}
