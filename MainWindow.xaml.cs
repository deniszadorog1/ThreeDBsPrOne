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
            RemoveResume first = new RemoveResume(_main, TaskType.RemoveResume);
            first.ShowDialog();
        }

        private void SecondCommand_Click(object sender, RoutedEventArgs e)
        {
            RemoveResume second = new RemoveResume(_main, TaskType.RemoveHobbies);
            second.ShowDialog();
        }

        private void ThirdCommand_Click(object sender, RoutedEventArgs e)
        {
            RemoveResume third = new RemoveResume(_main, TaskType.RemovePlaces);
            third.ShowDialog();
        }

        private void FourtCommand_Click(object sender, RoutedEventArgs e)
        {
            RemoveResume forth = new RemoveResume(_main, TaskType.RemoveHobbieByCity);
            forth.ShowDialog();
        }

        private void FifthCommand_Click(object sender, RoutedEventArgs e)
        {
            (List<int> userIds, List<int> resumesIds) = _main.GetUserAndResumeIdsToDelete();

            SsmsUsage.RemoveUsersWitchWorkInOnePlace(userIds, resumesIds);

            MessageBox.Show("Done!");
        }
    }
}
