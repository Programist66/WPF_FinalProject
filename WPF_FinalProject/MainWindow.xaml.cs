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
using System.Windows.Shapes;
using WPF_FinalProject.ViewModels;

namespace WPF_FinalProject
{
    public partial class MainWindow : Window
    {
        private MainVM mainVM;
        public MainWindow(User AuthorizationUser)
        {
            InitializeComponent();
            mainVM = new MainVM(this, AuthorizationUser);
            DataContext = mainVM;
        }
    }
}
