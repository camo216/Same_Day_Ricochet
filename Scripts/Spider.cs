using Godot;
using System;

public partial class Spider : Enemy
{
    private EnemyManager manager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        manager = GetParent<EnemyManager>();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void Move(double delta)
	{
        Velocity = direction.Normalized() * speed;
        MoveAndSlide();
    }
}
