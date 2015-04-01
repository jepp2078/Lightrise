using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GuiFunction : MonoBehaviour {
    public RawImage hotbar0, hotbar1, hotbar2, hotbar3, hotbar4, hotbar5, hotbar6, hotbar7, hotbar8, hotbar9, activeSkill, activeWeapon, activeWeaponBg;
    public Text consoleText, castTime, name, tooltipText;
    public RawImage[] castBar = new RawImage[5];
    public RawImage[] inventoryImage = new RawImage[2];
    public RawImage[] tooltipImage = new RawImage[2];
    public RawImage[] inventory = new RawImage[21];
    public Image health, stamina, mana, castProgress;
    public Image[] skillCooldowns = new Image[10];
    public InventoryGuiFunction[] inventoryItems = new InventoryGuiFunction[21];
    public hotbarGuiFunction[] hotbarSlots = new hotbarGuiFunction[10];
    Texture tempIcon, tempIconInventory;
    string tempMessageString, tempCastString, tempNameString, tempToolTip;
    int hotbarIndex, inventoryIndex, lines = 0, maxLines = 90;
    bool toolTipCall = false, nameCall = false, hotbarCall = false, activeSkillCall = false, activeWeaponCall = false, drawWeaponCall = false, textCall = false, clearText = false, castTimeCall = false, inventoryCall = false, casting = false;
    Color alpha, bg, inventoryAlpha;
    public Player player;
    private bool isInvShowing = false, isTooltipShowing = false;
    Item tempItem, tempItem1;
    Skill tempSkill;
    public Scrollbar scrollbar;

    public void init()
    {
        hideCastBar();
        hideInventory();
        hideTooltip();
    }

    public void setName(string name)
    {
        tempNameString = name;
        nameCall = true;
    }
    public void setHotbarIcon(int hotbarSlot, Texture icon, bool transparent, Item item, Skill skill)
    {
        hotbarIndex = hotbarSlot;
        tempIcon = icon;
        tempItem1 = item;
        tempSkill = skill;
        if (transparent)
        {
            alpha = new Color(255, 255, 255, 0);
        }
        else
        {
            alpha = new Color(255, 255, 255, 255);
        }
        hotbarCall = true;
        OnGUI();
    }

    public void setInventoryIcon(int inventorySlot, Texture icon, bool transparent, Item item)
    {
        inventoryIndex = inventorySlot;
        tempIconInventory = icon;
        tempItem = item;
        if (transparent || !isInvShowing)
        {
            inventoryAlpha = new Color(255, 255, 255, 0);
        }
        else
        {
            inventoryAlpha = new Color(255, 255, 255, 255);
        }
        inventoryCall = true;
        OnGUI();
    }

    public void setActiveSkillIcon(Texture icon, bool transparent)
    {
        tempIcon = icon;
        if (transparent)
        {
            alpha = new Color(255, 255, 255, 0);
        }
        else
        {
            alpha = new Color(255, 255, 255, 255);
        }
        activeSkillCall = true;
        OnGUI();
    }

    public void setActiveWeaponIcon(Texture icon, bool sheathed, bool sheathCallIn)
    {
        if (icon != null && sheathCallIn == false) 
        {
            drawWeaponCall = true;
            tempIcon = icon;
        }
        else
        {
            drawWeaponCall = false;
        }

        if (sheathed)
        {
            alpha = new Color(255, 255, 255, 255);
            bg = new Color(58f / 14790 * 58, 58f / 14790 * 58, 58f / 14790 * 58);
        }
        else
        {
            alpha = new Color(255, 255, 255, 255);
            bg = new Color(58f / 14790 * 58, 255f / 65025f * 255, 58f / 14790 * 58);
        }
        activeWeaponCall = true;
        OnGUI();
    }

    public void newTextLine(string input)
    {
        lines++;
        if (lines > maxLines)
        {
            clearText = true;
            lines = 1;
        }
        if (lines > 12)
            scrollbar.value -= 0.01228f; //Mathf.Abs(1f - (((float)lines) / ((float)maxLines)) + 0.01f + 0.005f);
        else
            scrollbar.value = 1;
        tempMessageString = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + input + "\n";
        textCall = true;
        OnGUI();


    }

    public void setCastTime(float input, float total)
    {
        if (casting == false)
        {
            showCastBar();
            casting = true;
        }
        if (input != 0) 
        { 
        //TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(input));

        //tempCastString = string.Format("{0:D2}:{1:D3}s",
        //                t.Seconds,
        //                t.Milliseconds);
        castProgress.fillAmount = Mathf.Abs(1-(input/total));
        }
        else
        {
            //tempCastString = "";
            hideCastBar();
            casting = false;
            castProgress.fillAmount = 0;
        }
        castTimeCall = true;
        OnGUI();
    }
    public void showCastBar()
    {
        for (int i = 0; i < 5; i++)
        {
            castBar[i].color = new Color(255, 255, 255, 255);
        }
    }
    public void hideCastBar()
    {
        for (int i = 0; i < 5; i++)
        {
            castBar[i].color = new Color(255, 255, 255, 0);
        }
    }

    public void showInventory()
    {

        inventoryImage[0].color = new Color(255, 255, 255, 255);
        inventoryImage[1].color = new Color(0, 0, 0, 255);

        for (int i = 0; i < 21; i++)
        {
            if (inventory[i].texture != null)
                inventory[i].color = new Color(255, 255, 255, 255);
        }
        isInvShowing = true;
    }
    public void hideInventory()
    {
        for (int i = 0; i < 2; i++)
        {
            inventoryImage[i].color = new Color(255, 255, 255, 0);
        }

        for (int t = 0; t < 21; t++)
        {
            
            inventory[t].color = new Color(255, 255, 255, 0);
        }
        hideTooltip();
        isInvShowing = false;
    }

    public void showTooltip()
    {

        tooltipImage[0].color = new Color(0, 0, 0, 255);
        tooltipImage[1].color = new Color(0, 0, 0, 255);
        tooltipText.color = new Color(255, 255, 255, 255);
        isTooltipShowing = true;
    }
    public void hideTooltip()
    {
        for (int i = 0; i < 2; i++)
        {
            tooltipImage[i].color = new Color(255, 255, 255, 0);
        }
        tooltipText.color = new Color(255, 255, 255, 0);
        isTooltipShowing = false;
    }

    public void setToolTip(string input)
    {
        tempToolTip = input;
        toolTipCall = true;
    }

    public bool isInventoryShowing()
    {
        return isInvShowing;
    }

    public bool isToolTShowing()
    {
        return isTooltipShowing;
    }
    public void setSkillCooldown(float current, float total, int slot)
    {
        skillCooldowns[slot].fillAmount = current / total;
    }

    void OnGUI()
    {
        if (hotbarCall)
        {
            switch (hotbarIndex)
            {
                case 0: hotbar0.texture = tempIcon; hotbar0.color = alpha; break;
                case 1: hotbar1.texture = tempIcon; hotbar1.color = alpha; break;
                case 2: hotbar2.texture = tempIcon; hotbar2.color = alpha; break;
                case 3: hotbar3.texture = tempIcon; hotbar3.color = alpha; break;
                case 4: hotbar4.texture = tempIcon; hotbar4.color = alpha; break;
                case 5: hotbar5.texture = tempIcon; hotbar5.color = alpha; break;
                case 6: hotbar6.texture = tempIcon; hotbar6.color = alpha; break;
                case 7: hotbar7.texture = tempIcon; hotbar7.color = alpha; break;
                case 8: hotbar8.texture = tempIcon; hotbar8.color = alpha; break;
                case 9: hotbar9.texture = tempIcon; hotbar9.color = alpha; break;
            }
            if (tempItem1 == null)
                hotbarSlots[hotbarIndex].setSkill(tempSkill);
            else
                hotbarSlots[hotbarIndex].setItem(tempItem1);

            hotbarCall = false;
        }
        if(activeSkillCall)
        {
            activeSkill.texture = tempIcon; 
            activeSkill.color = alpha;
            activeSkillCall = false;
        }
        if (activeWeaponCall)
        {
            activeWeaponBg.color = bg;
            if (drawWeaponCall == true)
            {
                activeWeapon.texture = tempIcon;
            }
            activeWeapon.color = alpha;
            activeWeaponCall = false;
        }
        if (textCall)
        {
            if (clearText)
            {
                consoleText.text = "";
                clearText = false;
            }
            consoleText.text += tempMessageString;
            textCall = false;
        }
        if (castTimeCall)
        {
            //castTime.text = tempCastString;
            textCall = false;
        }
        if (nameCall)
        {
            name.text = tempNameString;
            nameCall = false;
        }
        if (inventoryCall)
        {
            inventory[inventoryIndex].texture = tempIconInventory; 
            inventory[inventoryIndex].color = inventoryAlpha;
            inventoryItems[inventoryIndex].setItem(tempItem);

            inventoryCall = false;
        }
        if (toolTipCall)
        {
            tooltipText.text = tempToolTip;
            toolTipCall = false;
        }

        health.fillAmount = player.player.getTempHealthFloat() / player.player.getHealthFloat();
        stamina.fillAmount = player.player.getTempStaminaFloat() / player.player.getStaminaFloat();
        mana.fillAmount = player.player.getTempManaFloat() / player.player.getManaFloat();

    }

    void OnMouseEnter()
    {
        Debug.Log("hit something");
    }

}
