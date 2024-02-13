using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Service.Logger;

namespace GarmentRecordSystem.Utils;

public static class InputValidator
{
    private static readonly ILogger Logger = new ConsoleLogger();
    
    public static Sizes? Size(string label)
    {
        Sizes selectedSize;
        bool isValidSize = false;

        do
        {
            Console.WriteLine(" Available sizes:");
            foreach (Sizes size in Enum.GetValues(typeof(Sizes)))
            {
                Console.WriteLine($" - {size}");
            }
            
            Console.Write($"{label}");
            var userInput = Console.ReadLine();
            if (userInput == "--back") return null;

            isValidSize = Enum.TryParse(userInput, out selectedSize);

            if (!isValidSize)
            {
                Logger.LogMessage("Invalid size! Please choose from the available options.", "WARNING");
            }

        } while (!isValidSize);

        return selectedSize;
    }

    public static DateOnly? Date(string label)
    {
        DateOnly selectedDate;
        bool isValidDate = false;

        do
        {
            Console.Write($"{label}");
            var userInput = Console.ReadLine();
            if (userInput == "--back") return null;

            isValidDate = DateOnly.TryParse(userInput, out selectedDate);

            if (!isValidDate)
            {
                Logger.LogMessage("Invalid format! Please write a valid 'Date' format.", "WARNING");
            }

        } while (!isValidDate);

        return selectedDate;
    }
    
    public static int? Integer(int max, string label)
    {
        int input = 0;
        
        while (true)
        {
            Console.Write($"\n {label}");
            var i = Console.ReadLine();
            if (i == "--back") return null;
            
            if (!int.TryParse(i, out input))
            {
                Logger.LogMessage("Please provide a valid number!", "WARNING");
            } 
            else if (input < 1)
            {
                Logger.LogMessage("Input must be a positive integer!", "WARNING");
            }
            else if (max > 0)
            {
                if (input <= max)
                {
                    break;
                }
                else
                {
                    Logger.LogMessage($"Input must be between 1 - {max}!", "WARNING");
                }
            }
            else
            {
                break;
            }
        }
        
        return input;
    }
    
    public static int MenuInteger(int max, string label)
    {
        int input = 0;
        
        while (true)
        {
            Console.Write($"\n {label}");
            var i = Console.ReadLine();
            
            if (!int.TryParse(i, out input))
            {
                Logger.LogMessage("Please provide a valid number!", "WARNING");
            } 
            else if (input < 1)
            {
                Logger.LogMessage("Input must be a positive integer!", "WARNING");
            }
            else if (max > 0)
            {
                if (input <= max)
                {
                    break;
                }
                else
                {
                    Logger.LogMessage($"Input must be between 1 - {max}!", "WARNING");
                }
            }
            else
            {
                break;
            }
        }
        
        return input;
    }

    public static bool Null(string? input)
    {
        if (!string.IsNullOrWhiteSpace(input)) return false;
        Logger.LogMessage("Input cannot be empty!", "WARNING");
        return true;

    }
}