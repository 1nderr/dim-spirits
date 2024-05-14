using Godot;

public partial class AttackState : State
{
  [Export] private AttackController attackController;

  private Entity entity;

  public override void _Ready()
  {
    entity = (Entity)GetParent().GetParent();
    entity.hurtboxComponent.Hit += OnHit;
    attackController.Done += OnDone;
  }

  public override void Enter()
  {
    attackController.Attack(entity.direction);
  }

  private void OnHit()
  {
    attackController.Stop();
    Transition(StateType.Damaged);
  }

  private void OnDone()
  {
    Transition(StateType.Idle);
  }
}