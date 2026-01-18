using MultiBandRadio.Components.Features.Components.Interfaces;

namespace MultiBandRadio.Components.Features.Components;

public class PlayerProps(MultiBandRadioProperties multiBandRadioProperties) : IPlayerPropertyModule
{
    public MultiBandRadioProperties MultiBandRadioProperties { get; } = multiBandRadioProperties;
    public bool IsEnabled { get; set; } = false;
    public int RadioBand { get; set; } = 1;
}