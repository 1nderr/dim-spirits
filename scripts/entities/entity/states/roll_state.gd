class_name RollState
extends State

@export var roll_strength: float

var _entity: Entity
var _roll_vector := Vector2.ZERO
var _direction := Vector2.ZERO


func _ready() -> void:
	_entity = get_parent().get_parent()
	_entity.hurtbox_component.hit.connect(_on_hit)
	_entity.hurtbox_component.stunned.connect(_on_stunned)


func enter() -> void:
	_entity.animation_component.play_roll(_entity.direction)
	_direction = _entity.direction
	_roll_vector = _direction * roll_strength
	_entity.velocity_component.max_speed = roll_strength


func exit() -> void:
	_entity.velocity_component.reset_speed()


func update(delta: float) -> void:
	_roll_vector = _roll_vector.lerp(Vector2.ZERO, 0.05)
	if _roll_vector.round() == Vector2.ZERO:
		transition(StateType.IDLE)
		return

	_entity.velocity = _entity.velocity_component.calculate_biased_velocity(
		_direction, delta, _roll_vector
	)
	_entity.move_and_slide()


func _on_hit() -> void:
	transition(StateType.DAMAGED)


func _on_stunned() -> void:
	transition(StateType.STUNNED)
