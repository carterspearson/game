using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyPress;
            Character hero = new Character(150, 25, 3, 2);
            Enemy enemy = new Enemy(175, 25, 6, 3);
            bool isHeroDead = false, isEnemyDead = false, turnOver = false;
            while (isHeroDead != true && isEnemyDead != true)
            {
                turnOver = false;
                while (!turnOver)
                {
                    turnOver = true;
                    Console.WriteLine("Your turn. Press A to attack, S to enrage, and D to defend");
                    Console.WriteLine("Hero:");
                    Console.WriteLine("Health: {0} || Stamina: {1} || Attack Points: {2} || Defense Points: {3}", hero.health, hero.stamina, hero.attackPoints, hero.defensePoints);
                    Console.WriteLine("Enemy:");
                    Console.WriteLine("Health: {0}", enemy.health);
                    keyPress = Console.ReadKey();
                    Console.WriteLine(" ");
                    if (keyPress.Key == ConsoleKey.A)
                        hero.Attack(enemy);
                    else if (keyPress.Key == ConsoleKey.D)
                        hero.Defend();
                    else if (keyPress.Key == ConsoleKey.S)
                        hero.Enrage();
                    else
                    {
                        turnOver = false;
                        Console.WriteLine("Unknown command");
                    }
                }

                enemy.takeTurn(hero);
                isHeroDead = hero.isDead();
                isEnemyDead = enemy.isDead();
            }

            if (isEnemyDead)
                Console.WriteLine("You have defeated your enemy!");
            else
                Console.WriteLine("You have been defeated");

            Console.ReadKey();

        }
    }

    class Character
    {
        public int health, stamina, attackPoints, defensePoints;

        public Character(int health0, int stamina0, int attackPoints0, int defensePoints0)
        {
            health = health0;
            stamina = stamina0;
            attackPoints = attackPoints0;
            defensePoints = defensePoints0;
        }

        public void Attack(Character character)
        {
            if (stamina <= 4)
            {
                Console.WriteLine("I'm too weak for that");
                return;
            }
            if (character.defensePoints >= attackPoints)
                character.health -= 1;
            else
                character.health = character.health - attackPoints + character.defensePoints;
            stamina -= 5;
            if (stamina < 0)
                stamina = 0;

        }

        public void Defend()
        {
            defensePoints += 1;
            stamina += 3;
            if (stamina > 25)
                stamina = 25;
        }

        public void Enrage()
        {
            attackPoints += 1;
            stamina += 1;
            if (stamina > 25)
                stamina = 25;
        }

        public bool isDead()
        {
            if (health <= 0)
                return true;
            return false;
        }
    }

    class Enemy : Character
    {

        public Enemy(int health0, int stamina0, int attackPoints0, int defensePoints0) : base(health0, stamina0, attackPoints0, defensePoints0)
        {
        }

        public void takeTurn(Character hero)
        {

            Random rnd = new Random();
            int chance = rnd.Next(0, 4);

            if (hero.health + hero.defensePoints < attackPoints || (stamina == 25 && attackPoints - hero.defensePoints > 5))
            {
                Attack(hero);
                Console.WriteLine("The enemy has attacked you!");
            }
            else if (hero.attackPoints - defensePoints > 10)
            {
                Defend();
                Console.WriteLine("The enemy has defended!");
            }
            else if (hero.defensePoints > attackPoints)
            {
                Enrage();
                Console.WriteLine("The enemy is enraged!");
            }
            else if (chance == 2)
            {
                Enrage();
                Console.WriteLine("The enemy is enraged!");
            }
            else if ((chance == 1 || chance == 0) && stamina >0)
            {
                Attack(hero);
                Console.WriteLine("The enemy has attacked you!");
            }
            else
            {
                Defend();
                Console.WriteLine("The enemy has defended!");
            }

        }

    }
}
