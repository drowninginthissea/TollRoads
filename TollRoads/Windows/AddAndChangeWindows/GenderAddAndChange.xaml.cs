using System.Windows;
using TollRoads.Models;
using TollRoads.Tools;
using Wpf.Ui.Controls;

namespace TollRoads.Windows.AddAndChangeWindows
{
    public partial class GenderAddAndChange : UiWindow
    {
        int id;

        bool _changeMode;
        public GenderAddAndChange(bool changeMode, Gender gender = null)
        {
            InitializeComponent();
            _changeMode = changeMode;
            if (changeMode)
            {
                id = gender.Id;
                GenderNameTextBox.Text = gender.GenderName;
            }
        }

        private bool Validation()
        {
            MessageBoxManager manager = new MessageBoxManager();
            if (GenderNameTextBox.Text.Length == 0)
            {
                manager.Show("Ошибка сохранения", "Поля не заполнены");
                return false;
            }
            if (DbUtils.db.Genders.ToList().Any(ce => ce.GenderName == GenderNameTextBox.Text && ce.Id != id))
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
                Gender gender;
                if (_changeMode)
                    gender = DbUtils.db.Genders.FirstOrDefault(c => c.Id == id);
                else
                    gender = new Gender();

                gender.GenderName = GenderNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.Genders.Add(gender);

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
