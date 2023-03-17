using PEMS.BusinessEntities;
using PEMS.BusinessLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Account
{
    public class AccountService : IAccountService
    {
        public int RegisterUser(UserViewModel userModel)
        {
            int result = 0;
           using(PEMSDBEntities db = new PEMSDBEntities())
            {
                string encryptedPassword = EncodePasswordToBase64(userModel.Password);
                var user = new PEMS.BusinessEntities.User();
                user.UserName = userModel.UserName;
                user.Password = encryptedPassword;
                user.ConfirmPassword = encryptedPassword;
                user.Email = userModel.Email;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.RoleId = userModel.RoleId;
                db.Users.Add(user);
                result = db.SaveChanges();
            }
            return result;
        }
        public PEMS.BusinessEntities.User GetUser(string userName, string password)
        {
            string encryptedPassword = EncodePasswordToBase64(password);
            PEMS.BusinessEntities.User user = new PEMS.BusinessEntities.User();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                user = db.Users.Where(x => x.UserName == userName && x.Password == encryptedPassword).FirstOrDefault();
            }
            return user;
        }

        public List<UserViewModel> GetUserList()
        {
            List<UserViewModel> userList = new List<UserViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                userList = (from u in db.Users
                            join r in db.Roles on u.RoleId equals r.RoleId
                            select new UserViewModel
                            {
                                UserId = u.UserId,
                                LastName = u.LastName,
                                UserRole = r.UserRole,
                                Email = u.Email,
                                FullName = u.FirstName + " " + u.LastName,
                                UserName = u.UserName,
                            }).Distinct().ToList();
            }
            return userList;
        }
        //this function Convert to Encord your Password
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
