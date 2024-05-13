using Godot;
using System;

public abstract partial class Attack : Node2D
{
  [Export] protected AnimationPlayer animationPlayer;

  public abstract void Use(Vector2 direction, Entity entity);
}
