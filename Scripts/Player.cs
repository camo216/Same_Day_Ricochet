using Godot;
using System;
[Tool]

public partial class Player : CharacterBody2D
{
	private Vector2 Circlevector;
	private int speed = 500;

	private void UpdateVelocity()
	{
		Velocity = Input.GetVector("left", "right", "up", "down") * speed;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateVelocity();
		MoveAndSlide();

		QueueRedraw();
	}

	public override void _Draw()
	{
		// Your draw commands here.
		Circlevector = new Vector2(0,0);
		DrawArc(Circlevector, 20, 0, 360, 100, Color.Color8(0, 255, 255, 255), 1, true);



	}
}
