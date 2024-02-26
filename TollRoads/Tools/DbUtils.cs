using TollRoads.Models;
using Wpf.Ui.Controls;

namespace TollRoads.Tools
{
    static class DbUtils
    {
        public static Db db;

        static DbUtils()
        {
            try
            {
                db = new Db();
            }
            catch (Exception)
            {
                MessageBox box = new MessageBox();
                box.Title = "Ошибка";
                box.Content = "Ошибка подключения к базе";
                box.Show();
            }
        }
    }
}