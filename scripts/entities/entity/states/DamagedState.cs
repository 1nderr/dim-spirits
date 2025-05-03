using Godot;

public partial class DamagedState : State
{
  [Export] private float knockbackStrength;
  [Export] private float cameraShakeAmount;
  [Export] private SpriteFlashEffect spriteFlashEffect;
  [Export] private PackedScene hitParticlesScene;

  private Entity entity;
  private Vector2 knockbackVector;
  private GameCamera camera;
  private bool isHit = false;

  public override void _Ready()
  {
    entity = (Entity)GetParent().GetParent();
    camera = (GameCamera)GetTree().GetFirstNodeInGroup("camera");
  }

  public async override void Enter()
  {
    entity.hurtboxComponent.isInvincible = true;

    Engine.TimeScale = 0.05;
    await ToSignal(GetTree().CreateTimer(0.0075f), Timer.SignalName.Timeout);
    Engine.TimeScale = 1;

    entity.animationComponent.PlayIdle(entity.direction);
    spriteFlashEffect.Flash();

    if (hitParticlesScene != null)
    {
      var hitParticles = hitParticlesScene.Instantiate<HitParticles>();
      AddChild(hitParticles);
      hitParticles.Emit(entity.hurtboxComponent.hitDirection);
    }

    camera.Shake(cameraShakeAmount);
    knockbackVector = entity.hurtboxComponent.hitDirection * knockbackStrength;
    entity.velocityComponent.maxSpeed = knockbackStrength;

    isHit = true;
  }

  public override void Exit()
  {
    isHit = false;
    entity.hurtboxComponent.isInvincible = false;
    entity.velocityComponent.ResetSpeed();
  }

  public override void Update(double delta)
  {
    if (!isHit) { return; }

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