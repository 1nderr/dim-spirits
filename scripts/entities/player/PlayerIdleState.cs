using Godot;

public partial class PlayerIdleState : IdleState
{
  private Player player;

  public override void _Ready()
  {
    base._Ready();
    player = (Player)GetParent().GetParent();
  }

  public override void Update(double delta)
  {
    base.Update(delta);

    if (player.controller.GetMovementInput() != Vector2.Zero)
    {
      Transition(StateType.Move);
      return;
    }
  }
}