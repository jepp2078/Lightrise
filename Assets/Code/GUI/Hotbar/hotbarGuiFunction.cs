using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hotbarGuiFunction : MonoBehaviour {
    public RawImage hotbar0, hotbar1, hotbar2, hotbar3, hotbar4, hotbar5, hotbar6, hotbar7, hotbar8, hotbar9, activeHotbar, weaponEquipped;
    public static hotbarGuiFunction instance;
    RawImage tempIcon;
    int input;

    void Start()
    {
        instance = this;
    }
    //public void setHotbarIcon(int hotbarSlot)
    //{
    //    input = hotbarSlot;

    //    OnGui();
    //}

    //void OnGui()
    //{
    //    switch (input)
    //    {
    //        case 2: tempIcon = inputIn; break;
    //    }
    //    hotbar2.texture = tempIcon.texture;
    //}

}
