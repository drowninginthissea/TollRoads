using System.Windows;
using System.Windows.Controls;
using TollRoads.Models;
using TollRoads.Tools;
using TollRoads.Windows.AddAndChangeWindows;

namespace TollRoads.Windows.Pages
{
    public partial class VehicleCategoryPage : Page
    {
        public VehicleCategoryPage()
        {
            InitializeComponent();

            //выгрузка данных из БД
            VehicleCategoryDataGrid.ItemsSource = DbUtils.db.VehicleCategories.ToList();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //выгрузка данных из БД с поиском по атрибутам таблицы
            VehicleCategoryDataGrid.ItemsSource = DbUtils.db.VehicleCategories.ToList()
                .Where(vc => vc.CategoryName.Contains(SearchTextBox.Text) ||
                vc.FaceCoefficient.ToString().Contains(SearchTextBox.Text));
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            VehicleCategoryDataGrid.ItemsSource = DbUtils.db.VehicleCategories.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //вызов окна для добавления нового элеметна в таблицу
            new VehicleCategoryAddAndChange(false).ShowDialog();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            //выбор конкретно взятого элемента 
            var category = (sender as Button).DataContext as VehicleCategory;

            //вызов окна для изменения существующего элемента таблицы
            new VehicleCategoryAddAndChange(true, category).ShowDialog();
        } 
    }
}
