using System;
using Godot;

public partial class CameraShakeEffect : Node
{
  [Export] private FastNoiseLite noise;

  private GameCamera camera;

  private float shakeDecay = 1f;
  private Vector2 shakeMaxOffset = new Vector2(100, 100);
  private float shakeMaxRotation = 0.2f;
  private float currentTrauma = 0f;
  private float traumaStrength = 2f;
  private float noiseY = 0f;
  private bool isShaking = false;

  public override void _Ready()
  {
    camera = (GameCamera)GetTree().GetFirstNodeInGroup("camera");

    Random rand = new();
    noise.Seed = rand.Next();
  }

  public override void _Process(double delta)
  {
    if (currentTrauma <= 0)
    {
      isShaking = false;
      return;
    }
    Shake((float)delta);
  }

  public void AddTrauma(float trauma)
  {
    if (isShaking) { return; }
    isShaking = true;
    currentTrauma = Mathf.Min(currentTrauma + trauma, 1.0f);
  }

  private void Shake(float delta)
  {
    currentTrauma = Mathf.Max(currentTrauma - shakeDecay * (float)delta, 0f);
    float amount = Mathf.Pow(currentTrauma, traumaStrength);
    noiseY++;
    camera.Rotation = shakeMaxRotation * amount * noise.GetNoise2D(0, noiseY);
    camera.Offset = new Vector2(
      shakeMaxOffset.X * noise.GetNoise2D(1000, noiseY),
      shakeMaxOffset.Y * noise.GetNoise2D(2000, noiseY)
    ) * amount;
  }
}
