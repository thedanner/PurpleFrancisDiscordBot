using System.Collections.Generic;

namespace PurpleFrancis.Models.Configuration;

public class Settings
{
    public string BotToken { get; set; } = "";
    public SettingsLinks Links { get; set; } = new SettingsLinks();
    public List<string> QuotesMarkdown { get; set; } = new List<string>();
    public Dictionary<string, ulong> EmoteMap { get; set; } = new Dictionary<string, ulong>();
}

public class SettingsLinks
{
    public string History { get; set; } = "";
}
