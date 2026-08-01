class_name Sword
extends Attack

var _is_attacking := false
var _move_vector := Vector2.ZERO
var _attack_direction := Vector2.ZERO


func _process(delta: float) -> void:
	if not _is_attacking:
		return

	var entity := get_parent() as Entity
	if entity == null:
		return

	_move_vector = _move_vector.lerp(Vector2.ZERO, 0.3)
	if _move_vector.round() == Vector2.ZERO:
		_is_attacking = false

		entity.animation_component.play_attack(_attack_direction)
		play_animation(_attack_direction)
		await animation_player.animation_finished

		done.emit()
		queue_free()

	entity.velocity = entity.velocity_component.calculate_biased_velocity(
		_attack_direction, delta, _move_vector
	)
	entity.move_and_slide()


func use(direction: Vector2, _entity: Entity) -> void:
	_is_attacking = true
	_attack_direction = direction
	_move_vector = direction * 10


func play_animation(direction: Vector2) -> void:
	direction = direction.round()
	if direction.x < 0:
		animation_player.play("Swing_Left")
	elif direction.x > 0:
		animation_player.play("Swing_Right")
	elif direction.y < 0:
		animation_player.play("Swing_Up")
	elif direction.y > 0:
		animation_player.play("Swing_Down")
