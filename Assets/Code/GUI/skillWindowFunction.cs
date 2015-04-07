using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class skillWindowFunction : MonoBehaviour, IPointerDownHandler
{
    private Skill skill = null;
    public GuiFunction gui;
    public Function func;
    private Skill currentlyDragging;
    private bool dragging = false;

    public void setSkill(Skill skillIn)
    {
        skill = skillIn;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetComponent<skillWindowFunction>().skill != null)
        {
            currentlyDragging = eventData.pointerEnter.GetComponent<skillWindowFunction>().skill;
            dragging = true;
        }
    }

    public void OnPointerEndDrag(BaseEventData e)
    {
        if (dragging)
        {
            ((PointerEventData)e).pointerEnter.GetComponent<hotbarGuiFunction>().dropHotbarAble((HotbarAble)skill);
            dragging = false;
            currentlyDragging = null;
        }
    }
}
