class_name StunnedState
extends State

@export var stun_time: float
@export var sprite_color_effect: SpriteColorEffect

var _entity: Entity


func _ready() -> void:
	_entity = get_parent().get_parent()
	_entity.hurtbox_component.hit.connect(_on_hit)


func enter() -> void:
	_entity.animation_component.play_idle(_entity.direction)
	sprite_color_effect.set_color()
	_entity.velocity = Vector2.ZERO
	await get_tree().create_timer(stun_time).timeout
	transition(StateType.IDLE)


func exit() -> void:
	sprite_color_effect.reset_color()


func _on_hit() -> void:
	transition(StateType.DAMAGED)
