class_name InteractionAreaComponent
extends Area2D


func interact(interactor: Node2D) -> void:
	(get_parent() as InteractableObject).interact(interactor)
