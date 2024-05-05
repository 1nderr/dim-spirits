using Godot;
using System;

public partial class InteractionComponent : RayCast2D
{
	private Player player;

	public override void _Ready()
	{
		player = (Player)GetTree().GetFirstNodeInGroup("player");
	}

	public override void _Process(double delta)
	{
		if (!IsInstanceValid(player)) { return; }

		if (
			GetCollider() is InteractableObject obj &&
			Input.IsActionJustPressed("interact") &&
			player.direction.Round() == Vector2.Up)
		{
			obj.Interact(GetParent());
		}
	}
}
