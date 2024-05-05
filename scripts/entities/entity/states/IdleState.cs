using Godot;

public partial class IdleState : State
{
  private Entity entity;

  public override void _Ready()
  {
    entity = (Entity)GetParent().GetParent();
    entity.hurtboxComponent.Hit += OnHit;
    entity.hurtboxComponent.Stunned += OnStunned;
  }

  public override void Enter()
  {
    entity.animationComponent.PlayIdle(entity.direction);
  }

  public override void Update(double delta)
  {
    if (entity.controller.GetMovementInput() != Vector2.Zero)
    {
      Transition(StateType.Move);
      return;
    }

    if (entity.controller.GetAttackInput())
    {
      Transition(StateType.Attack);
      return;
    }

    if (entity.controller.GetRollInput())
    {
      Transition(StateType.Roll);
      return;
    }

    if (entity.controller.GetItemInput())
    {
      Transition(StateType.Item);
      return;
    }
  }

  private void OnHit()
  {
    Transition(StateType.Damaged);
  }

  private void OnStunned()
  {
    Transition(StateType.Stunned);
  }
}