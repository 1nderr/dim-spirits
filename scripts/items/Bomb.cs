using Godot;
using System;

public partial class Bomb : Node2D
{
	private const float DETONATION_TIME = 1.5f;
	private const float CAMERA_SHAKE = 0.5f;

	[Signal] public delegate void ExplodedEventHandler();

	[Export] private AnimationPlayer animationPlayer;
	[Export] private PackedScene explosionEffect;

	private GameCamera camera;

	public async override void _Ready()
	{
		await ToSignal(GetTree().CreateTimer(DETONATION_TIME), Timer.SignalName.Timeout);
		animationPlayer.Play("Explode");
		await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

		var explosion = explosionEffect.Instantiate<Node2D>();
		GetParent().AddChild(explosion);
		explosion.GlobalPosition = GlobalPosition;

		camera = (GameCamera)GetTree().GetFirstNodeInGroup("camera");
		camera.Shake(CAMERA_SHAKE);

		EmitSignal(SignalName.Exploded);
		QueueFree();
	}
}
