using Godot;

public abstract partial class Controller : Node2D
{
  public abstract Vector2 GetMovementInput();
  public abstract bool GetAttackInput();
  public abstract bool GetRollInput();
  public abstract bool GetItemInput();
}