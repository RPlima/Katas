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
        public int Level { get; set; }
        public bool IsAlive { get; private set; }

        public void Attack(Character characterStriker, Character characterReceiver, float damage)
        {
            if (characterStriker.Equals(characterReceiver))
                return;

            int diferenceLevel = characterStriker.Level - characterReceiver.Level;

            if (diferenceLevel >= 5)
                damage += 50 % damage;

            if (diferenceLevel <= -5)
                damage -= 50 % damage;

            if (damage < 0)
                return;

            characterStriker.Health -= damage;
            if (characterStriker.Health <= minimumHealth)
            {
                characterStriker.Health = minimumHealth;
                characterStriker.IsAlive = false;
            }
        }

        public void Heal(Character character, int identifierHealer, float lifePoints)
        {
            if (character.GetHashCode() != identifierHealer)
                return;

            if (lifePoints == minimumHealth || character.IsAlive == false)
                return;

            character.Health += lifePoints;
            if (character.Health > maxHealth)
                character.Health = maxHealth;
        }
    }
}