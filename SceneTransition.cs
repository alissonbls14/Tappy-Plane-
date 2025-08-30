using Godot;

/// <summary>
/// Handles playing an animation during scene transitions in the middle of the game.
/// </summary>
public partial class SceneTransition : CanvasLayer
{
	[Export] AnimationPlayer _animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPlayer.AnimationFinished += OnAnimationFinished;
	}

    /// <summary>
    /// Called when the animation finishes; removes this node from the scene tree.
    /// </summary>
    /// <param name="animName"> Name of the animation </param>
    private void OnAnimationFinished(StringName animName) => QueueFree();

    /// <summary>
    /// Switches to the next scene after the fade animation.
    /// </summary>
    private void SwitchScene() => GetTree().ChangeSceneToPacked(GameManager.GetNextScene());
}