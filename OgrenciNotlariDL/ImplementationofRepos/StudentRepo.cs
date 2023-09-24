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
    public class StudentRepo : Repository<Student, int>,IStudentRepo
    {
        public StudentRepo(MyContext context) : base(context)
        {
        }
    }
}
