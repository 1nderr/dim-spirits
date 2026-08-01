class_name PlayerHealthBar
extends Control

@export var heart_scene: PackedScene
@export var container: HBoxContainer

var _hearts: Array[UiHeart] = []
var _player: Player


func _ready() -> void:
	_player = get_tree().get_first_node_in_group("player")
	if not is_instance_valid(_player):
		return

	_player.health_component.health_updated.connect(_on_health_updated)

	initialize_hearts(_player.health_component.get_max_health())


func initialize_hearts(max_health: float) -> void:
	for _i in int(max_health):
		var heart: UiHeart = heart_scene.instantiate()
		_hearts.append(heart)
		container.add_child(heart)
	set_hearts(max_health, max_health)


func set_hearts(max_health: float, health: float) -> void:
	for i in int(max_health):
		if health == 0.5:
			_hearts[i].half_fill()
			health = 0.0
		elif health > 0.0:
			_hearts[i].fill()
			health -= 1.0
		else:
			_hearts[i].empty()


func _on_health_updated() -> void:
	set_hearts(_player.health_component.get_max_health(), _player.health_component.get_current_health())
