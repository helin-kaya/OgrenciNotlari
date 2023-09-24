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
    public class StudentManager : Manager<StudentDTO, Student, int>,IStudentManager
    {
        public StudentManager(IStudentRepo repo, IMapper mapper) : base(repo, mapper,null)
        {
        }
    }
}
