using Godot;
using System;

public partial class Entity : CharacterBody2D
{
  [Export] public AnimationComponent animationComponent;
  [Export] public HurtboxComponent hurtboxComponent;
  [Export] public VelocityComponent velocityComponent;
  [Export] public Controller controller;

  public Vector2 direction = Vector2.Down;

  public override void _Process(double delta)
  {
    var inputVector = controller.GetMovementInput();
    if (inputVector != Vector2.Zero)
    {
      direction = inputVector;
    }
  }
}
