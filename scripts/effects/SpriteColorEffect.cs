using Godot;
using System;

public partial class SpriteColorEffect : Node
{
  [Export] private Sprite2D sprite;
  [Export] private Color color;

  public void Set()
  {
    sprite.Modulate = color;
  }

  public void Reset()
  {
    sprite.Modulate = Color.Color8(255, 255, 255);
  }
}
