using Godot;
using System;
using System.Threading.Tasks;

public partial class AttackController : Node2D
{
  [Signal] public delegate void DoneEventHandler();

  [Export] protected PackedScene attackScene;

  private Attack attack;

  public void Attack(Vector2 direction)
  {
    attack = attackScene.Instantiate<Attack>();
    GetParent().AddChild(attack);

    if (direction.Y < 0)
    {
      GetParent().MoveChild(attack, 0);
    }

    attack.Done += OnDone;
    attack.GlobalPosition = GlobalPosition;
    attack.Use(direction, GetParent<Entity>());
  }

  public void Stop()
  {
    if (!IsInstanceValid(attack)) { return; }
    attack.QueueFree();
  }

  private void OnDone()
  {
    EmitSignal(SignalName.Done);
  }
}
