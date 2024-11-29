using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для TextBoxPlaceHolder.xaml
    /// </summary>
    public partial class TextBoxPlaceHolder : UserControl
    {

        public string Hint { get; set; }
        public Brush HintBrush { get; set; }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                //tb.Text = value;
                SetValue(TextProperty, value);                
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxPlaceHolder),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnTextChanged));

        private static void OnTextChanged (DependencyObject d, DependencyPropertyChangedEventArgs e) {
            TextBoxPlaceHolder textBox = (TextBoxPlaceHolder) d;
            textBox.TextChanget?.Invoke ();
        }


        //private string text 
        //{
        //    get => tb.Text;
        //    set 
        //    { 
        //        tb.Text = value;
        //    } 
        //}
        public Action? TextChanget { get; set; }

        public TextBoxPlaceHolder()
        {
            InitializeComponent();
            // DataContext = this;
            grid.DataContext = this;
        }

        //private void tb_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextChanget?.Invoke();
        //    Text = tb.Text;
        //}
    }
}
