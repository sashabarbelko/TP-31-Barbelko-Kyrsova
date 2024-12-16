using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void DeleteUser(int userId);
        User GetUserByUsername(string Username);
        IEnumerable<User> GetAllUsers();
    }
}
