using System.Windows;
using System.Windows.Controls;
using TollRoads.Models;
using TollRoads.Tools;
using TollRoads.Windows.AddAndChangeWindows;

namespace TollRoads.Windows.Pages
{
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            UsersOutput.ItemsSource = DbUtils.db.Employees.ToList();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeAddAndChange(false).ShowDialog();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UsersOutput.ItemsSource = DbUtils.db.Employees.ToList();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var employee = (sender as Button).DataContext as Employee;
            new EmployeeAddAndChange(true, employee).ShowDialog();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsersOutput.ItemsSource = DbUtils.db.Employees.ToList()
                .Where(u => $"{u.Surname} {u.Name} {(u.Patronymic == null ? "" : u.Patronymic)}".Contains(SearchTextBox.Text) ||
                    (u.IdCheckpoint == null ? "" : u.IdCheckpoint.ToString()).Contains(SearchTextBox.Text) ||
                    (u.IdCheckpoint == null ? "" : u.IdCheckpointNavigation.Address).Contains(SearchTextBox.Text) ||
                    u.Login.Contains(SearchTextBox.Text) ||
                    u.Password.Contains(SearchTextBox.Text) ||
                    u.PhoneNumber.Contains(SearchTextBox.Text) ||
                    u.IdRoleNavigation.RoleName.Contains(SearchTextBox.Text) ||
                    u.IdGenderNavigation.GenderName.Contains(SearchTextBox.Text));
        }
    }
}
