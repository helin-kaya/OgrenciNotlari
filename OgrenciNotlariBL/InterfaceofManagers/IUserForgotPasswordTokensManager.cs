using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgrenciNotlariEL.ViewModels;

namespace OgrenciNotlariBL.InterfaceofManagers
{
    public interface IUserForgotPasswordTokensManager
         : IManager<UserForgotPasswordTokensDTO, int>
    {
        public bool AddNewForgotPasswordToken(UserForgotPasswordTokensDTO userToken);
    }
}
