using System.Collections.Generic;
using ASS.Events.EventArgs;
using ASS.Features.Collections;
using ASS.Features.Settings;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using LabApi.Events.Arguments.PlayerEvents;
using MEC;
using MorkamoEventsRegistrator.Components;
using MultiBandRadio.Components.Extensions;
using events = LabApi.Events.Handlers;
using Player = LabApi.Features.Wrappers.Player;

namespace MultiBandRadio
{
    public class Menu : AbstractMenu, IEventsRegistrator
    {
        public void RegisterEvents()
        {
            events.PlayerEvents.Joined += OnJoined;
            events.PlayerEvents.Left += OnLeft;
            ASS.Events.Handlers.SettingEvents.SettingTriggered += OnSettingTriggered;
        }

        public void UnregisterEvents()
        {
            events.PlayerEvents.Joined -= OnJoined;
            events.PlayerEvents.Left -= OnLeft;
            ASS.Events.Handlers.SettingEvents.SettingTriggered -= OnSettingTriggered;
        }
        
        private static readonly Dictionary<Player, PlayerMenu> Menus = new();

        public void OnJoined(PlayerJoinedEventArgs ev)
        {
            Menus[ev.Player] = new PlayerMenu(Generate, ev.Player);
        }

        public void OnLeft(PlayerLeftEventArgs ev)
        {
            if (Menus.TryGetValue(ev.Player, out PlayerMenu menu))
                menu.Destroy();
        }
        
        protected override ASSGroup Generate(Player owner)
        {
            return new ASSGroup(
            [
                new ASSHeader(7000, "Multi-band Radio", false, 
                    "Этот раздел позволяет вам настроить рацию на вашу собственную частоту!"),
                
                new ASSTwoButtons(7001, "Режим:", "Обычный", "Мультичастотный", false,
                    "Если включён мультичастотный режим, то рация будет вещать на установленную частоту!"),
                
                new ASSSlider(7002, "Частота рации:", 1f, 1f, 1000f, isInteger: true)
            ], 
            viewers: p => p == owner);
        }
        
        private void OnSettingTriggered(SettingTriggeredEventArgs ev)
        {
            switch (ev.Setting)
            {
                case ASSTwoButtons { Id: 7001 } button:
                    ev.Player.AsExiled().MultiBandRadio().PlayerProps.IsEnabled = button.RightSelected;
                    break;
                
                case ASSSlider { Id: 7002 } slider:
                    ev.Player.AsExiled().MultiBandRadio().PlayerProps.RadioBand = (int)slider.Value;
                    break;
            }
        }
    }
}