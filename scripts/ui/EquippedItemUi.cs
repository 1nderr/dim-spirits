using System;
using Godot;

public partial class EquippedItemUi : Control
{
  [Export] private Sprite2D itemSprite;

  private ItemStore itemStore;
  private Player player;

  public override void _Ready()
  {
    itemStore = GetNode<ItemStore>("/root/ItemStore");

    player = (Player)GetTree().GetFirstNodeInGroup("player");
    player.inventoryComponent.ItemEquipped += OnItemEquipped;
  }

  public void OnItemEquipped(int itemId)
  {
    itemSprite.Texture = itemStore.GetItemSprite(itemId);
  }
}