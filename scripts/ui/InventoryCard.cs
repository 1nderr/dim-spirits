using System;
using Godot;

public partial class InventoryCard : Control
{
  [Signal] public delegate void ItemSelectedEventHandler(int itemId);

  [Export] private Sprite2D itemSprite;

  private ItemStore itemStore;
  private int itemId;

  public override void _Ready()
  {
    itemStore = GetNode<ItemStore>("/root/ItemStore");
    GuiInput += OnGuiInput;
  }

  public void SetItem(int itemId)
  {
    this.itemId = itemId;
    itemSprite.Texture = itemStore.GetItemSprite(itemId);
  }

  public void OnGuiInput(InputEvent e)
  {
    if (e.IsActionPressed("left_click"))
    {
      EmitSignal(SignalName.ItemSelected, itemId);
    }
  }
}