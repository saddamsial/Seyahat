using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory {

    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction) {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1, combinable = false, maxStack = 1 }); ;
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1, combinable = false });
        AddItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1, combinable = false });
        AddItem(new Item { itemType = Item.ItemType.String, amount = 3, combinable = true, maxStack = 4 });

        //**************************

    }

    public void AddItem(Item item) {
        if (item.IsStackable()) {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList) {
                // Case: We have 3 strings. Later we get 3 more.
                if (inventoryItem.itemType == item.itemType) {
                    itemAlreadyInInventory = true;
                    inventoryItem.amount += item.amount;
                    if (inventoryItem.combinable) {
                        if (inventoryItem.amount >= inventoryItem.maxStack) {
                            inventoryItem.amount -= inventoryItem.maxStack;
                            CreateCombinedItem(inventoryItem);
                            return;

                            //Item newItem = new Item();
                            //item = CreateCombinedItem(item.itemType);
                        }

                    }
                }
            }
            // Item is stackable and isn't in inventory:
            if (!itemAlreadyInInventory) {
                itemList.Add(item);
            }
        }
        // Item isn't stackable and isn't in inventory.
        else {
            itemList.Add(item);
        }
        // Whenever an item is added, OnItemListChanged event happens.
        // This event has one subscriber which calls RefreshInventoryItems() method. This method is inside of UI_Inventory class.
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    private void CreateCombinedItem(Item item) {
        Item newCombinedItem = new Item();
        if (item.itemType == Item.ItemType.String) {
            newCombinedItem.itemType = Item.ItemType.Rope;
        }
        newCombinedItem.amount = 1;
        newCombinedItem.maxStack = 1;
        AddItem(newCombinedItem);
        itemList.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

        // return newItem;
    }

    public void RemoveItem(Item item) {
        if (item.IsStackable()) {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0) {
                itemList.Remove(itemInInventory);
            }
        } else {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item) {
        useItemAction(item);
    }

    public List<Item> GetItemList() {
        return itemList;
    }

}
