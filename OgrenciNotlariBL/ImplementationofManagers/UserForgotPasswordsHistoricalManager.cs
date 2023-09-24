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
    public class UserForgotPasswordsHistoricalManager :
        Manager<UserForgotPasswordsHistoricalDTO, UserForgotPasswordsHistorical, int>, IUserForgotPasswordsHistoricalManager
    {
        public UserForgotPasswordsHistoricalManager(IUserForgotPasswordsHistoricalRepo repo, IMapper mapper) : base(repo, mapper, new string[] { "User" })
        {

        }
    }
}
