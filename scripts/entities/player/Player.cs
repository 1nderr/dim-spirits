using Godot;
using System;

public partial class Player : Entity
{
	[Export] public HealthComponent healthComponent;
	[Export] public InventoryComponent inventoryComponent;
}
