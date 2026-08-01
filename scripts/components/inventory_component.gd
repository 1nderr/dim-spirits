class_name InventoryComponent
extends Node2D

signal item_equipped(item_id: int)

@export var inventory_ui_scene: PackedScene

var items: Array[int] = []

var _equipped_item: Item


func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("inventory"):
		get_tree().get_first_node_in_group("main").add_child(inventory_ui_scene.instantiate())


func equip_item(id: int) -> void:
	if items.is_empty():
		return
	if _equipped_item != null:
		_equipped_item.queue_free()

	var item_data := ItemStore.get_item_data(id)

	var item_scene: PackedScene = load(item_data.scene_path)
	_equipped_item = item_scene.instantiate()
	add_child(_equipped_item)
	_equipped_item.global_position = global_position

	item_equipped.emit(id)


func use_equipped_item() -> void:
	if _equipped_item != null:
		_equipped_item.use(get_parent() as Entity)
