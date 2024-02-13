using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Ui;

public class UpdateGarmentUi : UiBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    private readonly ILogger _logger = new ConsoleLogger();
    private readonly SortedList<int, UiFactoryBase> _factories;
    
    public UpdateGarmentUi(string title, IGarmentBrowser garmentBrowser, SortedList<int, UiFactoryBase> factories) : base(title)
    {
        _garmentBrowser = garmentBrowser;
        _factories = factories;
    }

    public override void Run()
    {
        // List available garments
        _garmentBrowser.PrintAllGarment();

        // Collect input(s)
        var updatedGarment = CollectData();
        if (updatedGarment == null) Run();
        
        // Try to update 
        bool updateGarment = _garmentBrowser.UpdateGarment(updatedGarment);

        if (updateGarment)
        {
            _garmentBrowser.PrintAllGarment();
            _logger.LogMessage("Garment successfully changed! Return to Main Menu...", "SUCCESS");
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

    private Garment? CollectData()
    {
        // Request garment ID
        var getId = InputValidator.Integer();

        if (getId == null) ReturnToMainMenu();
        
        // Find garment
        var garment = _garmentBrowser.SearchGarment(getId);

        if (garment != null)
        {
            return UpdateFoundGarment(garment, getId);
        }

        _logger.LogMessage($"Garment not found with id: {getId}", "ERROR");
        Thread.Sleep(2000);

        return null;
    }

    private Garment UpdateFoundGarment(Garment garment, int? id)
    {
        // Get Brand name
        var brand = "";
        do
        {
            Console.Write($"Original Brand Name: {garment.BrandName} || New: ");
            brand = Console.ReadLine();
            if (brand != "--back") continue;
            ReturnToMainMenu();
            break;
        } while (InputValidator.Null(brand));

        
        // Get Purchase date
        var date = InputValidator.Date($"Original Purchase date: {garment.PurchaseDate} || New: ");
        if(date == null) ReturnToMainMenu();
        
        // Get Color
        var color = "";
        do
        {
            Console.Write($"Original Color: {garment.Color} || New: ");
            color = Console.ReadLine();
            if (color != "--back") continue;
            ReturnToMainMenu();
            break;
        } while (InputValidator.Null(color));

        
        // Get Size
        var size = InputValidator.Size($"Original Size: {garment.Size} || New: ");
        if(size == null) ReturnToMainMenu();

        return new Garment()
        {
            GarmentId = id ?? 0,
            BrandName = brand ?? "Unknown",
            PurchaseDate = date ?? DateOnly.Parse("0001. 01. 01."),
            Color = color ?? "Unknown",
            Size = size ?? (Sizes)Enum.Parse(typeof(Sizes), "S")
        };
    }
}