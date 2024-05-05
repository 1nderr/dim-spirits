using Godot;

public partial class MoveState : State
{
  private Entity entity;

  public override void _Ready()
  {
    entity = (Entity)GetParent().GetParent();

    entity.hurtboxComponent.Hit += OnHit;
    entity.hurtboxComponent.Stunned += OnStunned;
  }

  public override void Update(double delta)
  {
    var inputVector = entity.controller.GetMovementInput();
    if (inputVector == Vector2.Zero)
    {
      entity.animationComponent.PlayIdle(entity.direction);
    }
    else
    {
      entity.animationComponent.PlayMove(inputVector);
    }

    entity.Velocity = entity.velocityComponent.CalculateVelocity(inputVector, (float)delta);
    entity.MoveAndSlide();

    if (entity.Velocity == Vector2.Zero)
    {
      Transition(StateType.Idle);
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