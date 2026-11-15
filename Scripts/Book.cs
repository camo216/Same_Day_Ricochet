using Godot;
using System;

public partial class Book : Enemy
{
    private Vector2 drawVector = new Vector2(0, 0);

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

    public override void _Draw()
    {
        DrawRect(new Rect2(drawVector, 26, 20), Color.Color8(255, 255, 255, 255), false, 1);
        DrawLine(new Vector2(13, 20), new Vector2(13, 0), Color.Color8(255, 255, 255, 255), 1);
    }

}
