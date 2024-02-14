using System.Runtime.InteropServices;
using GarmentRecordSystem.Controllers;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Ui;

public class SaveGarmentUi : UiBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    private readonly ILogger _logger = new ConsoleLogger();
    private readonly SortedList<int, UiFactoryBase> _factories;
    
    public SaveGarmentUi(string title, IGarmentBrowser garmentBrowser, SortedList<int, UiFactoryBase> factories) : base(title)
    {
        _garmentBrowser = garmentBrowser;
        _factories = factories;
    }

    public override void Run()
    {
        var changes = Changes.GetChanges();

        if (Changes.GetChangeSum() == 0)
        {
            _logger.LogMessage("There are no changes to save ! Returning to Main Menu...", "INFO");
            Thread.Sleep(2000);
            ReturnToMainMenu();
        }
        
        // Save changes
        bool resultSave = SaveChanges(changes);

        if (resultSave)
        {
            _logger.LogMessage("Data successfully saved into JSON !", "SUCCESS");
            Changes.ResetCounters();
            WaitForKeypress();
        }
        else
        {
            _logger.LogMessage("The data was not saved !", "ERROR");
            WaitForKeypress();
        }
    }

    private bool SaveChanges(Tuple<int, int, int, int> changes)
    {
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

    private void ReturnToMainMenu()
    {
        Navigator.MainMenu(_logger, _factories);
    }

    private void WaitForKeypress()
    {
        Console.WriteLine("Press any key to return to Main Menu...");
        Console.ReadKey(true);

        ReturnToMainMenu();
    }
}