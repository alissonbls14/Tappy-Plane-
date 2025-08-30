using Godot;

/// <summary>
/// Global Class for managing Signals (Events), avoiding repetition and simplifying event handling.
/// </summary>
public partial class SignalManager : Node
{
    [Signal] public delegate void OnPlaneDiedEventHandler();
    [Signal] public delegate void OnScoreEventHandler();

    public static SignalManager Instance { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() => Instance = this;

    public static void EmitOnPlaneDied() => Instance.EmitSignal(SignalName.OnPlaneDied);

    public static void EmitOnScore() => Instance.EmitSignal(SignalName.OnScore);
}