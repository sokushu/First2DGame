using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        var animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animSprite.Play();
        string[] mobTypes = animSprite.SpriteFrames.GetAnimationNames();
        animSprite.Animation = mobTypes[GD.Randi() % mobTypes.Length];
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
