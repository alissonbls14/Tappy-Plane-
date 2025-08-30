using Godot;

/// <summary>
/// Handles the Main Menu, updating UI labels and starting the game.
/// </summary>
public partial class Main : Control
{
	[Export] Label _highscoreValue;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Displays the current high score in the label.
		float highscore = ScoreManager.GetHighScore();

		_highscoreValue.Text = $"{highscore:0000}";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Starts the game when the "fly" action is just pressed.
		if (Input.IsActionJustPressed("fly")) GameManager.LoadGame();
	}
}