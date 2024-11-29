using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_FinalProject.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PasswordBoxPlaceHolder.xaml
    /// </summary>
    public partial class PasswordBoxPlaceHolder : UserControl
    {
        public string Hint { get; set; }
        public Action TextChanget { get; set; }

        public string Password 
        {
            get {
                if (PwdIsHide)
                {
                    return pwd.Password;
                }
                else 
                {
                    return tb.Text;
                }
            }
        }

        public Brush HintBrush { get; set; }

        public ImageSource PasswordShowImg { get; set; }
        public ImageSource PasswordHideImg { get; set; }

        public bool PwdIsHide { get; set; } = true;

        public PasswordBoxPlaceHolder()
        {
            InitializeComponent();
            DataContext = this;
            ChechHint();
        }
        private void ChechHint() 
        {
            if (pwd.Password != "" || tb.Text != "")
            {
                hintBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                hintBlock.Visibility = Visibility.Visible;
            }
        }
        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChechHint();
            TextChanget?.Invoke(); 
        }

        private void pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ChechHint();
            TextChanget?.Invoke();
        }

        private void pwdImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PwdIsHide = !PwdIsHide;
            if (PwdIsHide)
            {
                pwdImage.Source = PasswordShowImg;
                pwd.Password = tb.Text;
                pwd.Visibility = Visibility.Visible;
                tb.Visibility = Visibility.Collapsed;
                ChechHint();
            }
            else
            {
                pwdImage.Source = PasswordHideImg;
                tb.Text = pwd.Password;
                tb.Visibility = Visibility.Visible;
                pwd.Visibility = Visibility.Collapsed;
                ChechHint();
            }
        }
    }
}
