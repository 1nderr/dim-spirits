using Godot;

public partial class State : Node2D
{
  [Signal] public delegate void TransitionedEventHandler(State state, string newStateName);

  public virtual void Enter() { return; }

  public virtual void Exit() { return; }

  public virtual void Update(double delta) { return; }

  public void Transition(StateType name)
  {
    EmitSignal(SignalName.Transitioned, this, name.ToString());
  }
}