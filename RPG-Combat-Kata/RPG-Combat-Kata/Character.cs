
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Character
    {
        private float maxHealth = 1000;
        private float minimumHealth = 0;
        private int levelInitial = 1;
        public IReadOnlyList<string> FactionsExisting => Faction;

        public Character(int MaxRange = 0)
        {
            Health = maxHealth;
            Level = levelInitial;
            IsAlive = true;
            Faction = null;
            IsARangedOrMeleeCharacter(this, MaxRange);
        }

        public float Health { get; private set; }
        public int Level { get; private set; }
        public bool IsAlive { get; private set; }
        public string TypeCharacter { get; set; }
        public int Range { get; set; }
        public List<string> Faction { get; set; }

        public void JoinFaction(string faction)
        {
            foreach (var factionNames in Faction)
            {
                if (!factionNames.Equals(faction, System.StringComparison.InvariantCultureIgnoreCase))
                    Faction.Add(faction);
            }
        }

        public void LeaveFaction(string faction)
        {
            foreach (var factionNames in Faction)
            {
                if (!factionNames.Equals(faction, System.StringComparison.InvariantCultureIgnoreCase))
                    Faction.Remove(faction);
            }
        }


        private void IsARangedOrMeleeCharacter(Character character, int MaxRange)
        {
            if (MaxRange <= 2)
            {
                character.TypeCharacter = "Melee";
                character.Range = MaxRange;
            }
            if (MaxRange >= 20)
            {
                character.TypeCharacter = "Ranged";
                character.Range = MaxRange;
            }
        }

        public void Attack(Character characterReceiver, float damage)
        {
            if (this.Equals(characterReceiver) || damage <= 0)
                return;

            bool isInRange = CheckDistanceForAttack(this.Range, characterReceiver.Range);
            bool checkIsAllie = CheckIsAllie(this.Faction,characterReceiver.Faction);

            if (!isInRange || checkIsAllie)
                return;

            int diferenceLevel = CheckDiferenceLevel(this.Level, characterReceiver.Level);

            if (diferenceLevel >= 5)
                damage += 50 % damage;

            if (diferenceLevel <= -5)
                damage -= 50 % damage;


            InflictDamage(characterReceiver, damage);
        }

        private bool CheckIsAllie(List<string> characterFactionAttacker, List<string> characterFactionRecivier)
        {
            bool isAllie = false;
            characterFactionAttacker.ForEach(f => isAllie = f.Equals(characterFactionRecivier));
            return isAllie;
        }

        private bool CheckDistanceForAttack(int rangeCharacterAttacker, int rangeCharactervReceiver)
        {
            if (rangeCharacterAttacker < rangeCharactervReceiver)
                return false;

            return true;
        }

        public void InflictDamage(Character characterReceiver, float damage)
        {
            characterReceiver.Health -= damage;
            if (characterReceiver.Health <= minimumHealth)
            {
                characterReceiver.Health = minimumHealth;
                characterReceiver.IsAlive = false;
            }
        }


        public void Heal(Character characterHealed, float lifePoints)
        {
            if (lifePoints == minimumHealth || characterHealed.IsAlive == false || lifePoints < 0)
                return;

            bool checkIsAllie = CheckIsAllie(this.Faction, characterHealed.Faction);
            if (!checkIsAllie)
                return;

            characterHealed.Health += lifePoints;
            if (characterHealed.Health > maxHealth)
                characterHealed.Health = maxHealth;
        }

        private int CheckDiferenceLevel(int level, int characterReceiverLevel)
        {
            return level - characterReceiverLevel;
        }


        public void LevelUp()
        {
            this.Level++;
        }
    }

    public class Things : Character
    {

    }
}