using Godot;
using System;

public partial class LootComponent : Node
{
  [Export(PropertyHint.Range, "0,1,")] private float dropChance = 0.5f;

  private LootStore lootStore;

  public override void _Ready()
  {
    lootStore = GetNode<LootStore>("/root/LootStore");
  }

  public void SpawnLoot(Vector2 position)
  {
    if (GD.Randf() > dropChance) { return; }

    var lootLayer = GetTree().GetFirstNodeInGroup("loot");
    if (lootLayer == null) { return; }

    var loot = lootStore.GetLoot();
    Callable callable = new(lootLayer, MethodName.AddChild);
    callable.CallDeferred(loot);
    loot.GlobalPosition = position;
  }
}
