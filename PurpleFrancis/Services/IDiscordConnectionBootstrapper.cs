﻿using Discord.WebSocket;
using System.Threading;
using System.Threading.Tasks;

namespace PurpleFrancis.Services;

public interface IDiscordConnectionBootstrapper
{
    Task StartAsync(DiscordSocketClient client, CancellationToken cancellationToken);
}
