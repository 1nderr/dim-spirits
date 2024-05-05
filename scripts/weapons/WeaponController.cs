using Godot;
using System;

public partial class WeaponController : Node2D
{
  [Export] protected PackedScene weaponScene;

  public void Attack(Vector2 direction)
  {
    direction = direction.Round();

    var weapon = weaponScene.Instantiate<Weapon>();
    GetParent().AddChild(weapon);

    if (direction.Y < 0)
    {
      GetParent().MoveChild(weapon, 0);
    }

    weapon.GlobalPosition = GlobalPosition;
    weapon.PlayAnimation(direction);
  }
}
