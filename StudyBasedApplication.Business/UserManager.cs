using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Models;
using System.Web.Mvc;
using System.Data.Common;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Business
{
    internal class UserManager:IUserManager
    {

        IRepository<User> userrepo = GenericFactory<User>.GetInstance().GetObject();
        public User ValidateUser(Models.LoginModel loginModel)
        {
            try
            {
                var crypto = new SimpleCrypto.PBKDF2();
                User user = userrepo.Find(model =>model.LoginID == loginModel.LoginID);
                if (user != null)
                {
                    if (user.Password == crypto.Compute(loginModel.Password, user.PasswordSalt))
                        return user;
                    else
                        return null;
                   
                }


                return user;


            }
            catch (Exception e)
            {
                throw e;
            }
        }


        IRepository<UserGroup> UserGroupRepo = GenericFactory<UserGroup>.GetInstance().GetObject();
public IEnumerable<System.Web.Mvc.SelectListItem>  GetGroups()
{
    try
    {
        var groups = UserGroupRepo.All().ToList<UserGroup>();
        var Group = from c in groups

                    select new SelectListItem
                    {
                        Text = c.GroupName,

                        Value = c.GroupID.ToString()

                    };
        return Group;
    }
    catch (Exception e)
    {
        throw e;
    }

}


public int InsertUser(User model)
{
    try
    {
       
        userrepo.Create(model);
        userrepo.Save();
       User user= userrepo.Find(m => m.LoginID == model.LoginID);
       return user.UserID;

    }
    catch (Exception e)
    {
        throw e;
    }

}


public User GetUserByID(int id)
{
    try
    {
        var user = userrepo.Find(id);
        return user;
    }
    catch (Exception e)
    {
        return null;
    }
}
public List<User> GetAllUsers()
{
    try
    {
        var users = userrepo.Get(model => model.GroupID !=3).ToList();
        return users;

    }
    catch (Exception e)
    {
        return null;
    }

}
public bool UpdateProfile(User user)
{
    try
    {
        
        userrepo.Update(user);
        return true;
    }
    catch (Exception e)
    {
        throw e;
    }
}
    
public bool UpdateUser(User user)
{
    try
    {
        var crypto = new SimpleCrypto.PBKDF2();
        var encpassword = crypto.Compute(user.Password);
        user.Password = encpassword;
        user.PasswordSalt = crypto.Salt;
       
        userrepo.Update(user);
        return true;
    }
    catch (Exception e)
    {
        throw e;
    }
}
    }
    }


