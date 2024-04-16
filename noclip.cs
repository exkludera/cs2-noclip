using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;

namespace Noclip;

[MinimumApiVersion(80)]
public class Noclip : BasePlugin
{
    public override string ModuleName => "noclip";
    public override string ModuleVersion => "1.0.1";
    public override string ModuleAuthor => "exkludera";
    public override string ModuleDescription => "";

    public override void Load(bool hotReload)
    {

    }

    [ConsoleCommand("css_noclip", "noclip command")]
    public void OnCmdNoclip(CCSPlayerController? player, CommandInfo command)
    {
        if (blockCheck(player))
            return;

        if (player.PlayerPawn.Value.MoveType == MoveType_t.MOVETYPE_NOCLIP)
        {
            player.PlayerPawn.Value.MoveType = MoveType_t.MOVETYPE_WALK;
            Schema.SetSchemaValue(player.PlayerPawn.Value.Handle, "CBaseEntity", "m_nActualMoveType", 2); // walk
            Utilities.SetStateChanged(player.PlayerPawn.Value, "CBaseEntity", "m_MoveType");
        }
        else
        {
            player.PlayerPawn.Value.MoveType = MoveType_t.MOVETYPE_NOCLIP;
            Schema.SetSchemaValue(player.PlayerPawn.Value.Handle, "CBaseEntity", "m_nActualMoveType", 8); // noclip
            Utilities.SetStateChanged(player.PlayerPawn.Value, "CBaseEntity", "m_MoveType");
        }
    }

    private bool blockCheck(CCSPlayerController? player)
    {
        if (player == null || !player.PawnIsAlive || player.Team == CsTeam.Spectator || player.Team == CsTeam.None)
            return true;
        return false;
    }

}
