using Godot;
using System;

public partial class VelocityComponent : Node
{
	private const float ACCELERATION_SMOOTHING = 20f;

	[Export] public float maxSpeed;

	private Vector2 velocity = Vector2.Zero;
	private float originalSpeed;

	public override void _Ready()
	{
		originalSpeed = maxSpeed;
	}

	public Vector2 CalculateVelocity(Vector2 direction, float delta)
	{
		velocity = velocity.Lerp(direction * maxSpeed, 1 - Mathf.Exp(-delta * ACCELERATION_SMOOTHING));
		if (velocity.Round() == Vector2.Zero)
		{
			velocity = Vector2.Zero;
		}
		return velocity;
	}

	public Vector2 CalculateBiasedVelocity(Vector2 direction, float delta, Vector2 bias)
	{
		return CalculateVelocity(direction, delta) + bias;
	}

	public void ResetSpeed()
	{
		maxSpeed = originalSpeed;
	}
}
