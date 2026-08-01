class_name Bomb
extends Node2D

signal exploded

const DETONATION_TIME := 1.5
const CAMERA_SHAKE := 0.5

@export var animation_player: AnimationPlayer
@export var explosion_effect: PackedScene


func _ready() -> void:
	await get_tree().create_timer(DETONATION_TIME).timeout
	animation_player.play("Explode")
	await animation_player.animation_finished

	var explosion: Node2D = explosion_effect.instantiate()
	get_parent().add_child(explosion)
	explosion.global_position = global_position

	var camera: GameCamera = get_tree().get_first_node_in_group("camera")
	camera.shake(CAMERA_SHAKE)

	exploded.emit()
	queue_free()
