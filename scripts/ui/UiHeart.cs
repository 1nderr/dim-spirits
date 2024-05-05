using Godot;
using System;

public partial class UiHeart : Control
{
	[Export] private AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		Empty();
	}

	public void Fill()
	{
		animationPlayer.Play("Filled");
	}

	public void HalfFill()
	{
		animationPlayer.Play("HalfFilled");
	}

	public void Empty()
	{
		animationPlayer.Play("Empty");
	}
}
