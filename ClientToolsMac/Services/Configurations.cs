
namespace RTProClientToolsMac.Controllers;

public class Configurations
{
    public Configurations(IConfiguration configuration)
    {
        CopyPrintPath = configuration.GetSection("CopyPrintPath").Value;
    }

    public string CopyPrintPath { get; set; }
    public string TempDirectory
    {
        get
        {
            var tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
            Directory.CreateDirectory(tempPath);
            return tempPath;
        }
    }
}