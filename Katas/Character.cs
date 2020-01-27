using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Katas
{
    public class CharacterType
    {
        private CharacterType(int maxRange)
        {
            MaxRange = maxRange;
        }

        // Nada mencionado sobre a existência de passos e etc. Pois o character tem que entrar no range para poder efetuar o ataque.
        public int MaxRange { get; private set; }

        public static CharacterType Melee() => new CharacterType(2);
        public static CharacterType Ranged() => new CharacterType(20);
    }

    public class Character
    {
        private const int maxHealth = 1000;
        private const int minHealth = 0;

        private readonly List<string> factions = new List<string>();

        public int Health { get; private set; }
        public int Level { get; private set; }
        public bool IsAlive { get; private set; }
        public CharacterType Type { get; set; }
        public ReadOnlyCollection<string> Factions => factions.AsReadOnly();

        public Character(CharacterType type)
        {
            Health = maxHealth;
            Level = 1;
            IsAlive = true;
            SetTypeCharacterAndRange(type);
        }

        public void JoinFaction(string faction)
        {
            faction = SanitizeFaction(faction);
            if (!factions.Contains(faction))
                factions.Add(faction);
        }

        private string SanitizeFaction(string faction)
        {
            return faction.ToLowerInvariant();
        }

        public void LeaveFaction(string faction)
        {
            faction = SanitizeFaction(faction);
            if (factions.Contains(faction))
                factions.Remove(faction);
        }

        public bool IsAlly(Character other)
        {
            return factions.Any(f => other.Factions.Any(f2 => f == f2));
        }

        private void SetTypeCharacterAndRange(CharacterType type)
        {
            Type = type;
        }

        public void DealDamage(Character target, int damage)
        {
            //Nada comentado sobre a possibilidade de atacar alguém morto
            if (this.Equals(target))
                return;

            if (!IsInRange(target))
                return;

            if (IsAlly(target))
                return;

            damage = CalculateDamage(this, target, damage);

            target.Health -= damage;
            if (target.Health <= minHealth)
            {
                target.Health = minHealth;
                target.IsAlive = false;
            }
        }

        public void DealDamageToThings(Thing thing, int damage)
        {
            thing.ReceiveDamage(damage);
        }

        private bool IsInRange(Character target)
        {
            return Type.MaxRange >= target.Type.MaxRange;
        }

        private int CalculateDamage(Character attacker, Character target, int damage)
        {
            int levelDifference = attacker.Level - target.Level;
            //não faz sentido se os personagens não sobem de nível (LevelUp)

            if (levelDifference >= 5)
                damage += (100 * 50) / damage;
            if (levelDifference <= -5)
                damage -= (100 * 50) / damage;

            return damage;
        }

        public void Heal(Character characterHealed, int healingPoints)
        {
            if (!characterHealed.IsAlive)
                return;
            if (!IsAlly(characterHealed) && !this.Equals(characterHealed))
                return;

            characterHealed.Health += healingPoints;

            if (characterHealed.Health > maxHealth)
                characterHealed.Health = maxHealth;
        }

        //Nada comentado sobre como o personagem sobe de nível (ou desça de nível)
        public void LevelUp(int level)
        {
            Level = level;
        }
    }

    public class Thing
    {
        public int Health { get; private set; }
        public bool IsDestroyed { get; set; }

        public Thing(int health = 1000)
        {
            Health = health;
            IsDestroyed = false;
        }

        public void ReceiveDamage(int damage)
        {
            this.Health -= damage;

            if (Health <= 0)
                IsDestroyed = true;
        }
    }
}
