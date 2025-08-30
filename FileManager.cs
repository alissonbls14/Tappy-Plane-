using Godot;

/// <summary>
/// Provides global management for saving and loading data.
/// </summary>
public partial class FileManager : Node
{
	public static FileManager Instance { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Instance == null) Instance = this;
		else QueueFree();
	}

	/// <summary>
	/// Saves the specified content to the given path in the user's file system.
	/// </summary>
	/// <param name="path"> Target file path where the content will be saved. </param>
	/// <param name="content"> Data to be stored in the file. </param>
	public static void SaveToFile(string path, object content)
	{
		using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);

		if (file != null)
		{
			string text = content.ToString(); // Turns the content on a readable string.

			file.StoreString(text);

			GD.Print($"[FileManager] Saved content '{text}' to {path}");
		}
	}

    /// <summary> 
    /// Loads the file content from the specified path.
    /// </summary>
    /// <param name="path"> Target file path where the content will be loaded. </param>
    /// <returns> 
    /// An empty string if the file does not exist;
    /// otherwise, the file content as text.
    /// </returns>
    public static string LoadFromFile(string path)
	{
		if (!FileAccess.FileExists(path)) return string.Empty;

		using FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Read);

		string content = file.GetAsText().Trim(); // Gets the content previously saved in the file as a text.

		GD.Print($"[File Access] Loaded content '{content}' from {path}");

		return content;
	}
}