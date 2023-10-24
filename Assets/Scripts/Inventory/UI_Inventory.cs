using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UI_Inventory : MonoBehaviour {

    private Inventory inventory;

    private PlayerController player;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake() {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }


    public void SetPlayer(PlayerController player) {
        this.player = player;
    }

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItems();
    }

    private void AddTouchAndStayLogic() {
        foreach (Item item in inventory.GetItemList()) {

        }
    }

    private void RefreshInventoryItems() {
        // Does this loop even work?
        foreach (Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 130f;
        foreach (Item item in inventory.GetItemList()) {

            //if (item.amount <= 0 && item.combinable) {
            //    inventory.RemoveItem(new Item { itemType = Item.ItemType.String, amount = 1 });

            //    //inventory.RemoveItem(item);
            //    //inventory.GetItemList().Remove(item);

            //}

            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {

                inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {

                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount, combinable = item.combinable, maxStack = item.maxStack };

                inventory.RemoveItem(item);

                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };

            itemSlotRectTransform.GetComponent<ButtonClickAndStayController>().OnLongClick += () => {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount, combinable = item.combinable, maxStack = item.maxStack };

                inventory.RemoveItem(item);

                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();

            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1) {
                uiText.SetText(item.amount.ToString());
            } else {
                uiText.SetText("");
            }

            x++;
            if (x >= 7) {
                x = 0;
                y++;
            }



        }

    }


}
