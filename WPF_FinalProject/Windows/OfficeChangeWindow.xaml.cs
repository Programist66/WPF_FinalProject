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

namespace WPF_FinalProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для OfficeChangeWindow.xaml
    /// </summary>
    public partial class OfficeChangeWindow : Window
    {
        public OfficeChangeWindow(OfficeVM office)
        {
            InitializeComponent();
            DataContext = office;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
