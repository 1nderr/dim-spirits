class_name AttackState
extends State

@export var attack_controller: AttackController

var _entity: Entity


func _ready() -> void:
	_entity = get_parent().get_parent()
	_entity.hurtbox_component.hit.connect(_on_hit)
	attack_controller.done.connect(_on_done)


func enter() -> void:
	attack_controller.attack(_entity.direction)


func _on_hit() -> void:
	attack_controller.stop()
	transition(StateType.DAMAGED)


func _on_done() -> void:
	transition(StateType.IDLE)
