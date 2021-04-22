using System;
using System.Collections.Generic;

namespace LabBook.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Permission { get; set; }
        public string Identifieri { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
        public string Password { get; }

        public UserDto(long id, string name, string surname, string email, string login, string permission, string identifieri, 
            bool active, DateTime date, string password)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Permission = permission;
            Identifieri = identifieri;
            Active = active;
            Date = date;
            Password = password;
        }

        public UserDto(string name, string surname, string email, string login, string permission, string identifieri, 
            bool active, DateTime date, string password)
        {
            Id = 0;
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Permission = permission;
            Identifieri = identifieri;
            Active = active;
            Date = date;
            Password = password;
        }

        public override bool Equals(object obj)
        {
            return obj is UserDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name &&
                   Surname == dto.Surname &&
                   Email == dto.Email &&
                   Login == dto.Login &&
                   Permission == dto.Permission &&
                   Identifieri == dto.Identifieri &&
                   Active == dto.Active &&
                   Date == dto.Date &&
                   Password == dto.Password;
        }

        public override int GetHashCode()
        {
            int hashCode = -1718557769;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Login);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Permission);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Identifieri);
            hashCode = hashCode * -1521134295 + Active.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            return hashCode;
        }
    }
}
