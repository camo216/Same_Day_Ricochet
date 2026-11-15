using Godot;
using System;

public partial class Spider : Enemy
{
    private Vector2 drawVector = new Vector2(0, 0);
    private enum ActionState
    {
        Waiting,
        Action1
    }
    private ActionState state = ActionState.Waiting;
    private const float WaitingCooldown = 1;
    private const float Action1FirstShotCooldown = 1f;
    private bool Action1FirstShotHasShot = false;
    private const float Action1SecondShotCooldown = .5f;
    private bool Action1SecondShotHasShot = false;
    private const float Action1ThirdShotCooldown = 0f;
    private bool Action1ThirdShotHasShot = false;
    private float Action1BulletSpeed = 10;
    private float cooldownTimer = WaitingCooldown;

    // Called when the node enters the scene tree for the first time.
    public override void Inst(Node2D newPlayer, BulletManager bullet)
	{
        manager = GetParent<EnemyManager>();
        type = "Spider";
        bulletManager = bullet;
        player = newPlayer;
        health = 1;
        direction = new Vector2(rng.Next(-1,1), rng.Next(-1,1)).Normalized();
        speed = 5;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        QueueRedraw();
        base._Process(delta);
    }

	public override void Move(double delta)
	{
        direction += new Vector2((float)rng.NextDouble() * .2f - .1f, (float)rng.NextDouble() * .2f - .1f);
        direction.Normalized();

        Velocity = direction.Normalized() * speed;
        MoveAndSlide();
    }

    /// <summary>
    /// Action logic for the spider
    /// </summary>
    /// <param name="delta">delta time</param>
    public override void DoAction(double delta)
    {
        // Update timer
        cooldownTimer -= (float)delta;
        // Take action based on state
        switch (state)
        {
            // Changes state to action once timer hits 0.
            case ActionState.Waiting:
                if (cooldownTimer <= 0)
                {
                    Action1FirstShotHasShot = false;
                    Action1SecondShotHasShot = false;
                    Action1ThirdShotHasShot = false;
                    cooldownTimer = Action1FirstShotCooldown;
                    state = ActionState.Action1;
                }
                break;
            // Performs the logic for the spider's first attack.
            case ActionState.Action1:
                if (cooldownTimer <= Action1FirstShotCooldown && !Action1FirstShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Web, this, BulletDirection(player.Position, 30), Action1BulletSpeed);
                    Action1FirstShotHasShot = true;
                }
                if (cooldownTimer <= Action1SecondShotCooldown && !Action1SecondShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Web, this, BulletDirection(player.Position, 0), Action1BulletSpeed);
                    Action1SecondShotHasShot = true;
                }
                if (cooldownTimer <= Action1ThirdShotCooldown && !Action1ThirdShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Web, this, BulletDirection(player.Position, -30), Action1BulletSpeed);
                    Action1ThirdShotHasShot = true;
                }
                if (cooldownTimer <= 0)
                {
                    cooldownTimer = WaitingCooldown;
                    state = ActionState.Waiting;
                }
                break;
        }
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
