using Godot;
using System;

public partial class HealthBar : ProgressBar
{
	[Export] private HealthComponent healthComponent;

	public override void _Ready()
	{
		healthComponent.HealthUpdated += OnHealthUpdated;
		Hide();
	}

	private void OnHealthUpdated()
	{
		Value = healthComponent.GetHealthPercent();
		Show();
	}
}
