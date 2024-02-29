using System.Windows;
using System.Windows.Controls;
using TollRoads.Models;
using TollRoads.Tools;
using TollRoads.Windows.AddAndChangeWindows;

namespace TollRoads.Windows.Pages
{
    public partial class GenderPage : Page
    {
        public GenderPage()
        {
            InitializeComponent();
            GenderDataGrid.ItemsSource = DbUtils.db.Genders.ToList();
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            GenderDataGrid.ItemsSource = DbUtils.db.Genders.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            GenderAddAndChange window = new GenderAddAndChange(false);
            window.ShowDialog();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            Gender gender = (sender as Button).DataContext as Gender;
            if (gender != null)
            {
                GenderAddAndChange window = new GenderAddAndChange(true, gender);
                window.ShowDialog();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GenderDataGrid.ItemsSource = DbUtils.db.Genders.ToList()
                .Where(g => g.GenderName.Contains(SearchTextBox.Text));
        }
    }
}
