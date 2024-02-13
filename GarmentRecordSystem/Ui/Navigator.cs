using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Ui.Selector;

namespace GarmentRecordSystem.Ui;

public static class Navigator
{
    public static void MainMenu(ILogger logger, SortedList<int, UiFactoryBase> factories)
    {
        // Create UI Selector
        var uiSelector = new UiSelector(logger, factories);
        UiBase ui = uiSelector.Select();

        // Run factory
        ui.DisplayTitle();
        ui.PrintSubmenuInfos();
        ui.Run();
    }
}