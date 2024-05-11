using Godot;

public partial class RollState : State
{
  [Export] private float rollStrength;

  private Entity entity;
  private Vector2 rollVector;
  private Vector2 direction;

  public override void _Ready()
  {
    entity = (Entity)GetParent().GetParent();
    entity.hurtboxComponent.Hit += OnHit;
    entity.hurtboxComponent.Stunned += OnStunned;
  }

  public override void Enter()
  {
    entity.animationComponent.PlayRoll(entity.direction);
    direction = entity.direction;
    rollVector = direction * rollStrength;
  }

  public override void Update(double delta)
  {
    rollVector = rollVector.Lerp(Vector2.Zero, 0.05f);
    if (rollVector.Round() == Vector2.Zero)
    {
      Transition(StateType.Idle);
      return;
    }

    entity.Velocity = entity.velocityComponent.CalculateBiasedVelocity(direction, (float)delta, rollVector);
    entity.MoveAndSlide();
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