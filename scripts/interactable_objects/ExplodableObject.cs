using Godot;
using System;

public partial class ExplodableObject : InteractableObject
{
	[Export] private PackedScene breakEffectScene;
	[Export] private LootComponent lootComponent;

	public async override void Interact(Node interactor)
	{
		if (interactor is not ExplosionEffect) { return; }

		Hide();

		var breakEffect = breakEffectScene.Instantiate<SmokeEffect>();
		GetParent().AddChild(breakEffect);
		breakEffect.GlobalPosition = GlobalPosition;
		await ToSignal(breakEffect.animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

		lootComponent.SpawnLoot(GlobalPosition);

		QueueFree();
	}
}
