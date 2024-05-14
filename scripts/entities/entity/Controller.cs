using Godot;

public abstract partial class Controller : Node2D
{
  public abstract Vector2 GetMovementInput();
  public abstract bool GetAttackInput();
  public abstract Entity GetAttackTarget();
  public abstract bool GetRollInput();
  public abstract bool GetItemInput();
}