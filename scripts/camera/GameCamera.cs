using Godot;
using Godot.Collections;
using System;

public partial class GameCamera : Camera2D
{
	[Export] private CameraShakeEffect cameraShakeEffect;

	private Vector2 targetPosition = Vector2.Zero;

	public override void _Ready()
	{
		MakeCurrent();
	}

	public override void _Process(double delta)
	{
		FindTargetPosition();
		GlobalPosition = GlobalPosition.Lerp(targetPosition, 1.0f - (float)Mathf.Exp(-delta * 20));
	}

	private void FindTargetPosition()
	{
		var player = (Node2D)GetTree().GetFirstNodeInGroup("player");
		if (!IsInstanceValid(player)) { return; }
		targetPosition = player.GlobalPosition;
	}

	public void Shake(float amount)
	{
		cameraShakeEffect.AddTrauma(amount);
	}
}
