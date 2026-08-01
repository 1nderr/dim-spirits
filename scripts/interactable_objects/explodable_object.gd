class_name ExplodableObject
extends InteractableObject

@export var break_effect_scene: PackedScene
@export var loot_component: LootComponent


func interact(interactor: Node) -> void:
	if not (interactor is ExplosionEffect):
		return

	hide()

	var break_effect: SmokeEffect = break_effect_scene.instantiate()
	get_parent().add_child(break_effect)
	break_effect.global_position = global_position
	await break_effect.animation_player.animation_finished

	loot_component.spawn_loot(global_position)

	queue_free()
