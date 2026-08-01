class_name HitParticles
extends CPUParticles2D

@export var animation_player: AnimationPlayer


func emit(emit_direction: Vector2) -> void:
	direction = emit_direction
	animation_player.play("Hit")
