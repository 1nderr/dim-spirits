extends Node

const ITEM_TABLE_PATH := "res://data/item_table.json"

var _items: Dictionary[int, ItemData] = {}


func _ready() -> void:
	var table := _read_table(ITEM_TABLE_PATH)
	for id: String in table:
		_items[id.to_int()] = ItemData.from_dictionary(table[id])


func get_item_sprite(id: int) -> Texture2D:
	return load(_items[id].sprite_path)


func get_item_data(id: int) -> ItemData:
	return _items[id]


func _read_table(path: String) -> Dictionary:
	var file := FileAccess.open(path, FileAccess.READ)
	if file == null:
		push_error("Could not open %s: %s" % [path, error_string(FileAccess.get_open_error())])
		return {}

	var parsed: Variant = JSON.parse_string(file.get_as_text())
	if parsed is not Dictionary:
		push_error("%s is not a JSON object" % path)
		return {}

	return parsed
