using AutoMapper;
using R3MUS.Devpack.CourierContractNotifier.Models;
using R3MUS.Devpack.Slack;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public class SlackService : ISlackService
    {
        private readonly IMapper _mapper;

        public SlackService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void PostMessage(NotificationRequest request)
        {
            var message = _mapper.Map<MessagePayload>(request);
            message.Attachments.Add(_mapper.Map<MessagePayloadAttachment>(request));

            Plugin.SendToRoom(message, Properties.Settings.Default.SlackChannel, Properties.Settings.Default.SlackWebhook, "Bishop");
        }
    }
}
