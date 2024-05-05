using Godot;
using System;

public partial class HeartLoot : Loot
{
  [Export] private int healAmount;

  public override void OnPickup(Player player)
  {
    player.healthComponent.Heal(healAmount);
  }
}
