class_name BombBag
extends Item

const MAX_BOMBS := 2

@export var bomb_scene: PackedScene

var _bomb_count := 0


func use(user: Entity) -> void:
	if _bomb_count == MAX_BOMBS:
		return
	_bomb_count += 1

	var bomb: Bomb = bomb_scene.instantiate()
	bomb.exploded.connect(_on_exploded)
	get_tree().get_first_node_in_group("entities").add_child(bomb)
	bomb.global_position = user.global_position


func _on_exploded() -> void:
	_bomb_count -= 1
