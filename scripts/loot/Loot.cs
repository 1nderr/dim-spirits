using Godot;
using System;
using System.Threading.Tasks;

public abstract partial class Loot : Node2D
{
  public abstract void OnPickup(Player player);
}
