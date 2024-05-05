using Godot;
using System;

public partial class EntityLootComponent : LootComponent
{
  [Export] private HealthComponent healthComponent;

  private LootStore lootStore;

  public override void _Ready()
  {
    base._Ready();
    healthComponent.Died += SpawnLoot;
  }
}
