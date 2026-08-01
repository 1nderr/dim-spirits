class_name Entity
extends CharacterBody2D

@export var animation_component: AnimationComponent
@export var hurtbox_component: HurtboxComponent
@export var velocity_component: VelocityComponent
@export var controller: Controller

var direction := Vector2.DOWN


func _process(_delta: float) -> void:
	var input_vector := controller.get_movement_input()
	if input_vector != Vector2.ZERO:
		direction = input_vector
