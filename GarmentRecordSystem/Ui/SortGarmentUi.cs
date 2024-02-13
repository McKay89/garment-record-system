using System.Runtime.InteropServices;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Ui;

public class SortGarmentUi : UiBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    private readonly ILogger _logger = new ConsoleLogger();
    private readonly SortedList<int, UiFactoryBase> _factories;
    
    public SortGarmentUi(string title, IGarmentBrowser garmentBrowser, SortedList<int, UiFactoryBase> factories) : base(title)
    {
        _garmentBrowser = garmentBrowser;
        _factories = factories;
    }

    public override void Run()
    {
        // List sorting options
        Console.WriteLine(" Choose what you want to sort by:");
        _logger.LogMenu(1, "Brand Name");
        _logger.LogMenu(2, "Purchase Date");
        _logger.LogMenu(3, "Color");
        _logger.LogMenu(4, "Size");
        
        // Get Input data
        var garmentId = CollectData();
        bool resultSort = false;
        
        // Sorting
        switch (garmentId)
        {
            case 1:
                resultSort = _garmentBrowser.SortByBrandName();
                break;
            case 2:
                resultSort = _garmentBrowser.SortByPurchaseDate();
                break;
            case 3:
                resultSort = _garmentBrowser.SortByColor();
                break;
            case 4:
                resultSort = _garmentBrowser.SortBySize();
                break;
            default:
                _logger.LogMessage("Invalid input !", "ERROR");
                break;
        }

        if (resultSort)
        {
            _logger.LogMessage("Sorting process finished successfully !", "SUCCESS");
        }
        else
        {
            _logger.LogMessage("The list cannot be sorted because it contains too few items", "INFO");
        }
        
        Thread.Sleep(2000);
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        Navigator.MainMenu(_logger, _factories);
    }

    private int? CollectData()
    {
        // Request garment ID
        var getId = InputValidator.Integer(4, "Please choose from the above: ");
        
        if (getId == null) ReturnToMainMenu();

        return getId;
    }
}