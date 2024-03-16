namespace SerilogLib.Configurations
{
    public class SerilogConfigurations
    {
        public DataBaseConfigurations DataBaseConfigurations { get; set; } = new();
        public FileConfigurations FileConfigurations { get; set; } = new();
        public ConsoleConfigurations ConsoleConfigurations { get; set; } = new();
        public MailConfigurations MailConfigurations { get; set; } = new();
    }
}