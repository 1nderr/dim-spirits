class_name ItemState
extends State

var _entity: Player


func _ready() -> void:
	_entity = get_parent().get_parent()
	_entity.hurtbox_component.hit.connect(_on_hit)
	_entity.hurtbox_component.stunned.connect(_on_stunned)


func enter() -> void:
	_entity.inventory_component.use_equipped_item()


func update(_delta: float) -> void:
	transition(StateType.IDLE)


func _on_hit() -> void:
	transition(StateType.DAMAGED)


func _on_stunned() -> void:
	transition(StateType.STUNNED)
