using FluentAssertions;
using Katas;
using Xunit;

namespace UnitTests
{
    public class IterationTwo
    {
        [Fact]
        public void Character_Cannot_Deal_Damage_To_Itself()
        {
            Character character = new Character();
            character.DealDamage(character, 100);

            character.Health.Should().Be(1000);
        }

        [Fact]
        public void Character_Can_Only_Heal_Itself()
        {
            Character character = new Character();
            Character character1 = new Character();

            character.DealDamage(character1, 100);
            character.Heal(character1, 100);

            character1.Health.Should().Be(900);
        }

        [Fact]
        public void When_Dealing_Damage_If_The_Target_Is_5_Or_More_Levels_Above()
        {
            Character character = new Character();
            Character character1 = new Character();

            character1.LevelUp(10);

            character.DealDamage(character1, 100);
            character1.Health.Should().Be(950);
        }

        [Fact]
        public void When_Dealing_Damage_If_The_Target_Is_5_Or_More_Levels_Below()
        {
            Character character = new Character();
            Character character1 = new Character();

            character.LevelUp(10);

            character.DealDamage(character1, 100);
            character1.Health.Should().Be(850);
        }
    }
}
