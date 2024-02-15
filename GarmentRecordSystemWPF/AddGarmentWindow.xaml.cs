using GarmentRecordSystemWPF.Enums;
using GarmentRecordSystemWPF.Service;
using GarmentRecordSystemWPF.Utils;
using System.Windows;

namespace GarmentRecordSystemWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddGarmentWindow : Window
    {
        private IGarmentService? garmentService;

        internal AddGarmentWindow(IGarmentService? garmentService)
        {
            InitializeComponent();

            this.garmentService = garmentService;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Declare input values
            var brandName = InputValidator.Null(brandNameInput.Text);
            var purchaseDate = InputValidator.Date(purchaseDateInput.Text);
            var colorName = InputValidator.Null(colorInput.Text);
            var sizeName = InputValidator.Size(sizeInput.Text);

            // Validate all fields
            bool resultValidation = CheckValidations(brandName, purchaseDate, colorName, sizeName);

            // If validaion is ok, create garment
            if (resultValidation)
            {
                var collectedData = new List<Tuple<string, DateOnly, string, Sizes>>
                {
                    Tuple.Create(
                        brandNameInput.Text,
                        DateOnly.Parse(purchaseDateInput.Text),
                        colorInput.Text,
                        (Sizes)Enum.Parse(typeof(Sizes), sizeInput.Text))
                };

                bool resultAdd = garmentService?.AddGarment(collectedData) ?? false;

                // If creation success, close window
                if (resultAdd)
                {
                    Popup.Alert("Success", "Garment successfully created !");
                    this.DialogResult = true;
                    this.Close();
                } else
                {
                    Popup.Alert("Error", "An error occured while creating Garment !");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private static bool CheckValidations(bool brandName, DateOnly? purchaseDate, bool colorName, Sizes? sizeName)
        {
            if (!brandName || !colorName)
            {
                Popup.Alert("Error", "Every field is required !");
                return false;
            }

            if(purchaseDate == null)
            {
                Popup.Alert("Error", "The date entered is not in the correct format !");
                return false;
            }

            if(sizeName == null)
            {
                Popup.Alert("Error", "The specified size cannot be selected !\n Available sizes: 'S', 'M', 'L', 'XL', 'XXL'");
                return false;
            }

            return true;
        }
    }
}
