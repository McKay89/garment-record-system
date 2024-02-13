using GarmentRecordSystem.Service.Products.Browser;

namespace GarmentRecordSystem.Ui.Factory;

public class ExitGarmentUiFactory : UiFactoryBase
{
    public ExitGarmentUiFactory()
    {
        
    }

    public override string UiName => "Exit";
    
    public override UiBase Create(SortedList<int, UiFactoryBase> factories)
    {
        return new ExitGarmentUi(UiName);
    }
}