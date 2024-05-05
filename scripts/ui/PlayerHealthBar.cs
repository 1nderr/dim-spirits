using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerHealthBar : Control
{
  [Export] private PackedScene heartScene;
  [Export] private HBoxContainer container;

  private List<UiHeart> hearts = new();
  private Player player;

  public override void _Ready()
  {
    player = (Player)GetTree().GetFirstNodeInGroup("player");
    if (!IsInstanceValid(player)) { return; }

    player.healthComponent.HealthUpdated += OnHealthUpdated;

    InitializeHearts(player.healthComponent.GetMaxHealth());
  }

  public void InitializeHearts(float max)
  {
    for (int i = 0; i < max; i++)
    {
      var heart = heartScene.Instantiate<UiHeart>();
      hearts.Add(heart);
      container.AddChild(heart);
    }
    SetHearts(max, max);
  }

  public void SetHearts(float max, float hp)
  {
    for (int i = 0; i < max; i++)
    {
      if (hp == 0.5f)
      {
        hearts[i].HalfFill();
        hp = 0;
      }
      else if (hp > 0)
      {
        hearts[i].Fill();
        hp--;
      }
      else
      {
        hearts[i].Empty();
      }
    }
  }

  private void OnHealthUpdated()
  {
    SetHearts(player.healthComponent.GetMaxHealth(), player.healthComponent.GetCurrentHealth());
  }
}
