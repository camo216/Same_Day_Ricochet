using Godot;
using System;

public partial class Spider : Enemy
{
    private Vector2 drawVector = new Vector2(0, 0);
    private EnemyManager manager;

    // Called when the node enters the scene tree for the first time.
    public override void Inst()
	{
        manager = GetParent<EnemyManager>();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        QueueRedraw();
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
        Vector2 toplegvector = new Vector2 (drawVector.X, drawVector.Y + 7);
        Vector2 bottomlegvector = new Vector2(drawVector.X, drawVector.Y + 12);
        Vector2 rightlegvector = new Vector2(drawVector.X + 8, drawVector.Y);
        Vector2 leftlegvector = new Vector2(drawVector.X - 8, drawVector.Y);
        DrawArc(new Vector2(rightlegvector.X,toplegvector.Y), 15, -2.75f, -0.75f, 10, Color.Color8(255, 255, 255, 255), 1, false);
        DrawArc(new Vector2(leftlegvector.X, toplegvector.Y), 15, -1, -2.5f, 10, Color.Color8(255, 255, 255, 255), 1, false);
        DrawArc(new Vector2(rightlegvector.X, bottomlegvector.Y), 15, -2.5f, 0.25f, 10, Color.Color8(255, 255, 255, 255), 1, false);
        DrawArc(new Vector2(leftlegvector.X, bottomlegvector.Y), 15, -1, -3.25f, 10, Color.Color8(255, 255, 255, 255), 1, false);
        DrawCircle(drawVector, 5, Color.Color8(255, 255, 255, 255), true);
    }
}
