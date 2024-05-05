using Godot;
using System;

public partial class VelocityComponent : Node
{
	[Export] public float maxSpeed;
	[Export] private float accelerationSmoothing;

	private Vector2 velocity = Vector2.Zero;

	public Vector2 CalculateVelocity(Vector2 direction, float delta)
	{
		velocity = velocity.Lerp(direction * maxSpeed, 1 - Mathf.Exp(-delta * accelerationSmoothing));
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
}
