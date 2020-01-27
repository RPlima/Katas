using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Katas
{
    public class Character
    {
        private readonly int maxHealth = 1000;
        private readonly int minHealth = 0;
        public static readonly List<string> AllFactions = new List<string>();

        public int Health { get; private set; }
        public int Level { get; private set; }
        public bool IsAlive { get; private set; }
        public string TypeCharacter { get; set; }
        public List<string> Factions { get; private set; } = new List<string>();

        //Nada mencionado sobre a existência de passos e etc. Pois o character tem que entrar no range para poder efetuar o ataque.
        public int MaxRange { get; set; }

        public Character(string typeCharacter)
        {
            Health = maxHealth;
            Level = 1;
            IsAlive = true;
            SetTypeCharacterAndRange(typeCharacter);
        }


        public void JoinFaction(string faction)
        {
            faction = SanitizeFaction(faction);
            if (!this.Factions.Any(f => f.Contains(faction, StringComparison.InvariantCultureIgnoreCase)))
                this.Factions.Add(faction);
            if (!AllFactions.Any(f => f.Contains(faction, StringComparison.InvariantCultureIgnoreCase)))
                AllFactions.Add(faction);
        }

        private string SanitizeFaction(string faction)
        {
            return faction.ToLowerInvariant();
        }

        public void LeaveFaction(string faction)
        {
            faction = SanitizeFaction(faction);
            if (this.Factions.Any(f => f.Contains(faction, StringComparison.InvariantCultureIgnoreCase)))
                this.Factions.Remove(faction);
            if (AllFactions.Any(f => f.Contains(faction, StringComparison.InvariantCultureIgnoreCase)))
                AllFactions.Remove(faction);
        }

        public bool IsAllie(Character character)
        {
            if (character.Factions.Any(f => this.Factions.Contains(f))) return true;
            return false;
        }

        private void SetTypeCharacterAndRange(string typeCharacter)
        {
            if (typeCharacter.Equals("Ranged", StringComparison.InvariantCultureIgnoreCase))
            {
                MaxRange = 20;
                TypeCharacter = "Ranged";
            }
            if (typeCharacter.Equals("Melee", StringComparison.InvariantCultureIgnoreCase))
            {
                MaxRange = 2;
                TypeCharacter = "Melee";
            }
        }

        public void DealDamage(Character characterRecivier, int damage)
        {
            //Nada comentado sobre a possibilidade de atacar alguém morto
            if (this.Equals(characterRecivier))
                return;

            if (!IsInRange(this, characterRecivier))
                return;

            if (IsAllie(characterRecivier))
                return;

            damage = CalculateDamage(this, characterRecivier, damage);

            characterRecivier.Health -= damage;
            if (characterRecivier.Health <= minHealth)
            {
                characterRecivier.Health = minHealth;
                characterRecivier.IsAlive = false;
            }
        }

        public void DealDamageOnThings(Thing thing, int damage)
        {
            thing.RecivieDamage(damage);
            if (thing.Health <= minHealth)
            {
                thing.SetAreThingIsDestoyed();
            }
        }

        private bool IsInRange(Character character, Character characterRecivier)
        {
            if (character.MaxRange >= characterRecivier.MaxRange)
                return true;
            return false;
        }

        private int CalculateDamage(Character characterAttacker, Character characterRecivier, int damage)
        {
            int differenceLevel = characterAttacker.Level - characterRecivier.Level;
            //não faz sentido se os personagens não sobem de nível (LevelUp)

            if (differenceLevel >= 5)
                damage += (100 * 50) / damage;
            if (differenceLevel <= -5)
                damage -= (100 * 50) / damage;

            return damage;
        }

        public void Heal(Character characterHealed, int Healing)
        {
            if (!characterHealed.IsAlive)
                return;
            if (!IsAllie(characterHealed))
                if (!this.Equals(characterHealed))
                    return;

            characterHealed.Health += Healing;

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

        public void RecivieDamage(int damage)
        {
            this.Health -= damage;
        }

        public void SetAreThingIsDestoyed()
        {
            this.IsDestroyed = true;
        }
    }
}
