class_name EntityLootComponent
extends LootComponent

@export var health_component: HealthComponent


func _ready() -> void:
	health_component.died.connect(spawn_loot)
