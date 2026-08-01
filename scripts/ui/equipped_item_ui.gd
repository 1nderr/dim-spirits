class_name EquippedItemUi
extends Control

@export var item_sprite: Sprite2D

var _player: Player


func _ready() -> void:
	_player = get_tree().get_first_node_in_group("player")
	_player.inventory_component.item_equipped.connect(_on_item_equipped)


func _on_item_equipped(item_id: int) -> void:
	item_sprite.texture = ItemStore.get_item_sprite(item_id)
