class_name HealthComponent
extends Node

signal health_updated
signal died(position: Vector2)

@export var max_health: float
@export var camera_shake: float
@export var death_effect_scene: PackedScene

var _current_health: float
var _camera: GameCamera


func _ready() -> void:
	_current_health = max_health
	_camera = get_tree().get_first_node_in_group("camera")


func damage(amount: float) -> void:
	_current_health = maxf(_current_health - amount, 0.0)

	health_updated.emit()

	_check_death.call_deferred()


func heal(amount: float) -> void:
	_current_health = minf(_current_health + amount, max_health)

	health_updated.emit()


func get_health_percent() -> float:
	return 0.0 if max_health <= 0.0 else minf(_current_health / max_health, 1.0)


func get_current_health() -> float:
	return _current_health


func get_max_health() -> float:
	return max_health


func _check_death() -> void:
	if _current_health != 0.0:
		return

	var parent: Node2D = get_parent()
	var death_position := Vector2(parent.global_position.x, parent.global_position.y - 8.0)
	parent.hide()

	var death_effect: Node2D = death_effect_scene.instantiate()
	parent.get_parent().add_child(death_effect)
	death_effect.global_position = death_position
	await get_tree().create_timer(0.3).timeout

	_camera.shake(camera_shake)

	died.emit(death_position)

	parent.queue_free()
