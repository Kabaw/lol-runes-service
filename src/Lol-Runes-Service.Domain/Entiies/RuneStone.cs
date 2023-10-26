using Lol_Runes_Service.Domain.Models;
using Lol_Runes_Service.Shared.Enums;

namespace Lol_Runes_Service.Domain.Entiies;

public class RuneStone : Entity
{
    public RuneTypeEnum RuneType { get; internal set; }

    public RuneStone(RuneTypeEnum runeType)
    {
        RuneType = runeType;
    }
}