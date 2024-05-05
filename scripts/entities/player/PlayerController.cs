using Godot;

public partial class PlayerController : Controller
{
  public override Vector2 GetMovementInput()
  {
    return Input.GetVector("move_left", "move_right", "move_up", "move_down").Normalized();
  }

  public override bool GetAttackInput()
  {
    return Input.IsActionJustPressed("attack");
  }

  public override bool GetRollInput()
  {
    return Input.IsActionJustPressed("roll");
  }

  public override bool GetItemInput()
  {
    return Input.IsActionJustPressed("use_item");
  }
}