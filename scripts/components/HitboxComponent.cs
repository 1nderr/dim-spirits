using Godot;
using System;

public partial class HitboxComponent : Area2D
{
	[Signal] public delegate void HitEventHandler();
	[Signal] public delegate void InteractedEventHandler();

	[Export] public float damage;
	[Export] public bool canStun;

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area2d)
	{
		if (area2d is HurtboxComponent hurtbox)
		{
			if (canStun)
			{
				hurtbox.Stun();
			}
			else
			{
				hurtbox.Damage(damage, GlobalPosition);
			}
			EmitSignal(SignalName.Hit);
		}
		else if (area2d is InteractionAreaComponent interactionArea)
		{
			interactionArea.Interact((Node2D)GetParent());
			EmitSignal(SignalName.Interacted);
		}
	}
}
