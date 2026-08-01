class_name BoomerangController
extends Item

@export var boomerang_scene: PackedScene

var _is_thrown := false


func use(user: Entity) -> void:
	if _is_thrown:
		return
	_is_thrown = true

	var boomerang: Boomerang = boomerang_scene.instantiate()
	boomerang.returned.connect(_on_returned)
	boomerang.thrower = user
	boomerang.start_direction = user.direction

	get_tree().get_first_node_in_group("entities").add_child(boomerang)
	boomerang.global_position = global_position


func _on_returned() -> void:
	_is_thrown = false
