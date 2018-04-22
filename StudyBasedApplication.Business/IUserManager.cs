using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;
using System.Web.Mvc;

namespace StudyBasedApplication.Business
{
    public interface IUserManager
    {
        User ValidateUser(LoginModel loginModel);
       
        IEnumerable<SelectListItem> GetGroups();
        int InsertUser(User model);
        User GetUserByID(int id);
        List<User> GetAllUsers();
        bool UpdateUser(User user);
        bool UpdateProfile(User user);
       

    }
}
