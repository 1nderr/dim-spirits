using System.Threading.Tasks;
using Godot;

public partial class AnimationComponent : Node
{
  [Export] private AnimationPlayer animationPlayer;

  public void PlayIdle(Vector2 direction)
  {
    direction = direction.Round();
    if (direction.X < 0)
    {
      animationPlayer.Play("Idle_Left");
    }
    else if (direction.X > 0)
    {
      animationPlayer.Play("Idle_Right");
    }
    else if (direction.Y < 0)
    {
      animationPlayer.Play("Idle_Up");
    }
    else if (direction.Y > 0)
    {
      animationPlayer.Play("Idle_Down");
    }
  }

  public void PlayMove(Vector2 direction)
  {
    direction = direction.Round();
    if (direction.X < 0)
    {
      animationPlayer.Play("Move_Left");
    }
    else if (direction.X > 0)
    {
      animationPlayer.Play("Move_Right");
    }
    else if (direction.Y < 0)
    {
      animationPlayer.Play("Move_Up");
    }
    else if (direction.Y > 0)
    {
      animationPlayer.Play("Move_Down");
    }
  }

  public void PlayRoll(Vector2 direction)
  {
    direction = direction.Round();
    if (direction.X < 0)
    {
      animationPlayer.Play("Roll_Left");
    }
    else if (direction.X > 0)
    {
      animationPlayer.Play("Roll_Right");
    }
    else if (direction.Y < 0)
    {
      animationPlayer.Play("Roll_Up");
    }
    else if (direction.Y > 0)
    {
      animationPlayer.Play("Roll_Down");
    }
  }

  public void PlayAttack(Vector2 direction)
  {
    direction = direction.Round();
    if (direction.X < 0)
    {
      animationPlayer.Play("Attack_Left");
    }
    else if (direction.X > 0)
    {
      animationPlayer.Play("Attack_Right");
    }
    else if (direction.Y < 0)
    {
      animationPlayer.Play("Attack_Up");
    }
    else if (direction.Y > 0)
    {
      animationPlayer.Play("Attack_Down");
    }
  }

  public async Task WaitForAnimation()
  {
    await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
  }
}