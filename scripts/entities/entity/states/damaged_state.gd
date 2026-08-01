class_name DamagedState
extends State

@export var knockback_strength: float
@export var camera_shake_amount: float
@export var sprite_flash_effect: SpriteFlashEffect
@export var hit_particles_scene: PackedScene

var _entity: Entity
var _knockback_vector := Vector2.ZERO
var _camera: GameCamera
var _is_hit := false


func _ready() -> void:
	_entity = get_parent().get_parent()
	_camera = get_tree().get_first_node_in_group("camera")


func enter() -> void:
	_entity.hurtbox_component.is_invincible = true

	Engine.time_scale = 0.05
	await get_tree().create_timer(0.0075).timeout
	Engine.time_scale = 1.0

	_entity.animation_component.play_idle(_entity.direction)
	sprite_flash_effect.flash()

	if hit_particles_scene != null:
		var hit_particles: HitParticles = hit_particles_scene.instantiate()
		add_child(hit_particles)
		hit_particles.emit(_entity.hurtbox_component.hit_direction)

	_camera.shake(camera_shake_amount)
	_knockback_vector = _entity.hurtbox_component.hit_direction * knockback_strength
	_entity.velocity_component.max_speed = knockback_strength

	_is_hit = true


func exit() -> void:
	_is_hit = false
	_entity.hurtbox_component.is_invincible = false
	_entity.velocity_component.reset_speed()


func update(delta: float) -> void:
	if not _is_hit:
		return

	_knockback_vector = _knockback_vector.lerp(Vector2.ZERO, 0.075)

	if _knockback_vector.round() == Vector2.ZERO:
		transition(StateType.IDLE)
		return

	_entity.velocity = _entity.velocity_component.calculate_biased_velocity(
		_entity.hurtbox_component.hit_direction, delta, _knockback_vector
	)
	_entity.move_and_slide()
