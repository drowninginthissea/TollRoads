using System.Windows;
using System.Windows.Controls;
using TollRoads.Models;
using TollRoads.Tools;
using TollRoads.Windows.AddAndChangeWindows;

namespace TollRoads.Windows.Pages
{
    public partial class RolesPage : Page
    {
        public RolesPage()
        {
            InitializeComponent();
            RoleDataGrid.ItemsSource = DbUtils.db.Roles.ToList();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RoleDataGrid.ItemsSource = DbUtils.db.Roles.ToList()
                .Where(r => r.RoleName.Contains(SearchTextBox.Text)).ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RoleAddAndChange window = new RoleAddAndChange(false);
            window.ShowDialog();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var role = (sender as Button).DataContext as Role;
            var window = new RoleAddAndChange(true, role);
            window.ShowDialog();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RoleDataGrid.ItemsSource = DbUtils.db.Roles.ToList();
        }
    }
}
