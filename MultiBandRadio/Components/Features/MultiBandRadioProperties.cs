using Exiled.API.Features;
using MultiBandRadio.Components.Features.Components;
using UnityEngine;

namespace MultiBandRadio.Components.Features;

public sealed class MultiBandRadioProperties() : MonoBehaviour
{
    private void Awake()
    {
        Player = Player.Get(gameObject);
        PlayerProps = new PlayerProps(this);
    }

    public Player Player { get; private set; }
    public PlayerProps PlayerProps { get; private set; }
}