class_name HeartLoot
extends Loot

@export var heal_amount: int


func on_pickup(player: Player) -> void:
	player.health_component.heal(heal_amount)
