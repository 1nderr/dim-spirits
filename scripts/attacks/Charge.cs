using Godot;

public partial class Charge : Attack
{
  private const float CHARGE_TIME = 2f;
  private const float CHARGE_DISTANCE = 750;

  private Entity entity;
  private bool isCharging = false;
  private Vector2 chargeVector = Vector2.Zero;
  private Vector2 chargeDirection = Vector2.Zero;

  public override async void Use(Vector2 direction, Entity entity)
  {
    this.entity = entity;
    this.entity.hurtboxComponent.isInvincible = true;
    entity.animationComponent.PlayAttack(entity.direction);

    await ToSignal(GetTree().CreateTimer(CHARGE_TIME), Timer.SignalName.Timeout);

    isCharging = true;

    var target = entity.controller.GetAttackTarget().GlobalPosition;
    chargeDirection = GlobalPosition.DirectionTo(target).Normalized();
    chargeVector = chargeDirection * CHARGE_DISTANCE;
    entity.animationComponent.PlayAttack(chargeDirection);
  }

  public override void _Process(double delta)
  {
    if (!isCharging) { return; }

    if (GetParent() is Entity entity)
    {
      chargeVector = chargeVector.Lerp(Vector2.Zero, 0.1f);
      if (chargeVector.Round() == Vector2.Zero)
      {
        isCharging = false;
        this.entity.hurtboxComponent.isInvincible = false;
        EmitSignal(SignalName.Done);
        QueueFree();
      }

      entity.Velocity = entity.velocityComponent.CalculateBiasedVelocity(chargeDirection, (float)delta, chargeVector);
      entity.MoveAndSlide();
    }
  }
}