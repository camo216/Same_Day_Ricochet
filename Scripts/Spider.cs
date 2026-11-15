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
	}
        QueueRedraw();
    }

	public override void Move(double delta)
	{
        Velocity = direction.Normalized() * speed;
        MoveAndSlide();
    }

    public override void DoAction(double delta)
    {
        
    public override void _Draw()
    {
        Vector2 toplegvector = new Vector2 (drawVector.X, drawVector.Y + 2);
        Vector2 bottomlegvector = new Vector2(drawVector.X, drawVector.Y - 2);
        DrawArc(toplegvector, 10, 0, 10, -10, Color.Color8(255, 255, 255, 255), 1, false);
        DrawArc(toplegvector, 10, 0, 10, 10, Color.Color8(255, 255, 255, 255), 1, false);
        DrawArc(bottomlegvector, 10, 0, 10, -10, Color.Color8(255, 255, 255, 255), 1, false);
        DrawArc(bottomlegvector, 10, 0, 10, 10, Color.Color8(255, 255, 255, 255), 1, false);
        DrawCircle(drawVector, 5, Color.Color8(255, 255, 255, 255), true);
    }
}
