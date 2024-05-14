using Godot;
using System;
using System.Threading.Tasks;

public abstract partial class Attack : Node2D
{
  [Signal] public delegate void DoneEventHandler();

  [Export] protected AnimationPlayer animationPlayer;

  public abstract void Use(Vector2 direction, Entity entity);
}
