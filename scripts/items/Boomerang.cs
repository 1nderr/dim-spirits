using System;
using Godot;

public partial class Boomerang : Node2D
{
  [Signal] public delegate void ReturnedEventHandler();

  [Export] private VelocityComponent velocityComponent;
  [Export] private HitboxComponent hitboxComponent;
  [Export] private Timer timer;

  public Entity thrower;
  public Vector2 startDirection;
  private bool isReturning = false;

  public override void _Ready()
  {
    timer.Timeout += OnHitOrTimeout;
    hitboxComponent.Hit += OnHitOrTimeout;
    hitboxComponent.Interacted += OnHitOrTimeout;

    hitboxComponent.SetCollisionMaskValue(
      (int)Layer.Player,
      !thrower.hurtboxComponent.GetCollisionLayerValue((int)Layer.Player)
    );

    hitboxComponent.SetCollisionMaskValue(
      (int)Layer.Enemies,
      !thrower.hurtboxComponent.GetCollisionLayerValue((int)Layer.Enemies)
    );
  }

  public override void _Process(double delta)
  {
    if (isReturning && thrower.GlobalPosition.DistanceTo(GlobalPosition) < 8)
    {
      EmitSignal(SignalName.Returned);
      QueueFree();
      return;
    }

    var direction = isReturning ? GlobalPosition.DirectionTo(thrower.GlobalPosition) : startDirection;
    var velocity = velocityComponent.CalculateVelocity(direction, (float)delta);
    GlobalPosition += velocity * (float)delta;
  }

  private void OnHitOrTimeout()
  {
    isReturning = true;
  }
}
