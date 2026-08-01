class_name GameCamera
extends Camera2D

@export var camera_shake_effect: CameraShakeEffect

var _target_position := Vector2.ZERO


func _ready() -> void:
	make_current()


func _process(delta: float) -> void:
	_find_target_position()
	global_position = global_position.lerp(_target_position, 1.0 - exp(-delta * 20.0))


func shake(amount: float) -> void:
	camera_shake_effect.add_trauma(amount)


func _find_target_position() -> void:
	var player := get_tree().get_first_node_in_group("player") as Node2D
	if not is_instance_valid(player):
		return
	_target_position = player.global_position
