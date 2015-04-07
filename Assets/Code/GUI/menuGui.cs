using UnityEngine;
using System.Collections;

public class menuGui : MonoBehaviour {
    public GuiFunction gui;
    public GameObject Console, Status, Hotbar, Skills0, Skills1;
    public void toggleInventory()
    {
        if (gui.isInventoryShowing())
            gui.hideInventory();
        else
            gui.showInventory();
    }

    public void toggleConsole()
    {
        if (Console.active)
            Console.SetActive(false);
        else
            Console.SetActive(true);
    }
    public void toggleStatus()
    {
        if (Status.active)
            Status.SetActive(false);
        else
            Status.SetActive(true);
    }
    public void toggleHotbar()
    {
        if (Hotbar.active)
            Hotbar.SetActive(false);
        else
            Hotbar.SetActive(true);
    }

    public void toggleSkills()
    {
        if (Skills0.active)
        {
            Skills0.SetActive(false);
            Skills1.SetActive(false);
        }
        else
        {
            Skills0.SetActive(true);
        }
    }
}
