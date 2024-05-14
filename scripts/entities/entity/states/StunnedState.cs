using Godot;

public partial class StunnedState : State
{
  [Export] private float stunTime;
  [Export] private SpriteColorEffect spriteColorEffect;

  private Entity entity;

  public override void _Ready()
  {
    entity = (Entity)GetParent().GetParent();
    entity.hurtboxComponent.Hit += OnHit;
  }

  public override async void Enter()
  {
    entity.animationComponent.PlayIdle(entity.direction);
    spriteColorEffect.Set();
    entity.Velocity = Vector2.Zero;
    await ToSignal(GetTree().CreateTimer(stunTime), Timer.SignalName.Timeout);
    Transition(StateType.Idle);
  }

  public override void Exit()
  {
    spriteColorEffect.Reset();
  }

  private void OnHit()
  {
    Transition(StateType.Damaged);
  }
}