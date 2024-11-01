namespace mood;

public class Items
{
    public record Item(string Name, int Id);

    public static Item[] AllItems = new Item[10];

    public static void Init()
    {
        //TODO add more items
        AllItems[0] = new Item("gun", 1);
        AllItems[1] = new Item("knife", 2);
        AllItems[2] = new Item("health potion", 3);
        AllItems[3] = new Item("gun", 1);
        AllItems[4] = new Item("gun", 1);
        AllItems[5] = new Item("gun", 1);
        AllItems[6] = new Item("gun", 1);
        AllItems[7] = new Item("gun", 1);
        AllItems[8] = new Item("gun", 1);
        AllItems[9] = new Item("gun", 1);
    }

    public static void Use(int id)
    {
        switch (id)
        {
            case 1:
                UseGun();
                break;
            case 2:
                UseKnife();
                break;
            case 3:
                UseHealthPotion();
                break;
        }
    }

    
    public static void UseGun()
    {   
        //TODO implement gun 
        
    }
    
    public static void UseKnife()
    {
        //TODO implement knife
    }
    
    public static void UseHealthPotion()
    {
        //TODO implement health potion
    }
}