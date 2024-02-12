namespace GarmentRecordSystem.Ui.Factory;

public class AddGarmentUiFactory : UiFactoryBase
{
    public override string UiName => "Add";
    
    public override UiBase Create()
    {
        return new AddGarmentUi(UiName);
    }
}