class_name Charge
extends Attack

const CHARGE_TIME := 2.0
const CHARGE_DISTANCE := 750.0

@export var collider: CollisionShape2D

var _entity: Entity
var _is_charging := false
var _charge_vector := Vector2.ZERO
var _charge_direction := Vector2.ZERO


func _ready() -> void:
	collider.disabled = true


func use(_direction: Vector2, entity: Entity) -> void:
	_entity = entity
	_entity.hurtbox_component.is_invincible = true
	entity.animation_component.play_attack(entity.direction)

	await get_tree().create_timer(CHARGE_TIME).timeout

	collider.disabled = false
	_is_charging = true
	_entity.hurtbox_component.is_invincible = false

	var target := entity.controller.get_attack_target().global_position
	_charge_direction = global_position.direction_to(target).normalized()
	_charge_vector = _charge_direction * CHARGE_DISTANCE
	entity.animation_component.play_attack(_charge_direction)


func _process(delta: float) -> void:
	if not _is_charging:
		return

	var entity := get_parent() as Entity
	if entity == null:
		return

	_charge_vector = _charge_vector.lerp(Vector2.ZERO, 0.1)
	if _charge_vector.round() == Vector2.ZERO:
		_is_charging = false
		done.emit()
		queue_free()

	entity.velocity = entity.velocity_component.calculate_biased_velocity(
		_charge_direction, delta, _charge_vector
	)
	entity.move_and_slide()
