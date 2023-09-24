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
    public class UserForgotPasswordTokensRepo : Repository<UserForgotPasswordTokens, int>, IUserForgotPasswordTokensRepo
    {
        public UserForgotPasswordTokensRepo(MyContext context) : base(context)
        {
        }
    }
}
