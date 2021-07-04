using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Security
{
    public sealed class UserSingleton
    {
        private static volatile UserSingleton _instance = null;
        private static object _syncRoot = new object();
        private static long _id = 0;
        private static string _name;
        private static string _surname;
        private static string _email;
        private static string _login;
        private static string _permission;
        private static string _identifier;
        private static bool _isActive;

        private UserSingleton(long id, string name, string surname, string email, string login, string permission, string identifier, bool isActive)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _email = email;
            _login = login;
            _permission = permission;
            _identifier = identifier;
            _isActive = isActive;
        }

        public static UserSingleton CreateInstance(long id, string name, string surname, string email, string login, string permission, string identifier, bool isActive)
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new UserSingleton(id, name, surname, email, login, permission, identifier, isActive);
                }
            }

            return _instance;
        }

        public static UserSingleton GetInstance
        {
            get
            {
                return _instance;
            }
        }

        public static long Id
        {
            get
            {
                return _id;
            }
        }

        public static string Name
        {
            get
            {
                return _name;
            }
        }

        public static string SurName
        {
            get
            {
                return _surname;
            }
        }

        public static string Email
        {
            get
            {
                return _email;
            }
        }

        public static string Login
        {
            get
            {
                return _login;
            }
        }

        public static string Permission
        {
            get
            {
                return _permission;
            }
        }

        public static string Identifier
        {
            get
            {
                return _identifier;
            }
        }

        public static bool IsActive
        {
            get
            {
                return _isActive;
            }
        }
    }
}
