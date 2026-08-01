extends Node

const LOOT_TABLE_PATH := "res://data/loot_table.json"

var _scene_paths: Array[String] = []


func _ready() -> void:
	var file := FileAccess.open(LOOT_TABLE_PATH, FileAccess.READ)
	if file == null:
		push_error(
			"Could not open %s: %s" % [LOOT_TABLE_PATH, error_string(FileAccess.get_open_error())]
		)
		return

	var parsed: Variant = JSON.parse_string(file.get_as_text())
	if parsed is not Dictionary:
		push_error("%s is not a JSON object" % LOOT_TABLE_PATH)
		return

	for id: String in parsed:
		_scene_paths.append(parsed[id]["scenePath"])


## Returns a new instance of a random entry from the loot table.
func get_loot() -> Loot:
	if _scene_paths.is_empty():
		return null

	var loot_scene: PackedScene = load(_scene_paths[randi() % _scene_paths.size()])
	return loot_scene.instantiate()
