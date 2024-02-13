using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Ui.Factory;

namespace GarmentRecordSystem.Ui.Selector;

public class UiSelector
{
    private readonly ILogger _logger;
    private readonly SortedList<int, UiFactoryBase> _factories;
    
    public UiSelector(ILogger logger, SortedList<int, UiFactoryBase> factories)
    {
        _logger = logger;
        _factories = factories;
    }

    public UiBase Select()
    {
        _logger.LogMessage("Welcome in Garment Record System", "INFO");
        Console.WriteLine(" 'garments.json' file loaded successfully");
        DisplayMenu();
        
        int getChoice = GetIntInput();
        return _factories.Values[getChoice - 1].Create(_factories);
    }

    private void DisplayMenu()
    {
        _logger.LogMessage("Main Menu", "~");
        
        foreach (var fact in _factories)
        {
            _logger.LogMenu(fact.Key, fact.Value.UiName);
        }
    }
    
    private int GetIntInput()
    {
        int input = 0;
        
        while (true)
        {
            Console.Write("\n Please choose: ");
            var i = Console.ReadLine();
            if (!int.TryParse(i, out input))
            {
                _logger.LogMessage("Please provide a valid number!", "WARNING");
            } 
            else if (input is < 1 or > 8)
            {
                _logger.LogMessage("Input must be between 1 - 8!", "WARNING");
            }
            else
            {
                break;
            }
        }

        if (input == 8)
        {
            _logger.LogMessage("Application is now shutting down...", "INFO");
            Environment.Exit(0);
        }
        
        return input;
    }
}