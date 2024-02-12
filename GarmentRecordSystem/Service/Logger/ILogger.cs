namespace GarmentRecordSystem.Service.Logger;

public interface ILogger
{
    public void LogMessage(string message, string type);
    public void LogMenu(int num, string title);
    public void LogMenuTitle(string text, string type);
}