using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // si usas el nuevo Input System

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Image inventorySlot;
    private Color inventorySlotColor = new Color32(113, 68, 52, 255);
    private List<Item> itemInventory = new List<Item>();
    private bool isInventoryOpen = false;
    public Player player;

    void OnEnable()
    {
        if (player == null)
            player = Player.instance;

        if (player != null)
        {
            player.OpenInventoryEvent += ToggleInventory;
            player.ItemCollectedEvent += AddItem;
            player.UseItemEvent += Use;
        }
        else
        {
            Debug.LogWarning("No se encontró Player para InventoryManager");
        }
    }


    private void OnDisable()
    {
        if (player != null)
        {
            player.OpenInventoryEvent -= ToggleInventory;
            player.ItemCollectedEvent -= AddItem;
            player.UseItemEvent -= Use;
        }
    }
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        
        inventoryUI.SetActive(isInventoryOpen);

        Time.timeScale = isInventoryOpen ? 0f : 1f;

        Cursor.visible = isInventoryOpen;
        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void Use()
    {
        if (itemInventory.Count > 0)
        {
            Debug.Log("You used an item");
            Debug.Log(itemInventory[0]);
            itemInventory[0].Use(player);
            itemInventory.RemoveAt(0);
            inventorySlot.color = inventorySlotColor;
            inventorySlot.sprite = null;
            RectTransform rt = inventorySlot.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(200, rt.sizeDelta.y);

        }
        else
        {
            
            Debug.Log("You don't have any item");
        }
    }

    public void AddItem(Item item)
    {
        Debug.Log(item.name);
        itemInventory.Add(item); 
        inventorySlot.sprite = itemInventory[0].icon;
        inventorySlot.color = Color.white;
        RectTransform rt = inventorySlot.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(150, rt.sizeDelta.y);
    }
}
