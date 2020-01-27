using FluentAssertions;
using Katas;
using Xunit;

namespace UnitTests
{
    public class IterationFive
    {
        [Fact]
        public void Characters_Can_Damage_Non_Character_Things()
        {
            Character character = new Character("melee");
            Thing thing = new Thing();

            character.DealDamageToThings(thing, 100);
            thing.Health.Should().Be(900);
        }

        [Fact]
        public void Characters_Destroy_Thing()
        {
            Character character = new Character("melee");
            Thing thing = new Thing();

            character.DealDamageToThings(thing, 1000);
            thing.Health.Should().Be(0);
            thing.IsDestroyed.Should().BeTrue();
        }
    }
}
