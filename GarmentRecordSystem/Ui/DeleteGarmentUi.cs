using System.Runtime.InteropServices;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Ui;

public class DeleteGarmentUi : UiBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    private readonly ILogger _logger = new ConsoleLogger();
    private readonly SortedList<int, UiFactoryBase> _factories;
    
    public DeleteGarmentUi(string title, IGarmentBrowser garmentBrowser, SortedList<int, UiFactoryBase> factories) : base(title)
    {
        _garmentBrowser = garmentBrowser;
        _factories = factories;
    }

    public override void Run()
    {
        // List available garments
        _garmentBrowser.PrintAllGarment();
        
        // Try deletion
        var garmentId = CollectData();
        bool deleteGarment = _garmentBrowser.DeleteGarment(garmentId);
        
        if (deleteGarment)
        {
            _garmentBrowser.PrintAllGarment();
            _logger.LogMessage("Garment successfully deleted! Return to Main Menu...", "SUCCESS");
        }
        else
        {
            _logger.LogMessage("An error occured! Return to Main Menu...", "ERROR");
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
        var getId = InputValidator.Integer();
        
        if (getId == null) ReturnToMainMenu();

        return getId;
    }
}