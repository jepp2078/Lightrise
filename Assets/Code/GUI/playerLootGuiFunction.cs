using UnityEngine;
using System.Collections;
using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class playerLootGuiFunction : MonoBehaviour, IEndDragHandler
{
    private Item item;
    public GuiFunction gui;

    public void OnHoverHandler(BaseEventData e)
    {
        if (!gui.isToolTShowing() && gui.isLootShowing() && item != null)
        {
            if (item is Weapon)
            {
                Weapon wep = ((Weapon)item);
                gui.setToolTip(item.getItemText() + "\n" + "Durability: " + ((Equipable)item).getDurability() + "\n" + "Rank: " + wep.getWeaponRank() + "\n" + "Damage: " + wep.getDamage() + "\n" + "Attackspeed: " + wep.getAttackspeed() + "\n" + "\n" + "\n" + item.getItemDescription());
                gui.showTooltip();
            }
            else if (item is Equipable)
            {
                gui.setToolTip(item.getItemText() + "\n Durability: " + ((Equipable)item).getDurability() + "\n" + "\n" + item.getItemDescription());
                gui.showTooltip();
            }
            else
            {
                gui.setToolTip(item.getItemText() + "\n" + "\n" + "\n" + item.getItemDescription());
                gui.showTooltip();
            }

        } 
    }

    public void OnExitHandler(BaseEventData e)
    {
        gui.hideTooltip();
    }

    public void setItem(Item itemIn)
    {
        item = itemIn;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetComponent<InventoryGuiFunction>() != null && item != null)
        {
            Item itemOut = (Item)Activator.CreateInstance(item.GetType());
            if (itemOut is Stackable)
            {
                ((Stackable)itemOut).stackCount = ((Stackable)item).stackCount;
            }
            eventData.pointerEnter.GetComponent<InventoryGuiFunction>().putInInventory(itemOut);
            gui.player.view.RPC("lootPlayerItem", PhotonTargets.All, gui.player.currentlyLooting, item.getInventorySlot());
        }
    }
}
