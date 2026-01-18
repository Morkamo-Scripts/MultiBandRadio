using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using MultiBandRadio.Components.Events;
using MultiBandRadio.Components.Features;
using events = Exiled.Events.Handlers;

namespace MultiBandRadio
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => nameof(MultiBandRadio);
        public override string Prefix => Name;
        public override string Author => "Morkamo";
        public override Version RequiredExiledVersion => new(9, 12, 5);
        public override Version Version => new(1, 0, 0);

        public static Plugin Instance;
        public static Harmony Harmony;
        private Menu _menu;
        private RadioHandler _radioHandler;

        public override void OnEnabled()
        {
            Instance = this;
            
            _menu = new Menu();
            _radioHandler = new RadioHandler();
            
            Harmony = new Harmony("ru.morkamo.serverToys.patches");
            Harmony.PatchAll();
            
            events.Player.Verified += OnVerifiedPlayer;
            
            MorkamoEventsRegistrator.Plugin.AddRegistrator(_menu);
            MorkamoEventsRegistrator.Plugin.AddRegistrator(_radioHandler);
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            MorkamoEventsRegistrator.Plugin.RemoveRegistrator(_menu);
            MorkamoEventsRegistrator.Plugin.RemoveRegistrator(_radioHandler);
            
            events.Player.Verified -= OnVerifiedPlayer;
            
            Harmony.UnpatchAll();

            _radioHandler = null;
            _menu = null;
            Instance = null;
            base.OnDisabled();
        }
        
        private void OnVerifiedPlayer(VerifiedEventArgs ev)
        {
            if (ev.Player.IsNPC)
                return;
            
            if (ev.Player.ReferenceHub.gameObject.GetComponent<MultiBandRadioProperties>() != null)
                return;

            ev.Player.ReferenceHub.gameObject.AddComponent<MultiBandRadioProperties>();
            
            EventManager.PlayerEvents.InvokePlayerFullConnected(ev.Player);
        }
    }
}