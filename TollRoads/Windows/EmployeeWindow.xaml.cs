using ModernWpf.Controls;
using TollRoads.Models;
using Wpf.Ui.Controls;

namespace TollRoads.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : UiWindow
    {
        public EmployeeWindow(Employee employee)
        {
            InitializeComponent();
            EmployeeDataPanel.DataContext = employee;

            //по началу планируется выводить пустую страницу,
            //на которой ещё не выбрана таблица для манипуляции
            //MainContent.Content = new EmptyPage();
        }

        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender,
            NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            if (item.Tag is Type pageType && typeof(System.Windows.Controls.Page).IsAssignableFrom(pageType))
            {
                MainContent.Content = (System.Windows.Controls.Page)Activator.CreateInstance(pageType);
            }
            else if (item.Tag != null)
            {
                AuthWindow window = new AuthWindow();
                window.Show();
                this.Close();
            }
        }
    }
}