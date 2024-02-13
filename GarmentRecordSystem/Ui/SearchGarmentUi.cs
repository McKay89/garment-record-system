using System.Runtime.InteropServices;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Ui;

public class SearchGarmentUi : UiBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    private readonly ILogger _logger = new ConsoleLogger();
    private readonly SortedList<int, UiFactoryBase> _factories;
    
    public SearchGarmentUi(string title, IGarmentBrowser garmentBrowser, SortedList<int, UiFactoryBase> factories) : base(title)
    {
        _garmentBrowser = garmentBrowser;
        _factories = factories;
    }

    public override void Run()
    {
        // List available garments
        // _garmentBrowser.PrintAllGarment();
        
        // Searching
        var garmentId = CollectData();
        Garment? foundGarment = _garmentBrowser.SearchGarment(garmentId);
        
        if (foundGarment != null)
        {
            _logger.LogMessage(" Garment found", "SUCCESS");
            _garmentBrowser.PrintGarment(foundGarment);
            
            Thread.Sleep(2000);
        }
        else
        {
            _logger.LogMessage($"Garment not found with ID: {garmentId}", "ERROR");
            Thread.Sleep(2000);
        }
        Run();
    }

    private void ReturnToMainMenu()
    {
        Navigator.MainMenu(_logger, _factories);
    }

    private int? CollectData()
    {
        // Request garment ID
        var getId = InputValidator.Integer();
        
        if (getId == null) ReturnToMainMenu();

        return getId;
    }
}