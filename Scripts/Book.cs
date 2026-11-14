using Godot;
using System;

public partial class Book : Enemy
{
    private EnemyManager manager;

    [Export] private Vector2 direction = new Vector2(0, 0);
    [Export] private float speed = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        manager = GetParent<EnemyManager>();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void Move()
    {
        Velocity = direction.Normalized() * speed;
        MoveAndSlide();
    }
}
