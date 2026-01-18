using System;
using Exiled.API.Features;
using MultiBandRadio.Components.Events.EventArgs.Player;

namespace MultiBandRadio.Components.Events
{
    public partial class PlayerEvents
    {
        public event Action<PlayerFullConnectedEventArgs> PlayerFullConnected;
    }

    public partial class PlayerEvents
    {
        public void InvokePlayerFullConnected(Player player)
        {
            var ev = new PlayerFullConnectedEventArgs(player);
            PlayerFullConnected?.Invoke(ev);
        }
    }
}