using Godot;
using Godot.Collections;
using System;

public partial class InventoryComponent : Node2D
{
	[Signal] public delegate void ItemEquippedEventHandler(int itemId);

	[Export] private PackedScene inventoryUiScene;

	private ItemStore itemStore;
	public Array<int> items = new();
	private Item equippedItem;

	public override void _Ready()
	{
		itemStore = GetNode<ItemStore>("/root/ItemStore");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("inventory"))
		{
			GetTree().GetFirstNodeInGroup("main").AddChild(inventoryUiScene.Instantiate());
		}
	}

	public void EquipItem(int id)
	{
		if (items.Count == 0) { return; }
		equippedItem?.QueueFree();

		var itemData = itemStore.GetItemData(id);

		PackedScene itemScene = (PackedScene)GD.Load(itemData.scenePath);
		equippedItem = itemScene.Instantiate<Item>();
		AddChild(equippedItem);
		equippedItem.GlobalPosition = GlobalPosition;

		EmitSignal(SignalName.ItemEquipped, id);
	}

	public void UseEquippedItem()
	{
		equippedItem?.Use((Entity)GetParent());
	}
}
