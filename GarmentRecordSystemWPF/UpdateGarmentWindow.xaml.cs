using GarmentRecordSystemWPF.Enums;
using GarmentRecordSystemWPF.Model;
using GarmentRecordSystemWPF.Service;
using GarmentRecordSystemWPF.Utils;
using System.Windows;

namespace GarmentRecordSystemWPF
{
    public partial class UpdateGarmentWindow : Window
    {
        private IGarmentService? garmentService;
        private Garment selectedGarment = new();

        internal UpdateGarmentWindow(IGarmentService? garmentService, Garment? garment)
        {
            InitializeComponent();

            this.garmentService = garmentService;
            if(garment == null)
            {
                this.DialogResult = false;
                this.Close();
            } else
            {
                selectedGarment = garment;
            }

            FillInputs();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
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
                var updatedGarment = new Garment()
                {
                    GarmentId = selectedGarment.GarmentId,
                    BrandName = brandNameInput.Text,
                    PurchaseDate = DateOnly.Parse(purchaseDateInput.Text),
                    Color = colorInput.Text,
                    Size = (Sizes)Enum.Parse(typeof(Sizes), sizeInput.Text)
                };

                bool resultAdd = garmentService?.UpdateGarment(updatedGarment) ?? false;

                // If update success, close window
                if (resultAdd)
                {
                    Popup.Alert("Success", "Garment successfully updated !");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    Popup.Alert("Error", "An error occured while updating Garment !");
                }
            }
        }

        private void FillInputs()
        {
            // Fill TextBoxes with existing data
            brandNameInput.Text = selectedGarment?.BrandName.ToString();
            purchaseDateInput.Text = selectedGarment?.PurchaseDate.ToString();
            colorInput.Text = selectedGarment?.Color.ToString();
            sizeInput.Text = selectedGarment?.Size.ToString();
        }

        private static bool CheckValidations(bool brandName, DateOnly? purchaseDate, bool colorName, Sizes? sizeName)
        {
            if (!brandName || !colorName)
            {
                Popup.Alert("Error", "Every field is required !");
                return false;
            }

            if (purchaseDate == null)
            {
                Popup.Alert("Error", "The date entered is not in the correct format !");
                return false;
            }

            if (sizeName == null)
            {
                Popup.Alert("Error", "The specified size cannot be selected !\n Available sizes: 'S', 'M', 'L', 'XL', 'XXL'");
                return false;
            }

            return true;
        }
    }
}
