using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ThreeDbsPrOne.DBs;
using ThreeDbsPrOne.Models;
using ThreeDbsPrOne.Models.Enums;

namespace ThreeDbsPrOne.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShowInfo.xaml
    /// </summary>
    public partial class ShowInfo : Window
    {
        private Resume _resume;
        private TaskType _type;
        public ShowInfo(Resume resume, TaskType type)
        {
            _resume = resume;
            _type = type;

            InitializeComponent();

            FillLabel();
        }
        public ShowInfo(string str)
        {
            InitializeComponent();

            ResLB.Content = str;
        }
        private void FillLabel()
        {
            switch (_type)
            {
                case TaskType.ShowResume:
                    {
                        string resumeInString = FillResumeInLabel(_resume);
                        ResLB.Content = resumeInString;
                        break;
                    }
                case TaskType.ShowHobbies:
                    {
                        string hobbies = FillResumeHobbies(_resume);
                        ResLB.Content = hobbies;

                        break;
                    }
                case TaskType.ShowPlaces:
                    {
                        break;
                    }
                case TaskType.ShowHobbieByCity:
                    {
                        break;
                    }
                case TaskType.ShowUserWithOneWorkPlace:
                    {
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }
        private string FillResumeInLabel(Resume resume)
        {
            string res = "";
            res += $"Resume Id -  {resume.Id.ToString()}" + "\n";
            res += $"Resume Name -  {resume.Name.ToString()}" + "\n";
            res += $"Resume Surname -  {resume.Surname.ToString()}" + "\n";
            res += $"Resume LivingPlace -  {resume.LivingPlace.ToString()}" + "\n";
            res += FillResumeHobbies(resume);
            res += FillResumeInstitution(resume);
            res += FillResumeCity(resume);

            return res;
        }
        private string FillResumeHobbies(Resume resume)
        {
            string res = "Hobbies: \n";
            for(int i = 0; i < resume.Hobbies.Count; i++)
            {
                res += resume.Hobbies[i].ToString() + ", ";
            }
            res += "\n";
            return res;
        }
        private string FillResumeInstitution(Resume resume)
        {
            string res = "Institution:  \n";
            for (int i = 0; i < resume.Institutions.Count; i++)
            {
                res += resume.Institutions[i].ToString() + ", ";
            }
            res += "\n";
            return res;
        }
        private string FillResumeCity(Resume resume)
        {
            string res = "Cities:  \n";
            for (int i = 0; i < resume.Cities.Count; i++)
            {
                res += resume.Cities[i].ToString() + ", ";
            }
            res += "\n";
            return res;
        }

        /*
                 public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LivingPlace { get; set; }
        public List<Hobbie> Hobbies { get; set; }
        public List<Institution> Institutions { get; set; }
        public List<City> Cities { get; set; }
         */
    }
}
