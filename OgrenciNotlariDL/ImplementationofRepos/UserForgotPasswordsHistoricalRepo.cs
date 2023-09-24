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
    public class UserForgotPasswordsHistoricalRepo : Repository<UserForgotPasswordsHistorical, int>, IUserForgotPasswordsHistoricalRepo
    {
        public UserForgotPasswordsHistoricalRepo(MyContext context) : base(context)
        {
        }
    }
}
