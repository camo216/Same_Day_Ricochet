using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;
public partial class Amulet : Enemy
{
    private enum ActionState
    {
        Waiting,
        Action1,
        Action2
    }

    private ActionState state = ActionState.Waiting;
    // Wait cooldown
    private const float WaitingCooldown = 2.5f;
    private float cooldownTimer = WaitingCooldown;

    // Action 1 checks

    private const float Action1FirstShotCooldown = .5f;
    private bool Action1FirstShotHasShot = false;
    private const float Action1SecondShotCooldown = 0f;
    private bool Action1SecondShotHasShot = false;
    private float Action1BulletSpeed = 100;

    // Action 2 checks
    private const float Action2FirstShotCooldown = 1f;
    private bool Action2FirstShotHasShot = false;
    private const float Action2SecondShotCooldown = .75f;
    private bool Action2SecondShotHasShot = false;
    private const float Action2ThirdShotCooldown = .5f;
    private bool Action2ThirdShotHasShot = false;
    private const float Action2FourthShotCooldown = .25f;
    private bool Action2FourthShotHasShot = false;
    private const float Action2FifthShotCooldown = 0f;
    private bool Action2FifthShotHasShot = false;
    private float Action2BulletSpeed = 100;

    private Vector2 drawVector = new Vector2(0, 0);

    // Called when the node enters the scene tree for the first time.
    public override void Inst(Node2D newPlayer, BulletManager bullet)
    {
        manager = GetParent<EnemyManager>();
        type = "Amulet";
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
                        Action1FirstShotHasShot = false;
                        Action1SecondShotHasShot = false;
                        cooldownTimer = Action1FirstShotCooldown;
                        state = ActionState.Action1;
                    }
                    else
                    {
                        Action2FirstShotHasShot = false;
                        Action2SecondShotHasShot = false;
                        Action2ThirdShotHasShot = false;
                        Action2FourthShotHasShot = false;
                        Action2FifthShotHasShot = false;
                        cooldownTimer = Action2FirstShotCooldown;
                        targetDirection = player.Position;
                        state = ActionState.Action2;
                    }
                }
                break;
            case ActionState.Action1:
                // Spawn bullets
                if (cooldownTimer <= Action1FirstShotCooldown && !Action1FirstShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, -15), Action1BulletSpeed);
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, -5), Action1BulletSpeed);
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, 5), Action1BulletSpeed);
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(player.Position, 15), Action1BulletSpeed);
                    Action1FirstShotHasShot = true;
                }
                if (cooldownTimer <= Action1SecondShotCooldown && !Action1SecondShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Red, this, BulletDirection(player.Position, -10), Action1BulletSpeed);
                    bulletManager.CreateBullet(BulletType.Green, this, BulletDirection(player.Position, 0), Action1BulletSpeed);
                    bulletManager.CreateBullet(BulletType.Blue, this, BulletDirection(player.Position, 10), Action1BulletSpeed);
                    Action1SecondShotHasShot = true;
                }

                // Enter attack cooldown.
                if (cooldownTimer <= 0)
                {
                    cooldownTimer = WaitingCooldown;
                    state = ActionState.Waiting;
                }
                break;
            case ActionState.Action2:
                // Spawn bullets
                if (cooldownTimer <= Action2FirstShotCooldown && !Action2FirstShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(targetDirection, 0), Action2BulletSpeed);
                    Action2FirstShotHasShot = true;
                }
                if (cooldownTimer <= Action2SecondShotCooldown && !Action2SecondShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Green, this, BulletDirection(targetDirection, -5), Action2BulletSpeed);
                    bulletManager.CreateBullet(BulletType.Green, this, BulletDirection(targetDirection, 5), Action2BulletSpeed);
                    Action2SecondShotHasShot = true;
                }
                if (cooldownTimer <= Action2ThirdShotCooldown && !Action2ThirdShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Blue, this, BulletDirection(targetDirection, 0), Action2BulletSpeed);
                    Action2ThirdShotHasShot = true;
                }
                if (cooldownTimer <= Action2FourthShotCooldown && !Action2FourthShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(targetDirection, -5), Action2BulletSpeed);
                    bulletManager.CreateBullet(BulletType.Unblockable, this, BulletDirection(targetDirection, 5), Action2BulletSpeed);
                    Action2FourthShotHasShot = true;
                }
                if (cooldownTimer <= Action2FifthShotCooldown && !Action2FifthShotHasShot)
                {
                    bulletManager.CreateBullet(BulletType.Red, this, BulletDirection(targetDirection, 0), Action2BulletSpeed);
                    Action2FifthShotHasShot = true;
                }

                // Enter attack cooldown.
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
        Vector2 chainrect = new Vector2(-4f,-20);
        DrawRect(new Rect2(chainrect, 8, 20), Color.Color8(255, 255, 255, 255), false, 1);
        DrawCircle(drawVector, 5, Color.Color8(255, 255, 255, 255), true);
    }
}
