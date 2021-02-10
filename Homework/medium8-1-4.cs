using System;

class Player
{
    public string Name { get; private set; }
    public int Age { get; private set; }
    private Weapon _weapon;
    private Movement _movement;

    public Player(Weapon weapon, Movement movement)
    {
        _weapon = weapon;
        _movement = movement;
    }

    public void Move()
    {
        _movement.Move();
    }

    public void Attack()
    {
        _weapon.Attack();
    }

    
}

class Weapon
{
    public float Cooldown { get; private set; }
    public int Damage { get; private set; }

    public bool IsReloading()
    {
        throw new NotImplementedException();
    }
    public void Attack()
    {
        
    }
}

class Movement
{
    public float DirectionX { get; private set; }
    public float DirectionY { get; private set; }
    public float Speed { get; private set; }

    public void Move()
    {
        
    }
}