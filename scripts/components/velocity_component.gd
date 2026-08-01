class_name VelocityComponent
extends Node

const ACCELERATION_SMOOTHING := 20.0

@export var max_speed: float

var _velocity := Vector2.ZERO
var _original_speed: float


func _ready() -> void:
	_original_speed = max_speed


func calculate_velocity(direction: Vector2, delta: float) -> Vector2:
	_velocity = _velocity.lerp(direction * max_speed, 1.0 - exp(-delta * ACCELERATION_SMOOTHING))
	if _velocity.round() == Vector2.ZERO:
		_velocity = Vector2.ZERO
	return _velocity


func calculate_biased_velocity(direction: Vector2, delta: float, bias: Vector2) -> Vector2:
	return calculate_velocity(direction, delta) + bias


func reset_speed() -> void:
	max_speed = _original_speed
