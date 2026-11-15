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
    private const float WaitingCooldown = 3;

    private float cooldownTimer = WaitingCooldown;
    private const float Action2FirstShotCooldown = 1f;
    private bool Action2FirstShotHasShot = false;
    private const float Action2SecondShotCooldown = .5f;
    private bool Action2SecondShotHasShot = false;
    private const float Action2ThirdShotCooldown = 0f;
    private bool Action2ThirdShotHasShot = false;
    private float ActionBulletSpeed = 100;


    private Vector2 drawVector = new Vector2(0, 0);
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
        base._Process(delta);
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
                        Action2FirstShotHasShot = false;
                        Action2SecondShotHasShot = false;
                        Action2ThirdShotHasShot = false;
                        cooldownTimer = Action2FirstShotCooldown;
                        state = ActionState.Action2;
                    }
                }
                break;
            case ActionState.Action1:
                // Spawn bullets
                bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, -40), ActionBulletSpeed);
                bulletManager.CreateBullet(BulletType.Blue, this, BulletDirection(player.Position, -20), ActionBulletSpeed);
                bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, 0), ActionBulletSpeed);
                bulletManager.CreateBullet(BulletType.Blue, this, BulletDirection(player.Position, 20), ActionBulletSpeed);
                bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, 40), ActionBulletSpeed);

                // Enter attack cooldown.
                cooldownTimer = WaitingCooldown;
                state = ActionState.Waiting;
                break;
            case ActionState.Action2:
                // Spawn bullets
                if (cooldownTimer <= Action2FirstShotCooldown && !Action2FirstShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, 0), ActionBulletSpeed);
                    Action2FirstShotHasShot = true;
                }
                if (cooldownTimer <= Action2SecondShotCooldown && !Action2SecondShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Red, this, BulletDirection(player.Position, 0), ActionBulletSpeed);
                    Action2SecondShotHasShot = true;
                }
                if (cooldownTimer <= Action2ThirdShotCooldown && !Action2ThirdShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, 0), ActionBulletSpeed);
                    Action2ThirdShotHasShot = true;
                }
                if (cooldownTimer <= 0)
                {
                    cooldownTimer = WaitingCooldown - 1;
                    state = ActionState.Waiting;
                }
                break;
        }
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(drawVector, 10, 20), Color.Color8(255, 255, 255, 255), false, 1);
        DrawLine(new Vector2(5, -10), new Vector2(5, 0), Color.Color8(255, 255, 255, 255), 1);
        DrawLine(new Vector2(0,20), new Vector2(5, 25), Color.Color8(255, 255,255, 255), 1);
        DrawLine(new Vector2(10, 20), new Vector2(5, 25), Color.Color8(255, 255, 255, 255), 1);
    }
}
