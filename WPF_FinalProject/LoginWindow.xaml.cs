using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_FinalProject.HelperClasses;

namespace WPF_FinalProject
{
    public class LoginVM {
        private string userName = "";
        public string UserName {
            get => userName;
            set => userName = value;
        }
    }

    public partial class LoginWindow : Window
    {
        private Brush normalBrush = new SolidColorBrush(Color.FromRgb(0, 255, 255));
        private Brush errorBrush = new SolidColorBrush(Color.FromRgb(212,0,11));
        private Authorization authorization = new Authorization();

        public LoginVM LoginVM { get; } = new LoginVM ();

        public LoginWindow()
        {
            InitializeComponent();
            pwdBox.TextChanget += ResetPasswordBorder;
            loginBox.TextChanget += ResetLoginBorder;

            this.DataContext = LoginVM;
        }

        private void ButtonMinimaze_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ButtonAuthrorization_Click(object sender, RoutedEventArgs e)
        {
            string userName = LoginVM.UserName;
            if (pwdBox.Password == "" || loginBox.Text == "")
            {
                if (pwdBox.Password == "")
                {
                    PasswordBoxError("Поле не может быть пустым");
                }
                if (loginBox.Text == "")
                {
                    LoginBoxError("Поле не может быть пустым");
                }
                return;
            }
            User? tmp = authorization.GetUser(loginBox.Text, pwdBox.Password);
            if (tmp is null)
            {
                PasswordBoxError("Неверный логин или пароль");
                LoginBoxError("Неверный логин или пароль");
                return;
            }
            MainWindow mainWindow = new MainWindow(tmp);
            mainWindow.Show();
            this.Hide();
        }

        private void PasswordBoxError(string errorText) 
        {
            passwordError.Visibility = Visibility.Visible;
            pwdBox.BorderBrush = errorBrush;
            passwordError.Text = errorText;
        }

        private void LoginBoxError(string errorText)
        {
            loginError.Visibility = Visibility.Visible;
            loginBox.BorderBrush = errorBrush;
            loginError.Text = errorText;
        }

        private void ResetPasswordBorder() 
        {
            passwordError.Visibility = Visibility.Hidden;
            pwdBox.BorderBrush = normalBrush;
        }
        private void ResetLoginBorder()
        {
            loginError.Visibility = Visibility.Hidden;
            loginBox.BorderBrush = normalBrush;
        }
    }
}