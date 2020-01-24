using System;

namespace Katas
{
    public class Character
    {
        private readonly int maxHealth = 1000;
        private readonly int minHealth = 0;

        public int Health { get; private set; }
        public int Level { get; private set; }
        public bool IsAlive { get; private set; }

        public Character()
        {
            Health = maxHealth;
            Level = 1;
            IsAlive = true;
        }

        public void DealDamage(Character characterRecivier, int damage)
        {
            //Nada comentado sobre a possibilidade de atacar alguém morto
            if (this.Equals(characterRecivier))
                return;

            damage = CalculateDamage(this, characterRecivier, damage);

            characterRecivier.Health -= damage;
            if (characterRecivier.Health <= minHealth)
            {
                characterRecivier.Health = minHealth;
                characterRecivier.IsAlive = false;
            }
        }

        private int CalculateDamage(Character characterAttacker, Character characterRecivier, int damage)
        {
            int differenceLevel = characterAttacker.Level - characterRecivier.Level;
            if (differenceLevel >= 5)
                damage += (100 * 50)/ damage;
            if (differenceLevel <= -5)
                damage -= (100 * 50) / damage;

            return damage;
        }

        public void Heal(Character characterHealed, int Healing)
        {
            if (!characterHealed.IsAlive || !this.Equals(characterHealed))
                return;

            characterHealed.Health += Healing;

            if (characterHealed.Health > maxHealth)
                characterHealed.Health = maxHealth;
        }

        public void LevelUp(int level)
        {
            Level = level;
        }
    }
}
