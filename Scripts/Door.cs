using Godot;
using System;

public partial class Door : CharacterBody2D
{
    private Color doorColor = new Color(169, 94, 0, 1);
    public bool cleared = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (cleared)
        {
            CollisionLayer = 2;
        }
	}

    public override void _Draw()
    {
        if (!cleared)
        {
            DrawRect(new Rect2(0, -10, 20, 20), doorColor, false);
        }
    }
}
