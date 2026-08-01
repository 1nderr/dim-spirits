class_name HitboxComponent
extends Area2D

signal hit
signal interacted

@export var damage: float
@export var can_stun: bool


func _ready() -> void:
	area_entered.connect(_on_area_entered)


func _on_area_entered(area: Area2D) -> void:
	if area is HurtboxComponent:
		var hurtbox := area as HurtboxComponent
		if can_stun:
			hurtbox.stun()
		else:
			hurtbox.damage(damage, global_position)
		hit.emit()
	elif area is InteractionAreaComponent:
		(area as InteractionAreaComponent).interact(get_parent())
		interacted.emit()
