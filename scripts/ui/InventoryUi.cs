using System;
using Godot;

public partial class InventoryUi : CanvasLayer
{
  [Export] private PackedScene inventoryCardScene;
  [Export] private GridContainer gridContainer;

  private Player player;

  public override void _Ready()
  {
    player = (Player)GetTree().GetFirstNodeInGroup("player");
    GetTree().Paused = true;
    BuildInventory();
  }

  public override void _Process(double delta)
  {
    if (Input.IsActionJustPressed("inventory"))
    {
      Close();
    }
  }

  private void BuildInventory()
  {
    foreach (int itemId in player.inventoryComponent.items)
    {
      var inventoryCard = inventoryCardScene.Instantiate<InventoryCard>();
      gridContainer.AddChild(inventoryCard);
      inventoryCard.SetItem(itemId);
      inventoryCard.ItemSelected += OnItemSelected;
    }
  }

  private void Close()
  {
    GetTree().Paused = false;
    QueueFree();
  }

  private void OnItemSelected(int itemId)
  {
    player.inventoryComponent.EquipItem(itemId);
    Close();
  }
}