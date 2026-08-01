class_name PickupComponent
extends Area2D

@export var sprite_flash_effect: SpriteFlashEffect


func _ready() -> void:
	body_entered.connect(_on_body_entered)
	sprite_flash_effect.flash()


func _on_body_entered(body: Node2D) -> void:
	if body is Player:
		var loot := get_parent() as Loot
		loot.on_pickup(body as Player)
		loot.queue_free()
