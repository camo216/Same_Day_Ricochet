using Godot;
using System;
[Tool]

public partial class StartButton : Button
{
	private MainMenu menu;
	private Rect2 rect = new Rect2(0, 0, 500, 100);
	private int textWidth = 200;
	private int textSize = 50;
	private Vector2 textVector;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		menu = (MainMenu)GetParent();
        Pressed += StartGame;

		textVector = new Vector2(500 - textWidth, 100 - textSize);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
    }

	public override void _Draw()
	{
		DrawRect(rect, Color.Color8(0, 200, 200)); 
		DrawString(ThemeDB.FallbackFont, textVector, "START",
               HorizontalAlignment.Center, textWidth, textSize);
        base._Draw();
	}


	private void StartGame()
	{
		menu.LoadLevel();
	}
}
