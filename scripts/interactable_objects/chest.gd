class_name Chest
extends InteractableObject

@export var item_id: int
@export var item_sprite: Sprite2D
@export var animation_player: AnimationPlayer


func interact(interactor: Node) -> void:
	if not interactable or not (interactor is Player):
		return
	var player := interactor as Player

	interactable = false
	item_sprite.texture = ItemStore.get_item_sprite(item_id)
	animation_player.play("Open")

	player.inventory_component.items.append(item_id)
