using FluentAssertions;
using Model;
using Xunit;

namespace UnitTests
{
    public class IterationOnes
    {
        [Fact]
        public void Instance_Character_Alive_FullHealth_level1()
        {
            Character character = new Character();

            character.Health.Should().Be(1000);
            character.Level.Should().Be(1);
            character.IsAlive.Should().BeTrue();
        }

        [Fact]
        public void Character_Attack()
        {
            Character character = new Character();
            Character character2 = new Character();

            character.Attack(character2, character, 100);

            character2.Health.Should().Be(0);
            character2.IsAlive.Should().BeFalse();
        }

        [Fact]
        public void Character_Heal()
        {
            Character character = new Character();
            Character character2 = new Character();

            character.Heal(character, character.GetHashCode(), 100);

            character2.Health.Should().Be(1000);
        }
    }

    public class IterationTwo
    {
        [Fact]
        public void Character_Cannot_Deal_Damage_It_Your_Self()
        {
            Character character = new Character();
            Character character2 = new Character();

            character.Attack(character2, character, 100);

            character.Health.Should().Be(1000);
            character2.Health.Should().Be(900);
        }

        [Fact]
        public void Character_can_only_Heal_itself()
        {
            Character character = new Character();
            Character character2 = new Character();

            character2.Attack(character, character2, 100);
            character.Heal(character, character.GetHashCode(), 100);

            character.Health.Should().Be(1000);
        }

        [Fact]
        public void DealingDamage_When_Attacker_Has_Level_5_Above_Receiver()
        {
            Character character = new Character();
            Character character2 = new Character();

            //character.Level = 5;
            //character2.Level = 10;

            character.Attack(character, character2, 100);
        }
    }
}
