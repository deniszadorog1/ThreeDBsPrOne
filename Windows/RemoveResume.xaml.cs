using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


using ThreeDbsPrOne.DBs;
using ThreeDbsPrOne.Models;
using ThreeDbsPrOne.Models.Enums;

namespace ThreeDbsPrOne.Windows
{
    /// <summary>
    /// Логика взаимодействия для RemoveResume.xaml
    /// </summary>
    public partial class RemoveResume : Window
    {
        private MainClass _main;
        private TaskType _type;
        public RemoveResume(MainClass main, TaskType type)
        {
            InitializeComponent();

            _main = main;
            _type = type;

            FillResumes();
            FillLBs();
        }
        private void FillLBs()
        {
            switch (_type)
            {
                case TaskType.RemoveResume:
                    {
                        TaskCommand.Content = "Перший Запит";
                        TaskBut.Content = "Видалити";
                        break;
                    }
                case TaskType.RemoveHobbies:
                    {
                        TaskCommand.Content = "Другий Запит";
                        TaskBut.Content = "Видалити хобі";
                        break;
                    }
                case TaskType.RemovePlaces:
                    {
                        TaskCommand.Content = "Третій Запит";
                        TaskBut.Content = "Видалити місця";
                        break;
                    }
                case TaskType.RemoveHobbieByCity:
                    {
                        TaskCommand.Content = "Четвертий Запит";
                        TaskBut.Content = "Видалити хобі";
                        break;
                    }
                case TaskType.RemoveUserWithOneWorkPlace:
                    {
                        TaskCommand.Content = "П'ятий Запит";
                        TaskBut.Content = "Видалити юзерів";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void FillResumes()
        {
            if(_type == TaskType.RemoveHobbieByCity)
            {
                for (int i = (int)City.Paris; i <= (int)City.Toronto; i++)
                {
                    Label lb = new Label();
                    lb.Content = ((City)i).ToString();
                    Resumes.Items.Add(lb);
                }
                return;
            }

            for (int i = 0; i < _main.Resumes.Count; i++)
            {
                Label lb = new Label();
                lb.Content = _main.Resumes[i].Name;

                Resumes.Items.Add(lb);
            }
        }
        private void TaskBut_Click(object sender, RoutedEventArgs e)
        {
            Resume resume = null;
            City city = City.Toronto;

            if (_type == TaskType.RemoveHobbieByCity)
            {
                city = _main.GetSityByString(((Label)Resumes.SelectedItem).Content.ToString());
            }
            else resume = _main.Resumes[Resumes.SelectedIndex];          

            if (_type == TaskType.RemoveResume)
            {
                SsmsUsage.RemoveResume(resume.Id);
                Resumes.Items.RemoveAt(Resumes.SelectedIndex);
            }
            else if (_type == TaskType.RemoveHobbies)
            {
                SsmsUsage.RemoveHobbiesByResumeId(resume.Id);
            }
            else if (_type == TaskType.RemovePlaces)
            {
                SsmsUsage.RemovePlacesByResumeId(resume.Id);
            }
            else if (_type == TaskType.RemoveHobbieByCity)
            {
                SsmsUsage.RemoveHobbiesWithChosenCity(city);
            }
            else if(_type == TaskType.RemoveUserWithOneWorkPlace)
            {

            }
            MessageBox.Show("Done!");
        }
        




    }
}
