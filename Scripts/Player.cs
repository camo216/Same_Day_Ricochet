using Godot;
using System;
//[Tool]

public partial class Player : CharacterBody2D
{
    private Random rng = new Random();

    private Vector2 drawVector = new Vector2(0, 0);
    private const int speed = 500;
    private AudioStreamPlayer deathSound;
    private AudioStreamPlayer hurtSound;
    private AudioStream[] hurtSounds;
    
    private bool onCooldown = false;
    private const double cooldownTime = 0.75;
    private const double parryTime = 0.5;
    private double cooldown = 1;
    //private double cooldownRed = 0.5;
    //private double cooldownGreen = 0.5;
    //private double cooldownBlue = 0.5;

    private bool isWebbed = false;
    private const float webCooldown = 1;
    private float webTimer = webCooldown;

    private static Color defaultColor = Color.Color8(0, 255, 255, 90);
    private Color currentColor = defaultColor;
    private BulletType parry = BulletType.None;

    private const int maxHealth = 5;
    [Export] private int health = maxHealth;
    private bool dead = false;


    private void UpdateVelocity()
    {
        Velocity = Input.GetVector("left", "right", "up", "down") * speed;
        if (isWebbed)
        {
            Velocity = Velocity * .25f;
        }
    }
    
   

    private void UpdateCooldown(double delta)
    {
        cooldown -= delta;
        if (cooldown <= 0)
        {
            cooldown = cooldownTime;
            onCooldown = false;
        }
        else if (cooldown - parryTime <= 0)
        {
            parry = BulletType.None;
            currentColor = defaultColor;
        }
        //if (parryRed)
        //{
        //    cooldownRed -= delta;
        //    if (cooldownRed <= 0)
        //    {
        //        parryRed = false;
        //        cooldownRed = cooldownTime;
        //    }
        //}
        //if (parryBlue)
        //{
        //    cooldownBlue -= delta;
        //    if (cooldownBlue <= 0)
        //    {
        //        parryBlue = false;
        //        cooldownBlue = cooldownTime;
        //    }
        //}
        //if (parryGreen)
        //{
        //    cooldownGreen -= delta;
        //    if (cooldownGreen <= 0)
        //    {
        //        parryGreen = false;
        //        cooldownGreen = cooldownTime;
        //    }
        //}
    }

    private void UpdateWebCooldown(double delta)
    {
        webTimer -= (float)delta;
        if (webTimer <= 0)
        {
            isWebbed = false;
        }
    }

    public void TakeDamage()
    {
        if (!dead)
        {
                health -= 1;
            if (health <= 0)
            {
                // fuckin die
                dead = true;
                deathSound.Finished += LoadMenu;
                deathSound.Play();
            }
            hurtSound.Stream = hurtSounds[rng.Next(1, 5)];
            hurtSound.Play();
        }
    }

    public void Heal()
    {
        if (health < 5)
        {
            health += 1;
        }
    }

    private void LoadMenu()
    {
        var scene = GD.Load<PackedScene>("res://menu.tscn").Instantiate();
        GetParent().GetParent().AddChild(scene);

        GetParent().QueueFree();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        deathSound = new AudioStreamPlayer();
        deathSound.Stream = GD.Load<AudioStream>("res://Audio/death.mp3");
        AddChild(deathSound);

        hurtSounds = [GD.Load<AudioStream>("res://Audio/glass1.mp3"), GD.Load<AudioStream>("res://Audio/glass2.mp3"), GD.Load<AudioStream>("res://Audio/glass3.mp3"), 
            GD.Load<AudioStream>("res://Audio/glass4.mp3"), GD.Load<AudioStream>("res://Audio/glass5.mp3"), GD.Load<AudioStream>("res://Audio/glass6.mp3")];

        hurtSound = new AudioStreamPlayer();
        AddChild(hurtSound);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!dead)
        {
            if (!onCooldown)
            {
                if (Input.IsActionJustPressed("parry_red"))
                {
                    parry = BulletType.Red;
                    onCooldown = true;
                    currentColor = Color.Color8(255, 0, 0, 90);
                }

                else if (Input.IsActionJustPressed("parry_green"))
                {
                    parry = BulletType.Green;
                    onCooldown = true;
                    currentColor = Color.Color8(0, 255, 0, 90);
                }


                else if (Input.IsActionJustPressed("parry_blue"))
                {
                    parry = BulletType.Blue;
                    onCooldown = true;
                    currentColor = Color.Color8(0, 0, 255, 90);
                }
            }

            if (onCooldown)
            {
                UpdateCooldown(delta);
            }

            // handles cooldown for being webbed.
            if (isWebbed)
            {
                UpdateWebCooldown(delta);
            }

            UpdateVelocity();
            MoveAndSlide();
            // Calls a method to handle each collision that happened.
            //for (int i = 0; i < GetSlideCollisionCount(); i++)
            //{
            //    var collision = GetSlideCollision(i);
            //    HandleCollisions(collision);
            //}
            for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                var collision = GetSlideCollision(i);
                HandleCollisions(collision);
            }
        }

        QueueRedraw();
    }

    // Handles collision interactions with the player.
    private void HandleCollisions(KinematicCollision2D collision)
    {
        if (collision.GetCollider() is Spider spider)
        {
            spider.TakeDamage();
        }
    }

    /// <summary>
    /// Handles all collisions with the player
    /// </summary>
    /// <param name="collision">Collision with player</param>
    public void HandleBullets(Bullet bullet)
    {
        // Does the player's parry match the bullet's type?
        if (parry == bullet.Type)
        {
            bullet.isDeflected = true;
            bullet.speed *= 2;
        }

        // Webs slow down the player
        else if (bullet.Type == BulletType.Web)
        {
            isWebbed = true;
            webTimer = webCooldown;
            bullet.DestroySelf();
        }

        // Player takes damage if not parried.
        else if (!bullet.isDeflected)
        {
            TakeDamage();
            bullet.DestroySelf();
        }
    }

    public override void _Draw()
    {
        // Your draw commands here.

        DrawArc(drawVector, 20, 0, 10, 100, Color.Color8(0, 255, 255, 255), 1, false);
        DrawCircle(drawVector, 20, currentColor, true);
    }
}
