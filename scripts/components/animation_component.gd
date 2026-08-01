class_name AnimationComponent
extends Node

@export var animation_player: AnimationPlayer


func play_idle(direction: Vector2) -> void:
	_play_directional("Idle", direction)


func play_move(direction: Vector2) -> void:
	_play_directional("Move", direction)


func play_roll(direction: Vector2) -> void:
	_play_directional("Roll", direction)


func play_attack(direction: Vector2) -> void:
	_play_directional("Attack", direction)


func wait_for_animation() -> void:
	await animation_player.animation_finished


## Plays "<prefix>_Left" / "_Right" / "_Up" / "_Down", picking the axis the
## direction leans towards most. Horizontal wins over vertical on a tie.
func _play_directional(prefix: String, direction: Vector2) -> void:
	direction = direction.round()
	if direction.x < 0:
		animation_player.play("%s_Left" % prefix)
	elif direction.x > 0:
		animation_player.play("%s_Right" % prefix)
	elif direction.y < 0:
		animation_player.play("%s_Up" % prefix)
	elif direction.y > 0:
		animation_player.play("%s_Down" % prefix)
