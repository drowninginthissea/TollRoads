using System.Windows;
using TollRoads.Models;
using TollRoads.Tools;
using Wpf.Ui.Controls;

namespace TollRoads.Windows.AddAndChangeWindows
{
    public partial class VehicleCategoryAddAndChange : UiWindow
    {
        //id изменяемой записи, если это изменение данных
        int id;

        //режим вызова окна (создание или изменение данных)
        bool _changeMode;
        public VehicleCategoryAddAndChange(bool changeMode, VehicleCategory category = null)
        {
            InitializeComponent();
            _changeMode = changeMode;
            if (changeMode)
            {
                id = category.Id;
                CategoryNameTextBox.Text = category.CategoryName;
                FaceCoefficientTextBox.Text = category.FaceCoefficient.ToString();
            }
        }

        //валидация вводимых данных
        private bool Validation()
        {
            MessageBoxManager manager = new MessageBoxManager();
            //проверка, что название введено
            if (CategoryNameTextBox.Text.Length == 0)
            {
                manager.Show("Ошибка сохранения", "Поля не заполнены");
                return false;
            }
            decimal coefficent;
            //проверка правильности ввода коэффициента
            if (!decimal.TryParse(FaceCoefficientTextBox.Text, out coefficent))
            {
                manager.Show("Ошибка сохранения", "Коэффициент записан не в виде числа");
                return false;
            }
            //проверка, что нововводимой записи ещё нет в бд
            if (DbUtils.db.VehicleCategories.ToList().Any(ce => ce.CategoryName == CategoryNameTextBox.Text &&
                ce.FaceCoefficient == coefficent &&
                ce.Id != id))
            {
                manager.Show("Ошибка сохранения", "Такая запись уже есть в базе");
                return false;
            }
            return true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validation())
                    return;
                VehicleCategory category;
                if (_changeMode)
                    category = DbUtils.db.VehicleCategories.FirstOrDefault(c => c.Id == id);
                else
                    category = new VehicleCategory();

                category.CategoryName = CategoryNameTextBox.Text;
                category.FaceCoefficient = decimal.Parse(FaceCoefficientTextBox.Text);

                if (!_changeMode)
                    DbUtils.db.VehicleCategories.Add(category);

                DbUtils.db.SaveChanges();
                Close();

            }
            catch (Exception ex)
            {
                MessageBoxManager manager = new MessageBoxManager();
                manager.Show("Ошибка сохранения", "Ошибка подключения к базе данных");
            }
        }
    }
}
