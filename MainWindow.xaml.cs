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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThreeDbsPrOne.DBs;
using ThreeDbsPrOne.Models;
using ThreeDbsPrOne.Models.Enums;
using ThreeDbsPrOne.Windows;
namespace ThreeDbsPrOne
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainClass _main;
        public MainWindow()
        {
            InitializeComponent();

            _main = new MainClass();
        }
        private void FirstQueue_Click(object sender, RoutedEventArgs e)
        {
            FillFillQueue first = new FillFillQueue(_main, TaskType.ShowResume);
            first.ShowDialog();
        }
        private void SecondCommand_Click(object sender, RoutedEventArgs e)
        {
            FillFillQueue second = new FillFillQueue(_main, TaskType.ShowHobbies);
            second.ShowDialog();
        }
        private void ThirdCommand_Click(object sender, RoutedEventArgs e)
        {
            FillFillQueue third = new FillFillQueue(_main, TaskType.ShowPlaces);
            third.ShowDialog();
        }
        private void FourtCommand_Click(object sender, RoutedEventArgs e)
        {
            FillFillQueue forth = new FillFillQueue(_main, TaskType.ShowHobbieByCity);
            forth.ShowDialog();
        }

        private void FifthCommand_Click(object sender, RoutedEventArgs e)
        {
            /*            (List<int> userIds, List<int> resumesIds) = _main.GetUserAndResumeIdsToDelete();
                        SsmsUsage.RemoveUsersWitchWorkInOnePlace(userIds, resumesIds);
            */

            string res = SsmsUsage.GetFifthTaskString();

            ShowInfo info = new ShowInfo(res);
            info.ShowDialog();

        }
    }
}
