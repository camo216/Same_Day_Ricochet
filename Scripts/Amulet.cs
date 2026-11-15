using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;

public partial class Amulet : Enemy
{

	private EnemyManager manager;

    // Called when the node enters the scene tree for the first time.
    public override void Inst()
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

    public override void DoAction(double delta)
    {

    }
}
