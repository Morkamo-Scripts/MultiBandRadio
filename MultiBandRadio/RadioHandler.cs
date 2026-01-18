using LabApi.Events.Arguments.PlayerEvents;
using MorkamoEventsRegistrator.Components;
using MultiBandRadio.Components.Extensions;
using VoiceChat;
using playerEvents = LabApi.Events.Handlers.PlayerEvents;

namespace MultiBandRadio;

public class RadioHandler : IEventsRegistrator
{
    public void RegisterEvents() => playerEvents.ReceivingVoiceMessage += OnReceivingVoiceMessage;
    public void UnregisterEvents() => playerEvents.ReceivingVoiceMessage -= OnReceivingVoiceMessage;

    private void OnReceivingVoiceMessage(PlayerReceivingVoiceMessageEventArgs ev)
    {
        if (ev.Message.Channel != VoiceChatChannel.Radio)
            return;

        if (ev.Player.AsExiled().MultiBandRadio().PlayerProps.IsEnabled)
        {
            if (!ev.Sender.AsExiled().MultiBandRadio().PlayerProps.IsEnabled)
            {
                ev.IsAllowed = false;
                return;
            }

            if (ev.Player.AsExiled().MultiBandRadio().PlayerProps.RadioBand !=
                ev.Sender.AsExiled().MultiBandRadio().PlayerProps.RadioBand)
            {
                ev.IsAllowed = false;
            }
        }
        else
        {
            if (ev.Sender.AsExiled().MultiBandRadio().PlayerProps.IsEnabled)
                ev.IsAllowed = false;
        }
    }
}