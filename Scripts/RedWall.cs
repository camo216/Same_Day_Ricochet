using Godot;
using System;

public partial class RedWall : CharacterBody2D
{
   
    private Color maroon = new Color(130, 0, 0, 1);
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
        DrawRect(new Rect2(0, -10, 20, 20), maroon, false);

    }
}
