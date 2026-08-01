class_name UiHeart
extends Control

@export var animation_player: AnimationPlayer


func _ready() -> void:
	empty()


func fill() -> void:
	animation_player.play("Filled")


func half_fill() -> void:
	animation_player.play("HalfFilled")


func empty() -> void:
	animation_player.play("Empty")
