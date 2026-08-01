class_name LootComponent
extends Node

@export_range(0.0, 1.0) var drop_chance := 0.5


func spawn_loot(spawn_position: Vector2) -> void:
	if randf() > drop_chance:
		return

	var loot_layer := get_tree().get_first_node_in_group("loot")
	if loot_layer == null:
		return

	var loot := LootStore.get_loot()
	if loot == null:
		return

	loot_layer.add_child.call_deferred(loot)
	loot.global_position = spawn_position
