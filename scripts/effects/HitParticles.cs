using Godot;

public partial class HitParticles : CpuParticles2D
{
  public void Emit(Vector2 direction)
  {
    Direction = direction;
    Emitting = true;
  }
}