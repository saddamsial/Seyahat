using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Experimental.Rendering.LWRP;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour {

    private static bool isRightSide;
    Joystick joystick;
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) {

        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);


        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);


        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item) {
        
        // Vector3 dropDirection = UtilsClass.GetRandomDir();
        // Instead I'll set a default Vector:
        Vector3 dropDirection;


        if (isRightSide) {
            //dropDirection = new Vector3(.50f, .05f);
            dropDirection = new Vector3(.7f, .3f);
        } else {
            //dropDirection = new Vector3(-.50f, .05f); 
            dropDirection = new Vector3(-.7f, .3f);

        }
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + dropDirection * 8f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(dropDirection * 40f, ForceMode2D.Impulse);


        return itemWorld;
    }

    private void Update() {

        //float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalInput = joystick.Horizontal;
        if (horizontalInput < 0) {
            isRightSide = false;
        } else if(horizontalInput > 0) {
            isRightSide = true;
        }
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private UnityEngine.Rendering.Universal.Light2D light2D;
    private TextMeshPro textMeshPro;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = transform.Find("Light").GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
    }

    public void SetItem(Item item) {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        light2D.color = item.GetColor();
        if (item.amount > 1) {
            textMeshPro.SetText(item.amount.ToString());
        } else {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem() {
        return item;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

}
