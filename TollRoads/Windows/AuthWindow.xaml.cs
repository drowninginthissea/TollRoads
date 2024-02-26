using System.Windows;
using System.Windows.Controls;
using TollRoads.Tools;
using Wpf.Ui.Animations;
using Wpf.Ui.Controls;

namespace TollRoads.Windows
{
    public partial class AuthWindow : UiWindow
    {
        int attemptCount;

        string answerForCaptcha;
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) //анимированная загрузка окна
        {
            Transitions.ApplyTransition(this, TransitionType.FadeInWithSlide, 1000);
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e) 
        {
            if (DbUtils.db.Employees.Any(u =>
                u.Login == LoginTextBox.Text && u.Password == PasswordBox.Password)) //проверка, есть ли среди пользователей
                // такой пользователь, у которого подходил логин и пароль
            {
                var employee = DbUtils.db.Employees.Single(u =>
                    u.Login == LoginTextBox.Text && u.Password == PasswordBox.Password); //выбор конкретного пользователя по
                // логину и паролю

                new EmployeeWindow(employee).Show();
                Close();
            }
            else
            {
                attemptCount++;
                if (attemptCount == 3)
                {
                    GenerateCaptcha();
                    CaptchaDialog.Show();
                }
                else
                {
                    new MessageBoxManager().Show("Ошибка авторизации",
                        "Логин или пароль введены не верно.\nПроверьте введённые данные");
                }
            }
        }

        private void GenerateCaptcha()
        {
            CaptchaDialog.Show();
            Captcha.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 6);
            answerForCaptcha = Captcha.CaptchaText;
            AnswwerTextBox.Text = string.Empty;
        }

        private void CaptchaDialog_ButtonLeftClick(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }

        private void CaptchaDialog_ButtonRightClick(object sender, RoutedEventArgs e)
        {
            if (AnswwerTextBox.Text == answerForCaptcha)
            {
                CaptchaDialog.Hide();
                attemptCount = 0;
            }
            else
                GenerateCaptcha();
        }
    }
}
