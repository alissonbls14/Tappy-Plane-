using Godot;

/// <summary>
/// Class responsible for the main game scene and it's functionalities.
/// </summary>
public partial class Game : Node2D
{
	[Export] private Marker2D _spawnUpper;
	[Export] private Marker2D _spawnLower;
	[Export] private Timer _spawnTimer;
	[Export] private PackedScene _pipesScene;
	[Export] private Node2D _pipesHolder;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScoreManager.ResetScore(); // Resets the score at the start of the game.

		_spawnTimer.Timeout += SpawnPipe; // Connects the spawn event to the timer.

		SignalManager.Instance.Connect(SignalManager.SignalName.OnPlaneDied, Callable.From(GameOver));

		SpawnPipe(); // Spawns the first pipe immediately.
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Allows returning to the main menu at any time.
		if (Input.IsActionJustPressed("quit")) ReturnToMenu();
	}

    /// <summary>
    /// Instantiates a new Pipes scene and adds it as a child of the Pipes holder.
    /// </summary>
    private void SpawnPipe()
	{
		Pipes pipes = _pipesScene.Instantiate<Pipes>();

		_pipesHolder.AddChild(pipes);

		pipes.Position = new Vector2(_spawnLower.Position.X, GetSpawnY());
	}

    /// <summary>
    /// Stops pipe spawning when the player dies.
    /// </summary>
    private void GameOver() => _spawnTimer.Stop();

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    private void ReturnToMenu() => GameManager.LoadMain();

    /// <summary>
    /// Returns a random Y position within the upper and lower spawn bounds for pipe instantiation.
    /// </summary>
    /// <returns>
	/// Random Y coordinate for pipe spawn.
	/// </returns>
    private float GetSpawnY() => (float)GD.RandRange(_spawnUpper.Position.Y, _spawnLower.Position.Y);
}