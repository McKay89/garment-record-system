using GarmentRecordSystemWPF.JsonController;
using GarmentRecordSystemWPF.Model;
using System.Windows;
using System.Windows.Input;

namespace GarmentRecordSystemWPF
{
    public partial class MainWindow : Window
    {
        private List<GarmentJson>? garmentJsons;

        public MainWindow()
        {
            InitializeComponent();

            var garmentsJson = JsonFileController.Read();
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            garmentJsons = JsonFileController.Read();

            if (garmentJsons != null)
            {
                ActivateButtons();
                ListGarments();
            } else
            {
                // TODO: Handle error
            }
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

        private void ActivateButtons()
        {
            addBtn.IsEnabled = true;
            searchInput.IsEnabled = true;
            sortInput.IsEnabled = true;
        }

        private void ListGarments()
        {            
            garmentsList.ItemsSource = garmentJsons;
        }
    }
}