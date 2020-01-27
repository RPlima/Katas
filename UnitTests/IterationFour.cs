using FluentAssertions;
using Katas;
using Xunit;

namespace UnitTests
{
    public class IterationFour
    {
        [Fact]
        public void Characters_May_Belong_To_One_Or_More_Factions()
        {
            Character character = new Character("melee");
            character.JoinFaction("BrotherHood");
            character.Factions.Should().HaveCount(1);
        }

        [Fact]
        public void A_Character_May_Join_Or_Leave_One_Or_More_Factions()
        {
            Character character = new Character("melee");
            character.JoinFaction("BrotherHood");
            character.LeaveFaction("BrotherHood");
            character.Factions.Should().HaveCount(0);
        }

        [Fact]
        public void Players_Belonging_To_The_Same_Faction_Are_Considered_Allies()
        {
            Character character = new Character("melee");
            Character character1 = new Character("melee");

            character.JoinFaction("BrotherHood");
            character1.JoinFaction("BrotherHood");
            character.IsAllie(character1).Should().BeTrue();
        }

        [Fact]
        public void Allies_Cannot_Deal_Damage_To_One_Another()
        {
            Character character = new Character("melee");
            Character character1 = new Character("melee");

            character.JoinFaction("BrotherHood");
            character1.JoinFaction("BrotherHood");

            character.DealDamage(character1, 100);
            character1.Health.Should().Be(1000);
        }

        [Fact]
        public void Allies_Can_Heal_One_Another()
        {
            Character character = new Character("melee");
            Character character1 = new Character("melee");

            character.DealDamage(character1, 100);

            character.JoinFaction("BrotherHood");
            character1.JoinFaction("BrotherHood");

            character.Heal(character1, 100);
            character1.Health.Should().Be(1000);
        }
    }
}
