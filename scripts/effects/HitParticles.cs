using Godot;

public partial class HitParticles : CpuParticles2D
{
  [Export] private AnimationPlayer animationPlayer;

  public void Emit(Vector2 direction)
  {
    Direction = direction;
    animationPlayer.Play("Hit");
  }
}