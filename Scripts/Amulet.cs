using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;
public partial class Amulet : Enemy
{
    private Vector2 drawVector = new Vector2(0, 0);
    private EnemyManager manager;

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
        Vector2 chainrect = new Vector2(-4f,-20);
        DrawRect(new Rect2(chainrect, 8, 20), Color.Color8(255, 255, 255, 255), false, 1);
        DrawCircle(drawVector, 5, Color.Color8(255, 255, 255, 255), true);
    }
}
