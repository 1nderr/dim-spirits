using Godot;

public partial class DamagedState : State
{
  [Export] private float knockbackStrength;
  [Export] private float cameraShakeAmount;
  [Export] private SpriteFlashEffect spriteFlashEffect;

  private Entity entity;
  private Vector2 knockbackVector;
  private GameCamera camera;

  public override void _Ready()
  {
    entity = (Entity)GetParent().GetParent();
    camera = (GameCamera)GetTree().GetFirstNodeInGroup("camera");
  }

  public override void Enter()
  {
    entity.animationComponent.PlayIdle(entity.direction);
    spriteFlashEffect.Flash();
    camera.Shake(cameraShakeAmount);
    knockbackVector = entity.hurtboxComponent.hitDirection * knockbackStrength;
  }

  public override void Update(double delta)
  {
    knockbackVector = knockbackVector.Lerp(Vector2.Zero, 0.075f);
    if (knockbackVector.Round() == Vector2.Zero)
    {
      Transition(StateType.Idle);
      return;
    }

    entity.Velocity = entity.velocityComponent.CalculateBiasedVelocity(entity.hurtboxComponent.hitDirection, (float)delta, knockbackVector);
    entity.MoveAndSlide();
  }
}