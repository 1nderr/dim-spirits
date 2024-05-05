using Godot;

public partial class EnemyController : Controller
{
  [Export] private Area2D triggerArea;

  private Player player;
  private bool triggered = false;

  private bool test = false;

  public override void _Ready()
  {
    player = (Player)GetTree().GetFirstNodeInGroup("player");
    triggerArea.BodyEntered += OnTriggerAreaEntered;
  }

  public override Vector2 GetMovementInput()
  {
    if (!IsInstanceValid(player) || !triggered) { return Vector2.Zero; }

    return GlobalPosition.DirectionTo(player.GlobalPosition).Normalized();
  }

  public override bool GetAttackInput()
  {
    return false;
  }

  public override bool GetRollInput()
  {
    return false;
  }

  public override bool GetItemInput()
  {
    return false;
  }

  private void OnTriggerAreaEntered(Node2D other)
  {
    triggered = true;
  }
}