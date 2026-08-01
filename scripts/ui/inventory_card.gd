class_name InventoryCard
extends Control

signal item_selected(item_id: int)

@export var item_sprite: Sprite2D

var _item_id: int


func _ready() -> void:
	gui_input.connect(_on_gui_input)


func set_item(item_id: int) -> void:
	_item_id = item_id
	item_sprite.texture = ItemStore.get_item_sprite(item_id)


func _on_gui_input(event: InputEvent) -> void:
	if event.is_action_pressed("left_click"):
		item_selected.emit(_item_id)
