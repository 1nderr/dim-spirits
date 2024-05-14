using Godot;

public partial class EnemyController : Controller
{
  [Export] private Area2D triggerArea;
  [Export] private Area2D attackArea;
  [Export] private Timer cooldownTimer;

  private Player player;
  private bool triggered = false;
  private bool targetInRange = false;
  private bool onCooldown = false;

  public override void _Ready()
  {
    player = (Player)GetTree().GetFirstNodeInGroup("player");
    triggerArea.BodyEntered += OnTriggerAreaEntered;
    attackArea.BodyEntered += OnAttackAreaEntered;
    attackArea.BodyExited += OnAttackAreaExited;
    cooldownTimer.Timeout += OnCooldownTimerTimeout;
  }

  public override Vector2 GetMovementInput()
  {
    if (!IsInstanceValid(player) || !triggered) { return Vector2.Zero; }

    return GlobalPosition.DirectionTo(player.GlobalPosition).Normalized();
  }

  public override bool GetAttackInput()
  {
    var canAttack = targetInRange && !onCooldown;

    if (canAttack)
    {
      onCooldown = true;
      cooldownTimer.Start();
    }

    return canAttack;
  }

  public override Entity GetAttackTarget()
  {
    return player;
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

  private void OnAttackAreaEntered(Node2D other)
  {
    targetInRange = true;
  }

  private void OnAttackAreaExited(Node2D other)
  {
    targetInRange = false;
  }

  private void OnCooldownTimerTimeout()
  {
    onCooldown = false;
  }
}