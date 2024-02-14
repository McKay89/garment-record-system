using System.Runtime.InteropServices;
using GarmentRecordSystem.Controllers;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Ui;

public class ExitGarmentUi : UiBase
{
    private readonly ILogger _logger = new ConsoleLogger();
    private readonly IGarmentBrowser _garmentBrowser;
    private readonly SortedList<int, UiFactoryBase> _factories;
    
    public ExitGarmentUi(string title, IGarmentBrowser garmentBrowser, SortedList<int, UiFactoryBase> factories) : base(title)
    {
        _garmentBrowser = garmentBrowser;
        _factories = factories;
    }

    public override void Run()
    {
        // Check if there are any changes
        bool resultCheck = CheckChanges();

        if (resultCheck)
        {
            // Get input
            var input = InputValidator.ExitString(" There are unsaved changes. Do you want to save it before exit?");

            switch (input)
            {
                case "--back":
                    ReturnToMainMenu();
                    break;
                case "y" or "Y":
                    bool resultSave = SaveChanges();
                    if(resultSave) ExitApplication();
                    break;
            }
        }

        ExitApplication();
    }
    
    private void ReturnToMainMenu()
    {
        Navigator.MainMenu(_logger, _factories);
    }

    private void ExitApplication()
    {
        _logger.LogMessage("Application is shutting down in 2 sec...", "INFO");
        Thread.Sleep(2000);
        Environment.Exit(0);
    }

    private bool SaveChanges()
    {
        // Get changes
        var changes = Changes.GetChanges();
        
        // Log the changes
        _logger.LogMessage("Saving into the JSON...", "INFO");
        Console.WriteLine($"  Changes: " + 
                          $"{(changes.Item1 > 0 ? $"{changes.Item1} creation, " : "")}" +
                          $"{(changes.Item2 > 0 ? $"{changes.Item2} modification, " : "")}" +
                          $"{(changes.Item3 > 0 ? $"{changes.Item3} deletion, " : "")}" +
                          $"{(changes.Item4 > 0 ? $"{changes.Item4} sort " : "")}");
        
        // Save Garments into JSON
        var allGarments = _garmentBrowser.GetAll();
        bool saveResult = JsonFileWriter.Write(allGarments);

        return saveResult;
    }

    private bool CheckChanges()
    {
        if (Changes.GetChangeSum() > 0)
        {
            return true;
        }

        return false;
    }
}