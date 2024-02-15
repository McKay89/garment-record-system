using GarmentRecordSystemWPF.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarmentRecordSystemWPF.Utils
{
    internal static class InputValidator
    {
        public static bool Null(string input)
        {
            if (!string.IsNullOrWhiteSpace(input)) return true;
            return false;

        }

        public static DateOnly? Date(string date)
        {
            DateOnly selectedDate;
            bool isValidDate = DateOnly.TryParse(date, out selectedDate);

            if (isValidDate) return selectedDate;
            return null;
        }

        public static Sizes? Size(string size)
        {
            Sizes selectedSize;
            bool isValidSize = Enum.TryParse(size, out selectedSize);

            if (isValidSize && Enum.GetNames(typeof(Sizes)).Contains(size))
            {
                return selectedSize;
            }
            return null;
        }
    }
}
