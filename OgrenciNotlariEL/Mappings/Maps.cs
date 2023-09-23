using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OgrenciNotlariEL.Entities;
using OgrenciNotlariEL.ViewModels;

namespace OgrenciNotlariEL.Mappings
{
    public class Maps:Profile
    {
        public Maps() {
            CreateMap<Lesson, LessonDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<StudentLessonScore , StudentLessonScoreDTO>().ReverseMap();
            CreateMap<UserForgotPasswordsHistorical, UserForgotPasswordsHistoricalDTO>().ReverseMap();
            CreateMap<UserForgotPasswordTokens, UserForgotPasswordTokensDTO>().ReverseMap();
        }
    }
}
