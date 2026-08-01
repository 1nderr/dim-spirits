class_name Boomerang
extends Node2D

signal returned

@export var velocity_component: VelocityComponent
@export var hitbox_component: HitboxComponent
@export var timer: Timer

var thrower: Entity
var start_direction := Vector2.ZERO

var _is_returning := false


func _ready() -> void:
	timer.timeout.connect(_on_hit_or_timeout)
	hitbox_component.hit.connect(_on_hit_or_timeout)
	hitbox_component.interacted.connect(_on_hit_or_timeout)

	# Only hit what the thrower cannot: an enemy's boomerang hits the player and
	# vice versa.
	hitbox_component.set_collision_mask_value(
		Layer.PLAYER, not thrower.hurtbox_component.get_collision_layer_value(Layer.PLAYER)
	)

	hitbox_component.set_collision_mask_value(
		Layer.ENEMIES, not thrower.hurtbox_component.get_collision_layer_value(Layer.ENEMIES)
	)


func _process(delta: float) -> void:
	if _is_returning and thrower.global_position.distance_to(global_position) < 8.0:
		returned.emit()
		queue_free()
		return

	var direction := (
		global_position.direction_to(thrower.global_position) if _is_returning else start_direction
	)
	var velocity := velocity_component.calculate_velocity(direction, delta)
	global_position += velocity * delta


func _on_hit_or_timeout() -> void:
	_is_returning = true
