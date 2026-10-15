using System.Security;
using Xunit;

public class ViewSizeTests
{
    private static string[] Render(Field field)
    {
        string text = new FieldView().Render(field);
        return text.Replace("\r\n", "\n").TrimEnd('\n');
    }

    [Fact]
    public void Render_PlayerInCentre_Shows5x5()
    {
        var field = new Field(7,7);
        field.Player = new Player(3,3);

        string expected = 
            ".....\n"+
            ".....\n"+
            "..p..\n"+
            ".....\n"+
            ".....";
        Assert.Equal(expected, Render(field));
    }
    [Fact]
    public void Render_PlayerInTopLeftCorner()
    {
        var field = new Field(7,7);
        field.Player = new Player(0,0);

        string expected = 
            "P..\n"+
            "...\n"+
            "...\n";
        Assert.Equal(expected, Render(field));
    }
}






public class FieldTests
{
    [Fact]
    public void IsWall_CellWithWall_ReturnsTrue()
    {
        var field = new Field(10, 10);
        field.AddWall(new Wall(3, 4));

        Assert.True(field.IsWall(3, 4));
    }

    [Fact]
    public void GetItemAt_EmptyCell_ReturnsNull()
    {
        var field = new Field(10, 10);
        field.AddItem(new Axe(2, 2));

        Assert.Null(field.GetItemAt(7, 7));
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(5, 0)]
    [InlineData(0, 5)]
    public void AddItem_OutsideField_ThrowsArgumentException(int x, int y)
    {
        var field = new Field(5, 5);

        Assert.Throws<ArgumentException>(() => field.AddItem(new Axe(x, y)));
    }

    [Fact]
    public void Render_AxeAndTorch_AreDrawnDifferently()
    {
        var field = new Field(4, 3);
        field.AddItem(new Axe(0, 0));
        field.AddItem(new Torch(1, 0));

        string[] lines = Render(field);

        char axe = lines[0][0];
        char torch = lines[0][1];
        char empty = lines[0][2];
        Assert.NotEqual(axe, torch);
        Assert.NotEqual(axe, empty);
        Assert.NotEqual(torch, empty);
    }
}

public class InventoryTests
{
    [Fact]
    public void Contains_UnknownName_ReturnsFalse()
    {
        var inventory = new Inventory();
        inventory.Add(new Axe(0, 0));

        Assert.False(inventory.Contains("unknown"));
    }
}