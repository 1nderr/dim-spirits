using Godot;
using System;

public partial class HurtboxComponent : Area2D
{
  [Signal] public delegate void HitEventHandler();
  [Signal] public delegate void StunnedEventHandler();

  [Export] private HealthComponent healthComponent;

  public bool isInvincible = false;
  public Vector2 hitDirection = Vector2.Zero;

  public void Stun()
  {
    EmitSignal(SignalName.Stunned);
  }

  public void Damage(float amount, Vector2 position)
  {
    if (isInvincible) { return; }
    healthComponent.Damage(amount);
    hitDirection = GlobalPosition.DirectionTo(position).Normalized() * -1;
    EmitSignal(SignalName.Hit);
  }
}
