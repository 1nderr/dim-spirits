class_name Grass
extends InteractableObject

@export var loot_component: LootComponent


func interact(interactor: Node) -> void:
	if interactor is Sword or interactor is ExplosionEffect or interactor is Boomerang:
		loot_component.spawn_loot(global_position)
		queue_free()
