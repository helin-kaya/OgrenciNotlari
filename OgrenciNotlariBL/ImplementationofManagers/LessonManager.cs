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
    public class LessonManager : Manager<LessonDTO, Lesson, int>, ILessonManager
    {
        public LessonManager(ILessonRepo repo, IMapper mapper) : base(repo, mapper, null)
        {
        }
    }
}
