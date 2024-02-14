using GarmentRecordSystemWPF.JsonController;
using GarmentRecordSystemWPF.Model;
using GarmentRecordSystemWPF.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GarmentRecordSystemWPF
{
    public partial class MainWindow : Window
    {
        private List<GarmentJson> garmentJson;
        private IGarmentService? garmentService;

        public MainWindow()
        {
            InitializeComponent();

            garmentJson = JsonFileController.Read();
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            // Load JSON content
            garmentJson = JsonFileController.Read();

            if (garmentJson != null)
            {
                CreateGarmentService();
                ActivateButtons();
                ListGarments("");
            } else
            {
                // TODO: Handle error
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Exit application
            Environment.Exit(0);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Search input field only accept numeric characters
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
            // Enable fields and buttons
            addBtn.IsEnabled = true;
            searchInput.IsEnabled = true;
            sortInput.IsEnabled = true;
        }

        private void ListGarments(string garmentId)
        {
            if(garmentId == "")
            {
                // List all garments
                garmentList.ItemsSource = garmentService?.GetAll();
            } else
            {
                // Filter by Garment ID
                garmentList.ItemsSource = garmentService?.SearchGarment(int.Parse(garmentId));
            }
        }

        private void CreateGarmentService()
        {
            // Create GarmentService instance
            garmentService = new GarmentService(garmentJson);
        }

        private void searchInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Check Search input when it changed
            TextBox textBox = (TextBox)sender;
            string searchText = textBox.Text;
            ListGarments(searchText);
        }
    }
}