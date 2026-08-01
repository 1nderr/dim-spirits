class_name HurtboxComponent
extends Area2D

signal hit
signal stunned

@export var health_component: HealthComponent

var is_invincible := false
var hit_direction := Vector2.ZERO


func stun() -> void:
	stunned.emit()


func damage(amount: float, source_position: Vector2) -> void:
	if is_invincible:
		return
	health_component.damage(amount)
	hit_direction = global_position.direction_to(source_position).normalized() * -1
	hit.emit()
