using System.Runtime.InteropServices;
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
    
    public ExitGarmentUi(string title) : base(title)
    {

    }

    public override void Run()
    {
        // Exit application
        _logger.LogMessage("Application is shutting down in 2 sec...", "INFO");
        Thread.Sleep(2000);
        Environment.Exit(0);
    }
}