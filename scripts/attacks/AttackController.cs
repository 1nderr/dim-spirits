using Godot;
using System;

public partial class AttackController : Node2D
{
  [Export] protected PackedScene attackScene;

  public void Attack(Vector2 direction)
  {
    direction = direction.Round();

    var attack = attackScene.Instantiate<Attack>();
    GetParent().AddChild(attack);

    if (direction.Y < 0)
    {
      GetParent().MoveChild(attack, 0);
    }

    attack.GlobalPosition = GlobalPosition;
    attack.Use(direction, GetParent<Entity>());
  }
}
