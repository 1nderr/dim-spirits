using Godot;
using System;

public partial class BombBag : Item
{
  private const int MAX_BOMBS = 2;

  [Export] private PackedScene bombScene;

  private int bombCount = 0;

  public override void Use(Entity user)
  {
    if (bombCount == MAX_BOMBS) { return; }
    bombCount++;

    var bomb = bombScene.Instantiate<Bomb>();
    bomb.Exploded += OnExploded;
    GetTree().GetFirstNodeInGroup("entities").AddChild(bomb);
    bomb.GlobalPosition = user.GlobalPosition;
  }

  private void OnExploded()
  {
    bombCount--;
  }
}
