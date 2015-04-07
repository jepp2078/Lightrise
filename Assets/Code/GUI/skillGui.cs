using UnityEngine;
using System.Collections;

public class skillGui : MonoBehaviour
{
    public GameObject SkillWindow;
    public Function func;

    public void toggleSkillWindow()
    {
        if (SkillWindow.active)
            SkillWindow.SetActive(false);
        else
            SkillWindow.SetActive(true);
    }

    public void makeSkillWindow(string type)
    {
        switch(type)
        {
            case "combat": func.playerInstance.player.makeSkillWindow(type); break;
            case "crafting": func.playerInstance.player.makeSkillWindow(type); break;
            case "general": func.playerInstance.player.makeSkillWindow(type); break;
            case "lesser magic": func.playerInstance.player.makeSkillWindow(type); break;
            case "weapon skill": func.playerInstance.player.makeSkillWindow(type); break;
        }
    }

}
