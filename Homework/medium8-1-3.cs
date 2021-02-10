using System;
using System.Collections.Generic;
using System.Linq;

class Bag
{
    public List<Item> Items;
    public int MaxWeight { get;}
    
    public Bag(List<Item> items, int maxWeight)
    {
        if (items.Sum(item => item.Count) > maxWeight)
        {
            throw new InvalidOperationException();
        }
        Items = items;
        MaxWeight = maxWeight;
    }

    public void AddItem(string name, int count)
    {
        int currentWeidth = Items.Sum(item => item.Count);
        Item targetItem = Items.FirstOrDefault(item => item.Name == name);

        if (targetItem == null)
            throw new InvalidOperationException();

        if (currentWeidth + count > MaxWeight)
            throw new InvalidOperationException();

        targetItem.AddCount(count);
    }
}

class Item
{
    public int Count { get; private set;}
    public readonly string Name;
    
    public Item(int count, string name)
    {
        Count = count;
        Name = name;
    }
    public void AddCount(int value)
    {
        Count += value;
    }
}