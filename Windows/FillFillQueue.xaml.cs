using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using ThreeDbsPrOne.Models.Enums;
using ThreeDbsPrOne.Models;


using ThreeDbsPrOne.DBs;
using ThreeDbsPrOne.Models;
using ThreeDbsPrOne.Models.Enums;


namespace ThreeDbsPrOne.Windows
{
    /// <summary>
    /// Логика взаимодействия для FillFillQueue.xaml
    /// </summary>
    public partial class FillFillQueue : Window
    {
        private MainClass _main;
        private TaskType _type;

        public FillFillQueue()
        {
            InitializeComponent();
        }
        public FillFillQueue(MainClass main, TaskType type)
        {
            InitializeComponent();

            _type = type;
            _main = main;

            FillLBs();
            FillResumes();
        }
        private void FillLBs()
        {
            switch (_type)
            {
                case TaskType.ShowResume:
                    {
                        TaskCommand.Content = "Перший Запит";
                        TaskBut.Content = "Показати";
                        break;
                    }
                case TaskType.ShowHobbies:
                    {
                        TaskCommand.Content = "Другий Запит";
                        TaskBut.Content = "Показати хобі";
                        break;
                    }
                case TaskType.ShowPlaces:
                    {
                        TaskCommand.Content = "Третій Запит";
                        TaskBut.Content = "Показати місця";
                        break;
                    }
                case TaskType.ShowHobbieByCity:
                    {
                        TaskCommand.Content = "Четвертий Запит";
                        TaskBut.Content = "Показати хобі";
                        break;
                    }
                case TaskType.ShowUserWithOneWorkPlace:
                    {
                        TaskCommand.Content = "П'ятий Запит";
                        TaskBut.Content = "Показати юзерів";
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
            if (_type == TaskType.ShowHobbieByCity)
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

            if (_type == TaskType.ShowHobbieByCity)
            {
                city = _main.GetSityByString(((Label)Resumes.SelectedItem).Content.ToString());
            }
            else resume = _main.Resumes[Resumes.SelectedIndex];
            if (_type == TaskType.ShowResume)
            {
                Resume res = SsmsUsage.GetResumeById(resume.Id);
                ShowInfo show = new ShowInfo(res, _type);
                show.ShowDialog();
                //Resumes.Items.RemoveAt(Resumes.SelectedIndex);
            }
            else if (_type == TaskType.ShowHobbies)
            {
                //SsmsUsage.RemoveHobbiesByResumeId(resume.Id);

                string res = SsmsUsage.GetHobbiesResumeById(resume.Id);
                ShowInfo show = new ShowInfo(res);
                show.ShowDialog();
            }
            else if (_type == TaskType.ShowPlaces)
            {
                //SsmsUsage.RemovePlacesByResumeId(resume.Id);

                string cities = SsmsUsage.GetCitiesResumeById(resume.Id);
                ShowInfo show = new ShowInfo(cities);
                show.ShowDialog();

            }
            else if (_type == TaskType.ShowHobbieByCity)
            {
                //SsmsUsage.RemoveHobbiesWithChosenCity(city);

                string hobbies = SsmsUsage.GetHobbieStringWithChosenCity(city);
                ShowInfo show = new ShowInfo(hobbies);
                show.ShowDialog();
            }
            else if (_type == TaskType.ShowUserWithOneWorkPlace)
            {

            }
            //MessageBox.Show("Done!");
        }

    }
}
