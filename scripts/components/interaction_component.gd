class_name InteractionComponent
extends RayCast2D

var _player: Player


func _ready() -> void:
	_player = get_tree().get_first_node_in_group("player")


func _process(_delta: float) -> void:
	if not is_instance_valid(_player):
		return

	var collider := get_collider()
	if (
		collider is InteractableObject
		and Input.is_action_just_pressed("interact")
		and _player.direction.round() == Vector2.UP
	):
		(collider as InteractableObject).interact(get_parent())
