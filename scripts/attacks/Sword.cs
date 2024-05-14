using Godot;
using System;

public partial class Sword : Attack
{
	[Export] private HitboxComponent hitboxComponent;

	private bool isAttacking = false;
	private Vector2 moveVector = Vector2.Zero;
	private Vector2 attackDirection = Vector2.Zero;
	private Entity entity;

	public override void _Ready()
	{
		hitboxComponent.Hit += OnHit;
	}

	public override async void _Process(double delta)
	{
		if (!isAttacking) { return; }

		if (GetParent() is Entity entity)
		{
			moveVector = moveVector.Lerp(Vector2.Zero, 0.3f);
			if (moveVector.Round() == Vector2.Zero)
			{
				isAttacking = false;

				entity.animationComponent.PlayAttack(attackDirection);
				PlayAnimation(attackDirection);
				await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

				EmitSignal(SignalName.Done);
				QueueFree();
			}

			entity.Velocity = entity.velocityComponent.CalculateBiasedVelocity(attackDirection, (float)delta, moveVector);
			entity.MoveAndSlide();
		}
	}

	public override void Use(Vector2 direction, Entity entity)
	{
		this.entity = entity;
		isAttacking = true;
		attackDirection = direction;
		moveVector = direction * 10;
	}

	public void PlayAnimation(Vector2 direction)
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
