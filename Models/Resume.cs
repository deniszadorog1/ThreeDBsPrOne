using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ThreeDbsPrOne.Models.Enums;
namespace ThreeDbsPrOne.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LivingPlace { get; set; }
        public List<Hobbie> Hobbies { get; set; }
        public List<Institution> Institutions { get; set; }
        public List<City> Cities { get; set; }


        public Resume()
        {
            Id = -1;
            UserId = -1;
            Name = "";
            Surname = "";
            LivingPlace = "";
            LivingPlace = "";
            Hobbies = new List<Hobbie>();
            Institutions = new List<Institution>();
            Cities = new List<City>();
        }

        public object this[string name]
        {
            get
            {
                return typeof(Resume).GetProperties().First(x => x.Name.Equals(name));
            }
            set
            {
                PropertyInfo[] asd =  typeof(Resume).GetProperties();

                for(int i = 0; i < asd.Length; i++)
                {
                    string qe = asd[i].Name;
                }

                if (typeof(Resume).GetProperties().Any(x => x.Name.Equals(name)))
                {
                    PropertyInfo propertInfo = typeof(Resume).GetProperty(name);
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
