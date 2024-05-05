using Godot;
using System;

public partial class PickupComponent : Area2D
{
  [Export] private SpriteFlashEffect spriteFlashEffect;

  public override void _Ready()
  {
    BodyEntered += OnBodyEntered;
    spriteFlashEffect.Flash();
  }

  public void OnBodyEntered(Node2D body)
  {
    if (body is Player player)
    {
      var loot = (Loot)GetParent();
      loot.OnPickup(player);
      loot.QueueFree();
    }
  }
}
