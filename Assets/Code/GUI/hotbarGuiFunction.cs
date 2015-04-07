using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hotbarGuiFunction : MonoBehaviour, IEndDragHandler
{

    private Item item = null;
    private Skill skill = null;
    public GuiFunction gui;
    public Function func;
    public int slot;

    public void OnHoverHandler(BaseEventData e)
    {
        if (!gui.isToolTShowing())
        {
            if (item != null)
            {
                Weapon wep = ((Weapon)item);
                gui.setToolTip(item.getItemText() + "\n" + "Rank: " + wep.getWeaponRank() + "\n" + "Damage: " + wep.getDamage() + "\n" + "Attackspeed: " + wep.getAttackspeed() + "\n" + "\n" + "\n" + item.getItemDescription());
                gui.showTooltip();
            }

            if (skill != null)
            {
                gui.setToolTip(skill.getSkillText() + "\n" + skill.getSkillDescription() + "\n" + "\n" + "Level: " + skill.getSkillLevel());
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

    public void setSkill(Skill skillIn)
    {
        skill = skillIn;
    }

    public void dropHotbarAble(HotbarAble input)
    {
        func.putOnHotbar(input, slot);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        func.removeFromHotbar(slot);
    }
}
