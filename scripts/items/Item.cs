using Godot;
using System;
using System.Threading.Tasks;

public abstract partial class Item : Node2D
{
  public abstract void Use(Entity user);
}
