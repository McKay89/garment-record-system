using GarmentRecordSystemWPF.JsonController;
using GarmentRecordSystemWPF.Model;
using GarmentRecordSystemWPF.Service;
using GarmentRecordSystemWPF.Utils;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GarmentRecordSystemWPF
{
    public partial class MainWindow : Window
    {
        private List<GarmentJson> garmentJson;
        private IGarmentService? garmentService;
        private Garment? selectedGarment;
        private int selectedGarmentIndex;

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
                SetButtonsStatus();
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

        private void SetButtonsStatus()
        {
            // Set buttons enabled & disabled
            addBtn.IsEnabled = true;
            searchInput.IsEnabled = true;
            sortInput.IsEnabled = true;
            updateBtn.IsEnabled = false;
            deleteBtn.IsEnabled = false;
        }

        private void ListGarments(string garmentId)
        {
            garmentList.ItemsSource = new List<string>();

            if (garmentId == "")
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

        private void DisableButtons()
        {
            // Disable Update & Delete buttons
            updateBtn.IsEnabled = false;
            deleteBtn.IsEnabled = false;
        }

        private void FocusGarment()
        {
            // Focus the lastly selected garment
            garmentList.Focus();
            garmentList.SelectedIndex = selectedGarmentIndex;
        }

        private void searchInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Check Search input when it changed
            TextBox textBox = (TextBox)sender;
            string searchText = textBox.Text;
            ListGarments(searchText);

            DisableButtons();
        }

        private void sortInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get ComboBox item
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            if (selectedItem != null)
            {
                string? selectedSortItem = selectedItem.Content.ToString();

                // Sort by selected item
                switch (selectedSortItem)
                {
                    case "Garment ID":
                        garmentList.ItemsSource = garmentService?.SortByGarmentId();
                        break;
                    case "Brand Name":
                        garmentList.ItemsSource = garmentService?.SortByBrandName();
                        break;
                    case "Purchase Date":
                        garmentList.ItemsSource = garmentService?.SortByPurchaseDate();
                        break;
                    case "Color":
                        garmentList.ItemsSource = garmentService?.SortByColor();
                        break;
                    case "Size":
                        garmentList.ItemsSource = garmentService?.SortBySize();
                        break;
                }

            }

            DisableButtons();
        }

        private void garmentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get selected ListView item
            ListView listView = (ListView)sender;
            selectedGarmentIndex = listView.SelectedIndex;
            selectedGarment = (Garment)listView.SelectedItem;

            // Enable Update & Delete buttons
            updateBtn.IsEnabled = true;
            deleteBtn.IsEnabled = true;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            FocusGarment();

            if (Popup.Confirm("Delete", "Are you sure you want to delete the selected item?"))
            {
                garmentService?.DeleteGarment(selectedGarment?.GarmentId);
                ListGarments("");
                Popup.Alert("Success", "Garment successfully deleted !");
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            FocusGarment();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();

            AddGarmentWindow addGarmentWindow = new AddGarmentWindow(garmentService);
            addGarmentWindow.Owner = this;
            addGarmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bool creationResult = addGarmentWindow.ShowDialog() ?? false;

            if (creationResult) ListGarments("");
        }

        private void searchInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            DisableButtons();
        }

        private void sortInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            DisableButtons();
        }
    }
}