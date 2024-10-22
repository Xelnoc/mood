namespace mood;

public class Items
{
    public record Item(string Name);

    public static Item[] allItems = new Item[10];

    public static void Init()
    {
        allItems[0] = new Item("gun");
        allItems[1] = new Item("knife");
        allItems[2] = new Item("health potion");
        allItems[3] = new Item("gun");
        allItems[4] = new Item("gun");
        allItems[5] = new Item("gun");
        allItems[6] = new Item("gun");
        allItems[7] = new Item("gun");
        allItems[8] = new Item("gun");
        allItems[9] = new Item("gun");
    }
    

}