using FluentAssertions;
using Xunit;

namespace PcStatsReporter.Core.Test.Unit;

public class StoreTests
{
    private readonly Store store;

    public StoreTests()
    {
        this.store = new Store();
    }

    [Fact]
    public void Get_Int_WhenNothingSet_ThenReturnZero()
    {
        int result = this.store.Get<int>();

        result.Should().Be(0);
    }

    [Fact]
    public void Get_Int_WhenSixSet_ThenReturnSix()
    {
        store.Set(6);
        int result = this.store.Get<int>();

        result.Should().Be(6);
    }
    
    [Fact]
    public void Get_Panda_WhenNothingSet_ThenReturnNull()
    {
        Panda result = this.store.Get<Panda>();

        result.Should().BeNull();
    }

    [Fact]
    public void Get_Panda_WhenSet_ThenReturnPanda()
    {
        Panda panda = new Panda()
        {
            Name = "Cheese",
            Number = 2.5d
        };

        store.Set(panda);
        panda = null;

        Panda result = this.store.Get<Panda>();

        result.Name.Should().Be("Cheese");
        result.Number.Should().Be(2.5d);
    }

    [Fact]
    public void GetSet_PandaTwoTimes_GetSecondOne()
    {
        Panda pandaOne = new Panda()
        {
            Name = "Cheese",
            Number = 2.5d
        };

        Panda pandaTwo = new Panda()
        {
            Name = "Tatanka",
            Number = 33.19d
        };

        store.Set(pandaOne);
        store.Set(pandaTwo);

        Panda result = this.store.Get<Panda>();

        result.Name.Should().Be("Tatanka");
        result.Number.Should().Be(33.19d);
    }

    private class Panda
    {
        public string Name { get; set; }
        public double Number { get; set; }
    }
}