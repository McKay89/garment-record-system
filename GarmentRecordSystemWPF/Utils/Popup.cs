using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GarmentRecordSystemWPF.Utils
{
    internal static class Popup
    {
        public static bool Confirm(string label, string text)
        {
            MessageBoxResult confirmation = MessageBox.Show(text, label, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Alert(string label, string text)
        {
            MessageBox.Show(text, label, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
