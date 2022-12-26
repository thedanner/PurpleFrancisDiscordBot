using Discord.Interactions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PurpleFrancis.Models.Configuration;
using PurpleFrancis.Helpers.Extensions;
using Discord;
using System.Globalization;
using Discord.Rest;

namespace PurpleFrancis.Discord.Modules;

public class DefaultInteractionModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly ILogger<DefaultInteractionModule> _logger;
    private readonly Settings _settings;

    public DefaultInteractionModule(ILogger<DefaultInteractionModule> logger, IOptions<Settings>? settings)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    [SlashCommand("history", "Gets the link to Purple Francis's backstory, along with a quote.")]
    public async Task HistoryAsync()
    {
        IEmote? emote = null;

        if (_settings.EmoteMap.TryGetValue(
            Context.Guild.Id.ToString(CultureInfo.InvariantCulture), out var emoteId))
        {
            emote = await Context.Guild.GetEmoteAsync(emoteId);
        }

        var quote = RandomHelper.PickSecureRandom(_settings.QuotesMarkdown);

        var builder = new ComponentBuilder()
            .WithButton("My story", style: ButtonStyle.Link, emote: emote, url: _settings.Links.History);

        await RespondAsync(quote, components: builder.Build());
    }

    [SlashCommand("quote", "Gets a quote from Purple Francis.")]
    public async Task QuoteAsync()
    {
        var quote = RandomHelper.PickSecureRandom(_settings.QuotesMarkdown);
        await RespondAsync(quote);
    }
}
