class_name InventoryUi
extends CanvasLayer

@export var inventory_card_scene: PackedScene
@export var grid_container: GridContainer

var _player: Player


func _ready() -> void:
	_player = get_tree().get_first_node_in_group("player")
	get_tree().paused = true
	_build_inventory()


func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("inventory"):
		_close()


func _build_inventory() -> void:
	for item_id in _player.inventory_component.items:
		var inventory_card: InventoryCard = inventory_card_scene.instantiate()
		grid_container.add_child(inventory_card)
		inventory_card.set_item(item_id)
		inventory_card.item_selected.connect(_on_item_selected)


func _close() -> void:
	get_tree().paused = false
	queue_free()


func _on_item_selected(item_id: int) -> void:
	_player.inventory_component.equip_item(item_id)
	_close()
