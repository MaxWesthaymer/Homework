using System;

class LivingEntity
{
	public int Health;
	
	public virtual void TakeDamage(int damage)
	{
		if(Health <= 0){
			Console.WriteLine("Я умер");
		}
	}
}
class Wombat : LivingEntity
{
	public int Armor;
	public override void TakeDamage(int damage)
	{
		Health -= damage - Armor;
		base.TakeDamage(damage);
	}
}

class Human : LivingEntity
{
	public int Agility;

	public void TakeDamage(int damage)
	{
		Health -= damage / Agility;
		base.TakeDamage(damage);
	}
}