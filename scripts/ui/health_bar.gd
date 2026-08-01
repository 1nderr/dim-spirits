class_name HealthBar
extends ProgressBar

@export var health_component: HealthComponent


func _ready() -> void:
	health_component.health_updated.connect(_on_health_updated)
	hide()


func _on_health_updated() -> void:
	value = health_component.get_health_percent()
	show()
