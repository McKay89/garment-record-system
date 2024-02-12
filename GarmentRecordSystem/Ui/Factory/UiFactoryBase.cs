namespace GarmentRecordSystem.Ui.Factory;

public abstract class UiFactoryBase
{
    public abstract string UiName { get; }
    public abstract UiBase Create();
}