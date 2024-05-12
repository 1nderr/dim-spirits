using Godot;
using System;

public partial class SpriteFlashEffect : Node
{
	[Export] private Sprite2D sprite;

	private Tween tween;

	public void Flash()
	{
		tween = CreateTween();
		tween.TweenProperty(sprite, "modulate:v", 1, 0.2).From(30);
	}
}
