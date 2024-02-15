using GarmentRecordSystemWPF.JsonController;
using GarmentRecordSystemWPF.Model;
using GarmentRecordSystemWPF.Service;
using GarmentRecordSystemWPF.Utils;
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
                // Change button & statistics statuses
                unsavedChanges.Visibility = Visibility.Visible;
                countGarments.Visibility = Visibility.Visible;
                jsonLoaded.Visibility = Visibility.Visible;
                loadBtn.IsEnabled = false;
                saveBtn.IsEnabled = true;

                CreateGarmentService();
                RefreshUi();
            } else
            {
                // JSON can't be loaded, exit application
                Environment.Exit(0);
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            int changes = Changes.GetChangeSum();

            if(changes > 0)
            {
                if(Popup.Confirm("Exit", $"There are {changes} unsaved changes\n Are you sure you want to exit?"))
                {
                    // Exit application
                    Environment.Exit(0);
                }
            } else
            {
                // Exit application
                Environment.Exit(0);
            }
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

        private void RefreshGarmentList(string garmentId)
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

        private void RefreshStatistics()
        {
            changesNum.Text = Changes.GetChangeSum().ToString();
            garmentNum.Text = garmentService?.GetGarmentCount().ToString();
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
            RefreshGarmentList(searchText);

            SetButtonsStatus();
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

            RefreshUi();
        }

        private void garmentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get selected ListView item
            ListView listView = (ListView)sender;
            selectedGarment = (Garment)listView.SelectedItem;

            // Enable Update & Delete buttons
            updateBtn.IsEnabled = true;
            deleteBtn.IsEnabled = true;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Popup.Confirm("Delete", "Are you sure you want to delete the selected item?"))
            {
                garmentService?.DeleteGarment(selectedGarment?.GarmentId);
                Popup.Alert("Success", "Garment successfully deleted !");
            }

            RefreshUi();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateGarmentWindow updateGarmentWindow = new UpdateGarmentWindow(garmentService, selectedGarment);
            updateGarmentWindow.Owner = this;
            updateGarmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bool updateResult = updateGarmentWindow.ShowDialog() ?? false;

            RefreshUi();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddGarmentWindow addGarmentWindow = new AddGarmentWindow(garmentService);
            addGarmentWindow.Owner = this;
            addGarmentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bool creationResult = addGarmentWindow.ShowDialog() ?? false;

            RefreshUi();
        }

        private void searchInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            RefreshUi();
        }

        private void sortInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            RefreshUi();
        }

        private void RefreshUi()
        {
            // Refreshing the UI components
            RefreshGarmentList("");
            SetButtonsStatus();
            RefreshStatistics();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshUi();

            var allGarments = garmentService?.GetAllAsJson() ?? new List<GarmentJson>();

            bool resultSave = JsonFileController.Write(allGarments);

            if(resultSave)
            {
                Popup.Alert("Success", "Data successfully saved into JSON !");
                Changes.ResetCounters();
            } else
            {
                Popup.Alert("Error", "Can't save data ! Please try again !");
            }

            RefreshUi();
        }
    }
}