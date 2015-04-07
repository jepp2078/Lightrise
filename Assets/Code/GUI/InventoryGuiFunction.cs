using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryGuiFunction : MonoBehaviour, IEndDragHandler
{
    private Item item;
    public GuiFunction gui;

    public void OnHoverHandler(BaseEventData e)
    {
        if (!gui.isToolTShowing() && gui.isInventoryShowing() && item != null)
        {
            if (item is Weapon)
            {
                Weapon wep = ((Weapon)item);
                gui.setToolTip(item.getItemText() + "\n" + "Rank: " + wep.getWeaponRank() + "\n" + "Damage: " + wep.getDamage() + "\n" + "Attackspeed: " + wep.getAttackspeed() + "\n" + "\n" + "\n" + item.getItemDescription());
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
        eventData.pointerEnter.GetComponent<hotbarGuiFunction>().dropHotbarAble((HotbarAble)item);
    }
}
