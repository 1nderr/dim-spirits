class_name SpriteFlashEffect
extends Node

@export var sprite: Sprite2D

var _tween: Tween


func flash() -> void:
	_tween = create_tween()
	_tween.tween_property(sprite, "modulate:v", 1.0, 0.2).from(30.0)
