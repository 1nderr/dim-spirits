using Godot;
using System;

public partial class Sword : Weapon
{
	[Export] private HitboxComponent hitboxComponent;

	public override void _Ready()
	{
		hitboxComponent.Hit += OnHit;
	}

	public override void PlayAnimation(Vector2 direction)
	{
		direction = direction.Round();
		if (direction.X < 0)
		{
			animationPlayer.Play("Swing_Left");
		}
		else if (direction.X > 0)
		{
			animationPlayer.Play("Swing_Right");
		}
		else if (direction.Y < 0)
		{
			animationPlayer.Play("Swing_Up");
		}
		else if (direction.Y > 0)
		{
			animationPlayer.Play("Swing_Down");
		}
	}

	private async void OnHit()
	{
		GetTree().Paused = true;
		await ToSignal(GetTree().CreateTimer(0.05f), Timer.SignalName.Timeout);
		GetTree().Paused = false;
	}
}
