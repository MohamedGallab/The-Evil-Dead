
public interface IStoreSlotHandler
{
    void AddToInventory(Item item, int stackCount);
    void RemoveFromInventory(Item item, int indexInInventory);

}
