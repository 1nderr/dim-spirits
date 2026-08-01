class_name EnemyController
extends Controller

@export var trigger_area: Area2D
@export var attack_area: Area2D
@export var cooldown_timer: Timer

var _player: Player
var _triggered := false
var _target_in_range := false
var _on_cooldown := false


func _ready() -> void:
	_player = get_tree().get_first_node_in_group("player")
	trigger_area.body_entered.connect(_on_trigger_area_entered)
	attack_area.body_entered.connect(_on_attack_area_entered)
	attack_area.body_exited.connect(_on_attack_area_exited)
	cooldown_timer.timeout.connect(_on_cooldown_timer_timeout)


func get_movement_input() -> Vector2:
	if not is_instance_valid(_player) or not _triggered:
		return Vector2.ZERO

	return global_position.direction_to(_player.global_position).normalized()


func get_attack_input() -> bool:
	var can_attack := _target_in_range and not _on_cooldown

	if can_attack:
		_on_cooldown = true
		cooldown_timer.start()

	return can_attack


func get_attack_target() -> Entity:
	return _player


func get_roll_input() -> bool:
	return false


func get_item_input() -> bool:
	return false


func _on_trigger_area_entered(_other: Node2D) -> void:
	_triggered = true


func _on_attack_area_entered(_other: Node2D) -> void:
	_target_in_range = true


func _on_attack_area_exited(_other: Node2D) -> void:
	_target_in_range = false


func _on_cooldown_timer_timeout() -> void:
	_on_cooldown = false
