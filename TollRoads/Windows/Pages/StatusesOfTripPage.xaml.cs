using System.Windows;
using System.Windows.Controls;
using TollRoads.Models;
using TollRoads.Tools;
using TollRoads.Windows.AddAndChangeWindows;

namespace TollRoads.Windows.Pages
{
    public partial class StatusesOfTripPage : Page
    {
        public StatusesOfTripPage()
        {
            InitializeComponent();
            StatusesDataGrid.ItemsSource = DbUtils.db.StatusesOfTrips.ToList();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StatusesDataGrid.ItemsSource = DbUtils.db.StatusesOfTrips.ToList()
                .Where(s => s.StatusName.Contains(SearchTextBox.Text));
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            StatusesDataGrid.ItemsSource = DbUtils.db.StatusesOfTrips.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new StatusesOfTripAddAndChange(false).ShowDialog();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var status = (sender as Button).DataContext as StatusesOfTrip;
            new StatusesOfTripAddAndChange(true, status).ShowDialog();
        }
    }
}
