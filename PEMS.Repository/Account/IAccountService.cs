using PEMS.BusinessEntities;
using PEMS.BusinessLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Account
{
    public interface IAccountService
    {
        int RegisterUser(UserViewModel user);
        PEMS.BusinessEntities.User GetUser(string userName, string password);
        List<UserViewModel> GetUserList();
    }
}
