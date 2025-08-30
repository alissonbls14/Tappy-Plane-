using Godot;

/// <summary>
/// Controls the Pipes' movement and collision behavior.
/// </summary>
public partial class Pipes : Node2D
{
	[Export] private Area2D _lowerPipe;
	[Export] private Area2D _upperPipe;
	[Export] private Area2D _laser;
	[Export] private AudioStreamPlayer2D _scoreSound;

	private VisibleOnScreenNotifier2D _OnScreenNotifier;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_OnScreenNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
        _OnScreenNotifier.ScreenExited += OnScreenExited;
		_lowerPipe.BodyEntered += OnPipeBodyEntered;
		_upperPipe.BodyEntered += OnPipeBodyEntered;
		_laser.BodyEntered += OnLaserBodyEntered;

		SignalManager.Instance.Connect(SignalManager.SignalName.OnPlaneDied, Callable.From(OnPlaneDied));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position -= new Vector2(GameManager.SPEED * (float)delta, 0); // Moves the pipe left every frame based on the game speed.
	}

	/// <summary>
	/// Stops the pipe movement when the Plane dies.
	/// </summary>
    public void OnPlaneDied() => SetProcess(false);

    /// <summary>
    /// Calls the Plane's Die() method when it collides with a pipe.
    /// </summary>
    /// <param name="body"> The body that entered the pipe's area. </param>
    private void OnPipeBodyEntered(Node2D body)
	{
		if (body is Plane) (body as Plane).Die(); 
	}

    /// <summary>
    /// Increments the player's score when the Plane passes through the laser.
    /// </summary>
    /// <param name="body"> The body that entered the laser area. </param>
    public void OnLaserBodyEntered(Node2D body)
	{
		if (body is Plane)
		{
			_laser.BodyEntered -= OnLaserBodyEntered;
			_scoreSound.Play();

			ScoreManager.IncrementScore();
        }
	}

    /// <summary>
    /// Removes the Pipe from the scene when it exits
    /// </summary>
    private void OnScreenExited() => QueueFree();
}