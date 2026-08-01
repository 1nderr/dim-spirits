class_name MoveState
extends State

var _entity: Entity


func _ready() -> void:
	_entity = get_parent().get_parent()
	_entity.hurtbox_component.hit.connect(_on_hit)
	_entity.hurtbox_component.stunned.connect(_on_stunned)


func update(delta: float) -> void:
	var input_vector := _entity.controller.get_movement_input()
	if input_vector == Vector2.ZERO:
		_entity.animation_component.play_idle(_entity.direction)
	else:
		_entity.animation_component.play_move(input_vector)

	_entity.velocity = _entity.velocity_component.calculate_velocity(input_vector, delta)
	_entity.move_and_slide()

	if _entity.velocity == Vector2.ZERO:
		transition(StateType.IDLE)
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
