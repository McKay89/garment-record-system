using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Utils;

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
        
        var getChoice = InputValidator.MenuInteger(7, "Please choose: ");

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
}