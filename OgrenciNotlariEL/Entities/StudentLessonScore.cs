using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciNotlariEL.Entities
{
    [Table("STUDENTLESSONSCORE")]
    public class StudentLessonScore:BaseEntity
    {
        public int Score { get; set; }
        public int LessonId { get; set; }
        public int StudentId { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

    }
}
