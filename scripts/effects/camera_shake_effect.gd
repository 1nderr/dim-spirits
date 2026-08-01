class_name CameraShakeEffect
extends Node

const SHAKE_DECAY := 1.0
const SHAKE_MAX_OFFSET := Vector2(100, 100)
const SHAKE_MAX_ROTATION := 0.2
const TRAUMA_STRENGTH := 2.0

@export var noise: FastNoiseLite

var _camera: GameCamera
var _current_trauma := 0.0
var _noise_y := 0.0
var _is_shaking := false


func _ready() -> void:
	_camera = get_tree().get_first_node_in_group("camera")
	noise.seed = randi()


func _process(delta: float) -> void:
	if _current_trauma <= 0.0:
		_is_shaking = false
		return
	_shake(delta)


func add_trauma(trauma: float) -> void:
	if _is_shaking:
		return
	_is_shaking = true
	_current_trauma = minf(_current_trauma + trauma, 1.0)


func _shake(delta: float) -> void:
	_current_trauma = maxf(_current_trauma - SHAKE_DECAY * delta, 0.0)
	var amount := pow(_current_trauma, TRAUMA_STRENGTH)
	_noise_y += 1.0
	_camera.rotation = SHAKE_MAX_ROTATION * amount * noise.get_noise_2d(0, _noise_y)
	_camera.offset = (
		Vector2(
			SHAKE_MAX_OFFSET.x * noise.get_noise_2d(1000, _noise_y),
			SHAKE_MAX_OFFSET.y * noise.get_noise_2d(2000, _noise_y)
		)
		* amount
	)
