using Exiled.API.Features;
using MultiBandRadio.Components.Features;

namespace MultiBandRadio.Components.Extensions;

public static class PlayerExtensions
{
    public static MultiBandRadioProperties MultiBandRadio(this Player player)
        => player.ReferenceHub.gameObject.GetComponent<MultiBandRadioProperties>();
    
    public static Player AsExiled(this LabApi.Features.Wrappers.Player player)
        => Player.Get(player);
}