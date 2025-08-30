using Godot;

/// <summary>
/// Handles the Game Over UI and related events.
/// </summary>
public partial class GameOver : Control
{
    [Export] private AudioStreamPlayer _gameOverSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // Connects the GameOver event when the plane dies.
        SignalManager.Instance.Connect(SignalManager.SignalName.OnPlaneDied, Callable.From(OnGameOver));
	}

	/// <summary>
	/// Returns to the Main Menu by pressing the Q key.
	/// </summary>
	private void OnGameOver()
	{
		Visible = true; // Makes the UI visible.

        _gameOverSound.Play();

        // Returns to the main menu when the 'quit' action is pressed.
        if (Input.IsActionJustPressed("quit")) GameManager.LoadMain();
    }
}