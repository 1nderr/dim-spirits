using Godot;
using System;

public partial class Chest : InteractableObject
{
	[Export] private int itemId;
	[Export] private Sprite2D itemSprite;
	[Export] private AnimationPlayer animationPlayer;

	private ItemStore itemStore;

	public override void _Ready()
	{
		itemStore = GetNode<ItemStore>("/root/ItemStore");
	}

	public override void Interact(Node interactor)
	{
		if (!interactable || interactor is not Player) { return; }
		var player = (Player)interactor;

		interactable = false;
		itemSprite.Texture = itemStore.GetItemSprite(itemId);
		animationPlayer.Play("Open");

		player.inventoryComponent.items.Add(itemId);
	}
}
