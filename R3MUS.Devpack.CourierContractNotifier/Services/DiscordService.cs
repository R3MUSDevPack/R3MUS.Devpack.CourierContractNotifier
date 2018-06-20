using AutoMapper;
using R3MUS.Devpack.CourierContractNotifier.Models;
using R3MUS.Devpack.Discord;
using R3MUS.Devpack.Discord.Models.Embeds;
using System.Drawing;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly IMapper _mapper;

        public DiscordService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void PostMessage(NotificationRequest request)
        {
            var embedPostModel = _mapper.Map<EmbedPostModel>(request);
            var embed = _mapper.Map<Embed>(request);
            embed.SetColour(ColorTranslator.FromHtml(request.GetColour()));

            embedPostModel.Embeds.Add(embed);

            Client.PostEmbed(embedPostModel, Properties.Settings.Default.DiscordWebhook);
        }
    }
}
