using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDbsPrOne.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User()
        {
            Id = -1;
            Login = "";
            Password = "";
        }

        public object this[string name]
        {
            get
            {
                return typeof(User).GetProperties().First(x => x.Name.Equals(name));
            }
            set
            {
                if (typeof(User).GetProperties().Any(x => x.Name.Equals(name)))
                {
                    PropertyInfo propertInfo = typeof(User).GetProperty(name);
                    if (propertInfo != null && propertInfo.CanWrite)
                    {
                        propertInfo.SetValue(this, value);
                    }
                    return;
                }
                throw new ArgumentException("Incorrect property name!");
            }
        }

    }
}
