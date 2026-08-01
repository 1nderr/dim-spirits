class_name ItemData
extends RefCounted

var name: String
var scene_path: String
var sprite_path: String


static func from_dictionary(data: Dictionary) -> ItemData:
	var item := ItemData.new()
	item.name = data.get("name", "")
	item.scene_path = data.get("scenePath", "")
	item.sprite_path = data.get("spritePath", "")
	return item
