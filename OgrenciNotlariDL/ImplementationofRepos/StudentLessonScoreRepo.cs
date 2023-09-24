using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgrenciNotlariDL.ContextInfo;
using OgrenciNotlariDL.InterfaceofRepos;
using OgrenciNotlariEL.Entities;

namespace OgrenciNotlariDL.ImplementationofRepos
{
    public class StudentLessonScoreRepo : Repository<StudentLessonScore , int>, IStudentLessonScoreRepo
    {
        public StudentLessonScoreRepo(MyContext context) : base(context)
        {
        }
    }
}
