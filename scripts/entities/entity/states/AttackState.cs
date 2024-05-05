using Godot;

public partial class AttackState : State
{
  [Export] private WeaponController weaponController;

  private Entity entity;

  public override void _Ready()
  {
    entity = (Entity)GetParent().GetParent();
    entity.hurtboxComponent.Hit += OnHit;
  }

  public override async void Enter()
  {
    weaponController.Attack(entity.direction);
    entity.animationComponent.PlayAttack(entity.direction);
    await entity.animationComponent.WaitForAnimation();
    Transition(StateType.Idle);
  }

  private void OnHit()
  {
    Transition(StateType.Damaged);
  }
}