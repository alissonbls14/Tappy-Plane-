using Godot;

/// <summary>
/// Controls the Plane's vertical movement.
/// </summary>
public partial class Plane : CharacterBody2D
{
	[Export] private AnimationPlayer _animationPlayer;
	[Export] private AnimatedSprite2D _planeSprite;
	[Export] private AudioStreamPlayer _engineSound;

	private const float GRAVITY = 800.0f; // Gravity force.
	private const float POWER = -300.0f; // Jump power.

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// NOT USED.
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.Y += GRAVITY * (float)delta; // Applies gravity to the vertical velocity based on delta time.

        if (Input.IsActionJustPressed("fly")) // Checks if the "fly" action was just pressed.
		{
			velocity.Y = POWER;

			_animationPlayer.Play("jump"); // Plays the jump animation.
		}

        Velocity = velocity;

        MoveAndSlide();

		if (IsOnFloor()) Die();
    }

    /// <summary>
    /// Handles the Plane's death, stopping physics and animations, and emitting the death signal.
    /// </summary>
    public void Die()
	{
		SetPhysicsProcess(false);

		_engineSound.Stop();
		_planeSprite.Stop();

        SignalManager.EmitOnPlaneDied();
    }
}