using FluentAssertions;
using Katas;
using Xunit;

namespace UnitTests
{
    public class IterationThree
    {
        [Fact]
        public void Create_Character_With_MaxRange_And_Melee()
        {
            Character character = new Character("melee");

            character.Type.Should().Be("Melee");
            character.MaxRange.Should().Be(2);
        }

        [Fact]
        public void Create_Character_With_MaxRange_And_Ranged()
        {
            Character character = new Character("ranged");

            character.Type.Should().Be("Ranged");
            character.MaxRange.Should().Be(20);
        }

        [Fact]
        public void Characters_Must_Be_In_Range_To_Deal_Damage_To_A_Target()
        {
            Character characterAttacker = new Character("ranged");
            Character characterRecivier = new Character("melee");

            characterAttacker.DealDamage(characterRecivier, 100);
            characterRecivier.Health.Should().Be(900);
        }
    }
}
