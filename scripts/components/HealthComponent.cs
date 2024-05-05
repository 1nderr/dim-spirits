using Godot;
using System;

public partial class HealthComponent : Node
{
	[Signal] public delegate void HealthUpdatedEventHandler();
	[Signal] public delegate void DiedEventHandler(Vector2 position);

	[Export] private float maxHealth = 10;
	[Export] private PackedScene smokeEffectScene;

	private float currentHealth;

	public override void _Ready()
	{
		currentHealth = maxHealth;
	}

	public void Damage(float amount)
	{
		currentHealth = Mathf.Max(currentHealth - amount, 0);

		EmitSignal(SignalName.HealthUpdated);

		Callable callable = new(this, MethodName.CheckDeath);
		callable.CallDeferred();
	}

	public void Heal(float amount)
	{
		currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

		EmitSignal(SignalName.HealthUpdated);
	}

	public float GetHealthPercent()
	{
		return (maxHealth <= 0) ? 0 : Mathf.Min(currentHealth / maxHealth, 1);
	}

	public float GetCurrentHealth()
	{
		return currentHealth;
	}

	public float GetMaxHealth()
	{
		return maxHealth;
	}

	private async void CheckDeath()
	{
		if (currentHealth == 0)
		{
			var parent = (Node2D)GetParent();
			var position = new Vector2(parent.GlobalPosition.X, parent.GlobalPosition.Y - 8);
			parent.Hide();

			var smokeEffect = smokeEffectScene.Instantiate<SmokeEffect>();
			parent.GetParent().AddChild(smokeEffect);
			smokeEffect.GlobalPosition = position;
			await ToSignal(smokeEffect.animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

			EmitSignal(SignalName.Died, position);

			parent.QueueFree();
		}
	}
}
