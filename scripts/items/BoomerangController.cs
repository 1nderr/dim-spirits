using Godot;
using System;

public partial class BoomerangController : Item
{
	[Export] private PackedScene boomerangScene;

	private bool isThrown = false;

	public override void Use(Entity user)
	{
		if (isThrown) { return; }
		isThrown = true;

		var boomerang = boomerangScene.Instantiate<Boomerang>();
		boomerang.Returned += OnReturned;
		boomerang.thrower = user;
		boomerang.startDirection = user.direction;

		GetTree().GetFirstNodeInGroup("entities").AddChild(boomerang);
		boomerang.GlobalPosition = GlobalPosition;
	}

	private void OnReturned()
	{
		isThrown = false;
	}
}
