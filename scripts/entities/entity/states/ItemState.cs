using Godot;

public partial class ItemState : State
{
  private Player entity;

  public override void _Ready()
  {
    entity = (Player)GetParent().GetParent();
    entity.hurtboxComponent.Hit += OnHit;
    entity.hurtboxComponent.Stunned += OnStunned;
  }

  public override void Enter()
  {
    entity.inventoryComponent.UseEquippedItem();
  }

  public override void Update(double delta)
  {
    Transition(StateType.Idle);
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