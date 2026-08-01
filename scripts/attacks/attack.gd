@abstract
class_name Attack
extends Node2D

signal done

@export var animation_player: AnimationPlayer


@abstract func use(direction: Vector2, entity: Entity) -> void
