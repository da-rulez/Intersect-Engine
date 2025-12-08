namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class RestoreStaminaCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.RestoreStamina;

    public int Amount { get; set; }
}
