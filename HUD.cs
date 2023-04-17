using Godot;
using System;

public partial class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGameEventHandler();
    
	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Label score = GetNode<Label>("ScoreLabel");
        score.Hide();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();
        GetNode<Timer>("MessageTimer").Start();
    }

    public void ShowScore()
    {
        Label label = GetNode<Label>("ScoreLabel");
        label.Text = "0";
        label.Show();
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");

        var messageTimer = GetNode<Timer>("MessageTimer");
        await ToSignal(messageTimer, Timer.SignalName.Timeout);

        var message = GetNode<Label>("Message");
        message.Text = "Game !!!\nOver !!!!!";
        message.Show();

        await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
        GetNode<Button>("StartButton").Show();
    }

    public void UpdateScore(int score)
    {
        var scoreLabel = GetNode<Label>("ScoreLabel");
        scoreLabel.Text = score.ToString();
        scoreLabel.Show();
    }

    private void OnStartButtonPressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal(SignalName.StartGame);
    }

    private void OnMessageTimerTimeout()
    {
        GetNode<Label>("Message").Hide();
    }
}
