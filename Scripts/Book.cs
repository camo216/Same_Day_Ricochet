using Godot;
using System;

public partial class Book : Enemy
{

    // Called when the node enters the scene tree for the first time.
    public override void Inst(Node2D newPlayer, BulletManager bullet)
    {
        manager = GetParent<EnemyManager>();
        bulletManager = bullet;
        player = newPlayer;
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
