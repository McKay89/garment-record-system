using System.Windows;
using System.Windows.Input;

namespace GarmentRecordSystemWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                
                e.Handled = true;
            }
        }

        private static bool IsNumeric(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}