using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OgrenciNotlariBL.InterfaceofManagers;
using OgrenciNotlariDL.InterfaceofRepos;
using OgrenciNotlariEL.Entities;
using OgrenciNotlariEL.ViewModels;

namespace OgrenciNotlariBL.ImplementationofManagers
{
    public class StudentLessonScoreManager : Manager<StudentLessonScoreDTO, StudentLessonScore, int>, IStudentLessonScoreManager
    {
        public StudentLessonScoreManager(IStudentLessonScoreRepo repo, IMapper mapper) : base(repo, mapper, new string[] { "Lesson","Student" })
        {
        }
    }
}
