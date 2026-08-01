class_name StateType

enum {
	IDLE,
	MOVE,
	ATTACK,
	DAMAGED,
	STUNNED,
	ROLL,
	ITEM,
}

## Lowercased state names, indexed by the enum above. A state machine looks up
## its children by these names.
const NAMES := ["idle", "move", "attack", "damaged", "stunned", "roll", "item"]


static func get_state_name(type: int) -> String:
	return NAMES[type]
