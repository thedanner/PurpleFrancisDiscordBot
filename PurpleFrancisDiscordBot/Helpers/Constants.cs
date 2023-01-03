using System;

namespace PurpleFrancisDiscordBot.Helpers;

public static class Constants
{
    public const int DelayAfterCommandMs = 250;
    public static readonly TimeSpan DelayAfterCommand = TimeSpan.FromMilliseconds(DelayAfterCommandMs);
}
