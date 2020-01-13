using System;

namespace Model
{
    public class Character
    {
        private float maxHealth = 1000;
        private float minimumHealth = 0;
        private int levelInitial = 1;

        public Character()
        {
            Health = maxHealth;
            Level = levelInitial;
            IsAlive = true;
        }

        public float Health { get; private set; }
        public int Level { get; private set; }
        public bool IsAlive { get; private set; }

        public void Attack(Character characterReceiver, float damage)
        {
            if (this.Equals(characterReceiver))
                return;

            int diferenceLevel = CheckDiferenceLevel(this.Level, characterReceiver.Level);

            if (diferenceLevel >= 5)
                damage += 50 % damage;

            if (diferenceLevel <= -5)
                damage -= 50 % damage;

            if (damage < 0)
                return;
            CalculateLifePointsOrIsAlive(characterReceiver, true, damage);
        }

        public void Heal(float lifePoints)
        {

            if (lifePoints == minimumHealth || this.IsAlive == false || lifePoints < 0)
                return;
            CalculateLifePointsOrIsAlive(this, false, lifePoints);
        }

        private int CheckDiferenceLevel(int level, int characterReceiverLevel)
        {
            return level - characterReceiverLevel;
        }

        private void CalculateLifePointsOrIsAlive(Character character, bool isAttack, float damageOrHeal)
        {
            if (isAttack)
            {
                character.Health -= damageOrHeal;
                if (character.Health <= minimumHealth)
                {
                    character.Health = minimumHealth;
                    character.IsAlive = false;
                }
            }
            else
            {
                character.Health += damageOrHeal;
                if (character.Health > maxHealth)
                    character.Health = maxHealth;
            }
        }

        public void LevelUp()
        {
            this.Level++;
        }
    }
}