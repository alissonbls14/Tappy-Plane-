using Godot;

/// <summary>
/// Controls the parallax autoscroll, making the background sprites move.
/// </summary>
public partial class ParallaxImage : Parallax2D
{
	[Export] private Texture2D _srcImage;
	[Export] private Sprite2D _sprite;
	[Export] private float _speedScale; // 0 - 1

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // Sets the autoscroll based on the speed scale and global game speed.
        Autoscroll = new Vector2(-_speedScale * GameManager.SPEED, 0);

        // Calculate the scaling factor to fit the sprite to the viewport height.
        float scaleFactor = GetViewportRect().Size.Y / _srcImage.GetHeight();

		_sprite.Texture = _srcImage;
		_sprite.Scale = new Vector2(scaleFactor, scaleFactor);

		// Adjust the repeat size for proper looping of the parallax texture.
		RepeatSize = new Vector2(_srcImage.GetWidth() * scaleFactor, 0);

        // Connect to the Plane death signal to stop autoscrolling.
        SignalManager.Instance.Connect(SignalManager.SignalName.OnPlaneDied, Callable.From(OnPlaneDied));
	}

    /// <summary>
    /// Stops the autoscroll while keeping the sprites in their current position.
    /// </summary>
    private void OnPlaneDied()
	{
		Vector2 pp = Position;
		Autoscroll = Vector2.Zero;
		Position = pp;
	}
}