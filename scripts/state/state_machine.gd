extends Node

@export var initial_state: State

var _current_state: State
var _states: Dictionary[String, State] = {}


func _ready() -> void:
	for child in get_children():
		if child is State:
			_states[child.name.to_lower()] = child
			child.transitioned.connect(_on_state_transition)

	if initial_state != null:
		initial_state.enter()
		_current_state = initial_state


func _process(delta: float) -> void:
	if _current_state != null:
		_current_state.update(delta)


func _on_state_transition(source_state: State, new_state_name: String) -> void:
	if source_state != _current_state:
		return

	var new_state: State = _states.get(new_state_name)
	if new_state == null:
		return

	if _current_state != null:
		_current_state.exit()
	new_state.enter()
	_current_state = new_state
