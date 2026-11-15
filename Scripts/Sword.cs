using Godot;
using System;

public partial class Sword : Enemy
{
    private enum ActionState
    {
        Waiting,
        Action1,
        Action2
    }

    private ActionState state = ActionState.Waiting;
    private const float WaitingCooldown = 1;

    private float cooldownTimer = WaitingCooldown;
    private float Action1BulletSpeed = 10;

    private EnemyManager manager;

    // Called when the node enters the scene tree for the first time.
    public override void Inst(Node2D newPlayer, BulletManager bullet)
    {
        manager = GetParent<EnemyManager>();
        type = "Sword";
        bulletManager = bullet;
        player = newPlayer;
        health = 1;
        direction = new Vector2(rng.Next(-1, 1), rng.Next(-1, 1)).Normalized();
        speed = 5;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    public override void Move(double delta)
    {
        direction += new Vector2((float)rng.NextDouble() * .2f - .1f, (float)rng.NextDouble() * .2f - .1f);
        direction.Normalized();

        Velocity = direction.Normalized() * speed;
        MoveAndSlide();
    }

    public override void DoAction(double delta)
    {
        // Update timer
        cooldownTimer -= (float)delta;

        // Take action based on state
        switch (state)
        {
            case ActionState.Waiting:
                if (cooldownTimer <= 0)
                {
                    if (rng.Next() % 2 == 0)
                    {
                        state = ActionState.Action1;
                    }
                    else
                    {

                    }
                }
                break;
            case ActionState.Action1:
                // Spawn bullets
                bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(-60), Action1BulletSpeed);
                bulletManager.CreateBullet(BulletType.Blue, this, BulletDirection(-30), Action1BulletSpeed);
                bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(0), Action1BulletSpeed);
                bulletManager.CreateBullet(BulletType.Blue, this, BulletDirection(30), Action1BulletSpeed);
                bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(60), Action1BulletSpeed);

                // Enter attack cooldown.
                cooldownTimer = WaitingCooldown;
                state = ActionState.Waiting;
                break;
            case ActionState.Action2:
                break;
        }
    }
}
