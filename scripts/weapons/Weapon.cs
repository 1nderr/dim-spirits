using Godot;
using System;

public abstract partial class Weapon : Node2D
{
  [Export] protected AnimationPlayer animationPlayer;

  public abstract void PlayAnimation(Vector2 direction);
}
