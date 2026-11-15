using Godot;
using System;
using System.Drawing;
using Color = Godot.Color;

public partial class BlueWall : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.

	
	private Color lightBlue = new Color(0, 153, 178, 1);
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		QueueRedraw();
	}

    public override void _Draw()
    {
		DrawRect(new Rect2(0, -10, 20, 20), lightBlue, false);

	}
}
