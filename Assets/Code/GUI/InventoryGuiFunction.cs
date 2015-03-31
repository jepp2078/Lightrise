using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryGuiFunction : MonoBehaviour
{
    private Item item;
    public GuiFunction gui;
    public void OnHoverHandler(BaseEventData e)
    {
        if (!gui.isToolTShowing() && gui.isInventoryShowing() && item != null)
        {
            gui.setToolTip(item.getItemText() + "\n" + "\n" + item.getItemDescription());
            gui.showTooltip();
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
}
