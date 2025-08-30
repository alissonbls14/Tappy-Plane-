using Godot;

/// <summary>
/// Manages the in-game Score UI, updating the score label when the player scores.
/// </summary>
public partial class Hud : Control
{
	[Export] private Label _scoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Connects the OnScore signal to update the score label.
		SignalManager.Instance.Connect(SignalManager.SignalName.OnScore, Callable.From(OnScored));
	}

    /// <summary>
    /// Updates the score label with the current score.
    /// </summary>
    private void OnScored() => _scoreLabel.Text = $"{ScoreManager.GetScore():0000}";
}