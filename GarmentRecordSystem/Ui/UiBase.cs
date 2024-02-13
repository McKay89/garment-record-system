using GarmentRecordSystem.Service.Logger;

namespace GarmentRecordSystem.Ui;

public abstract class UiBase
{
    private readonly string _title;
    private readonly ILogger _logger;

    protected UiBase(string title)
    {
        _title = title;
        _logger = new ConsoleLogger();
    }
    
    public void DisplayTitle()
    {
        _logger.LogMenuTitle(_title, "TITLE");
    }

    public void PrintSubmenuInfos()
    {
        Console.WriteLine(" Type '--back' if you want to return to Main Menu\n");
    }
    
    public abstract void Run();
}