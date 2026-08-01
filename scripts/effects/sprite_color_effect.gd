class_name SpriteColorEffect
extends Node

@export var sprite: Sprite2D
@export var color: Color


func set_color() -> void:
	sprite.modulate = color


func reset_color() -> void:
	sprite.modulate = Color.WHITE
