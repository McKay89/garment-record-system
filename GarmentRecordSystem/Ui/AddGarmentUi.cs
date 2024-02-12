using System.Collections;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Selector;

namespace GarmentRecordSystem.Ui;

public class AddGarmentUi : UiBase
{
    private IGarmentBrowser _garmentBrowser;
    private readonly ILogger _logger = new ConsoleLogger();
    
    public AddGarmentUi(string title, IGarmentBrowser garmentBrowser) : base(title)
    {
        _garmentBrowser = garmentBrowser;
    }

    public override void Run()
    {
        var collectedData = CollectData();
        bool result = _garmentBrowser.AddGarment(collectedData);
        
        _garmentBrowser.PrintAllGarment();
    }

    private List<Tuple<string, string, Sizes>> CollectData()
    {
        // Get Brand Name
        Console.Write(" Brand name: ");
        var brand = Console.ReadLine();
        
        // Get Color
        Console.Write(" Color: ");
        var color = Console.ReadLine();
        
        // Get Size
        var size = GetSizeInput();

        var collectedData = new List<Tuple<string, string, Sizes>>
        {
            Tuple.Create(brand, color, size)!
        };

        return collectedData;
    }
    
    private Sizes GetSizeInput()
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
            Console.Write(" Size: ");
            var userInput = Console.ReadLine();

            isValidSize = Enum.TryParse(userInput, out selectedSize);

            if (!isValidSize)
            {
                _logger.LogMessage("Invalid size! Please choose from the available options.", "WARNING");
            }

        } while (!isValidSize);

        return selectedSize;
    }
}