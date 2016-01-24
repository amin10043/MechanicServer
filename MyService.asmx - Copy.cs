using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SQLite;
using System.Data;

namespace Service
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MyService : System.Web.Services.WebService
    {
        DataClasses1DataContext context;
        List<food> allFood;
        User user;
   
        #region constractur

        public MyService()
        {
            context = new DataClasses1DataContext();
            allFood = new List<food>();
            user = new User();
        }

        #endregion

        #region athentication 
        
        [WebMethod]
        public string connect(string imei)
        {

            var user = context.Users.Where<User>(x => x.Code == imei);
            if (user != null && user.Count() > 0)
            {
                User us = user.First<User>();
                if (us != null)
                    return us.Id.ToString();
                else
                    return "";
            }

            return "";
            
        }

        [WebMethod]
        public bool login(string username, string password)
        {
           user =  context.Users.Where<User>(x=> x.Email == username && x.Password == password).First();
            if(user != null)
                return true;
            else
                return false;
        }

        [WebMethod]
        public bool register(string username, string password, string address, string phone, string imei)
        {
            try
            {
                User user = new User()
                {
                    Email = username,
                    Password = password,
                    Address = address,
                    Phone = phone,
                    Code = imei
                };

                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [WebMethod]
        public bool recoverPassword(string email)
        {
            try
            {
                User user = context.Users.Where(x => x.Email == email).First<User>();
                string pass = user.Password;
                // email it to user
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [WebMethod]
        public bool loginViaGoogle()
        {
            return false;
        }

        [WebMethod]
        public bool loginViaFacebook()
        {
            return false;
        }
        
        #endregion

    }

}