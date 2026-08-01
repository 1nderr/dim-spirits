class_name AttackController
extends Node2D

signal done

@export var attack_scene: PackedScene

var _attack: Attack


func attack(direction: Vector2) -> void:
	_attack = attack_scene.instantiate()
	get_parent().add_child(_attack)

	if direction.y < 0:
		get_parent().move_child(_attack, 0)

	_attack.done.connect(_on_done)
	_attack.global_position = global_position
	_attack.use(direction, get_parent() as Entity)


func stop() -> void:
	if not is_instance_valid(_attack):
		return
	_attack.queue_free()


func _on_done() -> void:
	done.emit()
