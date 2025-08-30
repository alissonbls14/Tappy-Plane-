using Godot;

/// <summary>
/// Singleton responsible for handling score and high score across the game.
/// </summary>
public partial class ScoreManager : Node
{
    public static ScoreManager Instance { get; private set; }

    private static uint _score = 0;
    private static uint _highScore = 0;

    private const string HIGHSCORE_FILE = "user://tappy.save"; // Path fot the score save file.

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Instance = this;

        LoadHighScore();
    }

    /// <summary>
    /// Shows the current score during the game.
    /// </summary>
    /// <returns> Score. </returns>
    public static uint GetScore() => _score;

    /// <summary>
    /// Gets the current highsscore
    /// </summary>
    /// <returns> Highscore </returns>
    public static uint GetHighScore() => _highScore;

    /// <summary>
    /// Updates the score and, if it's greater than the current high score, updates the high score as well.
    /// </summary>
    /// <param name="value"> New score value. </param>
    public static void SetScore(uint value)
    {
        _score = value;

        if (_score > _highScore)
        {
            _highScore = _score;

            SaveHighScore();
        }

        SignalManager.EmitOnScore();
    }

    /// <summary>
    /// Resets the Score back to 0.
    /// </summary>
    public static void ResetScore() => SetScore(0);

    /// <summary>
    /// Increments the Score by 1.
    /// </summary>
    public static void IncrementScore() => SetScore(GetScore() + 1);

    /// <summary>
    /// Loads the saved high score from file. Defaults to 0 if the file is empty or invalid.
    /// </summary>
    private static void LoadHighScore()
    {
        string content = FileManager.LoadFromFile(HIGHSCORE_FILE);

        if (!uint.TryParse(content, out _highScore)) _highScore = 0;
    }

    /// <summary>
    /// Saves the high score to a file.
    /// </summary>
    private static void SaveHighScore() => FileManager.SaveToFile(HIGHSCORE_FILE, _highScore);
}