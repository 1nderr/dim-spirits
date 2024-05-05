using Godot;
using System;

public partial class Grass : InteractableObject
{
  [Export] private LootComponent lootComponent;

  public override void Interact(Node interactor)
  {
    if (interactor is Sword || interactor is ExplosionEffect || interactor is Boomerang)
    {
      lootComponent.SpawnLoot(GlobalPosition);
      QueueFree();
    }
  }
}
