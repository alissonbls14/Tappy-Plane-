using Godot;

/// <summary>
/// Provides global game management through a singleton instance, centralizing scene transitions and constants.
/// </summary>
public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    public const float SPEED = 120.0f; // Movement speed of pipes.

    private PackedScene _gameScene = GD.Load<PackedScene>("res://Scenes/Game/Game.tscn");
    private PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");
    private PackedScene _transitionScene = GD.Load<PackedScene>("res://Scenes/Scene_Transition/SceneTransition.tscn");
    private PackedScene _nextScene; // Loads the next scene.

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() => Instance = this;

    /// <summary>
    /// Instanciates the next scene.
    /// </summary>
    /// <returns>
    /// An instance of the next scene.
    /// </returns>
    public static PackedScene GetNextScene() => Instance._nextScene;

    /// <summary>
    /// APrepares the specified scene to be loaded by triggering the transition.
    /// </summary>
    /// <param name="ns"> The scene to load next. </param>
    private void NextScene(PackedScene ns)
    {
        _nextScene = ns;

        CanvasLayer transition = (CanvasLayer)_transitionScene.Instantiate();

        AddChild(transition);
    }

    /// <summary>
    /// Loads the main gameplay scene.
    /// </summary>
    public static void LoadGame() => Instance.NextScene(Instance._gameScene);

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
	public static void LoadMain() => Instance.NextScene(Instance._mainScene);
}