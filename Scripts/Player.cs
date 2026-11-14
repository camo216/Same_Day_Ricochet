using Godot;
using System;
//[Tool]

public partial class Player : CharacterBody2D
{
	private Vector2 drawVector = new Vector2(0, 0);
	private const int speed = 500;

	private bool onCooldown = false;
    private const double cooldownTime = 0.75;
    private const double parryTime = 0.5;
    private double cooldown = 1;
	//private double cooldownRed = 0.5;
	//private double cooldownGreen = 0.5;
	//private double cooldownBlue = 0.5;

	private Color parryColor = Color.Color8(0, 0, 0, 0);
	private bool parryRed;
	private bool parryGreen;
	private bool parryBlue;

	private void UpdateVelocity()
	{
		Velocity = Input.GetVector("left", "right", "up", "down") * speed;
	}

	private void UpdateCooldown(double delta)
    {
		cooldown -= delta;
        if (cooldown <= 0)
        {
			cooldown = cooldownTime;
			onCooldown = false;
        }
        else if (cooldown - parryTime <= 0)
        {
            parryRed = false;
            parryGreen = false;
            parryBlue = false;
            parryColor = Color.Color8(0, 0, 0, 0);
        }
        //if (parryRed)
        //{
        //    cooldownRed -= delta;
        //    if (cooldownRed <= 0)
        //    {
        //        parryRed = false;
        //        cooldownRed = cooldownTime;
        //    }
        //}
        //if (parryBlue)
        //{
        //    cooldownBlue -= delta;
        //    if (cooldownBlue <= 0)
        //    {
        //        parryBlue = false;
        //        cooldownBlue = cooldownTime;
        //    }
        //}
        //if (parryGreen)
        //{
        //    cooldownGreen -= delta;
        //    if (cooldownGreen <= 0)
        //    {
        //        parryGreen = false;
        //        cooldownGreen = cooldownTime;
        //    }
        //}
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!onCooldown)
		{
			if (Input.IsActionPressed("parry_red"))
			{
				parryRed = true;
				onCooldown = true;
                parryColor = Color.Color8(255, 0, 0, 200);
			}

			else if (Input.IsActionPressed("parry_green"))
			{
				parryGreen = true;
                onCooldown = true;
                parryColor = Color.Color8(0, 255, 0, 200);
			}


			else if (Input.IsActionPressed("parry_blue"))
			{
				parryBlue = true;
                onCooldown = true;
                parryColor = Color.Color8(0, 0, 255, 200);
			}
		}

		if (onCooldown)
		{
			UpdateCooldown(delta);
		}

		UpdateVelocity();
		MoveAndSlide();

		QueueRedraw();
	}

	public override void _Draw()
	{
		// Your draw commands here.
		
		DrawArc(drawVector, 20, 0, 10, 100, Color.Color8(0, 255, 255, 255), 1, true);
		DrawCircle(drawVector, 20, parryColor, true);
	}
}
