class_name IdleState
extends State

var _entity: Entity


func _ready() -> void:
	_entity = get_parent().get_parent()
	_entity.hurtbox_component.hit.connect(_on_hit)
	_entity.hurtbox_component.stunned.connect(_on_stunned)


func enter() -> void:
	_entity.animation_component.play_idle(_entity.direction)


func update(_delta: float) -> void:
	if _entity.controller.get_movement_input() != Vector2.ZERO:
		transition(StateType.MOVE)
		return

	if _entity.controller.get_attack_input():
		transition(StateType.ATTACK)
		return

	if _entity.controller.get_roll_input():
		transition(StateType.ROLL)
		return

	if _entity.controller.get_item_input():
		transition(StateType.ITEM)
		return


func _on_hit() -> void:
	transition(StateType.DAMAGED)


func _on_stunned() -> void:
	transition(StateType.STUNNED)
