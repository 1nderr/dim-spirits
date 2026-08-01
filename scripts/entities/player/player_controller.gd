class_name PlayerController
extends Controller


func get_movement_input() -> Vector2:
	return Input.get_vector("move_left", "move_right", "move_up", "move_down").normalized()


func get_attack_input() -> bool:
	return Input.is_action_just_pressed("attack")


func get_attack_target() -> Entity:
	return null


func get_roll_input() -> bool:
	return Input.is_action_just_pressed("roll")


func get_item_input() -> bool:
	return Input.is_action_just_pressed("use_item")
