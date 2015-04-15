using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GuiFunction : MonoBehaviour {
    public RawImage hotbar0, hotbar1, hotbar2, hotbar3, hotbar4, hotbar5, hotbar6, hotbar7, hotbar8, hotbar9, activeSkill, activeWeapon, activeWeaponBg;
    public Text consoleText, castTime, name, tooltipText, targetName;
    public RawImage[] castBar = new RawImage[5];
    public RawImage[] inventoryImage = new RawImage[2];
    public RawImage[] tooltipImage = new RawImage[2];
    public RawImage[] inventory = new RawImage[21];
    public RawImage[] loot = new RawImage[21];
    public RawImage[] skillWindowIcons = new RawImage[12];
    public skillWindowFunction[] skillWindowSkills = new skillWindowFunction[12];
    public Text[] skillWindowNames = new Text[12];
    public Text[] skillWindowDescriptions = new Text[12];
    public Text[] skillWindowLevels = new Text[12];
    public GameObject[] skillWindowSkillEntity = new GameObject[12];
    public GameObject[] skillWindowEntity = new GameObject[2];
    public Text[] skillWindowGroupText = new Text[2];
    public RawImage[] skillWindowGroupIcon = new RawImage[1];
    public Image health, stamina, mana, castProgress, targetHealth;
    public Image[] skillCooldowns = new Image[10];
    public InventoryGuiFunction[] inventoryItems = new InventoryGuiFunction[21];
    public playerLootGuiFunction[] lootItems = new playerLootGuiFunction[21];
    public hotbarGuiFunction[] hotbarSlots = new hotbarGuiFunction[10];
    public Text[] characterSheet = new Text[25];
    public Text[] inventoryCount = new Text[21];
    public Text[] lootCount = new Text[21];
    Texture tempIcon, tempIconInventory, tempIconLoot;
    string tempMessageString, tempCastString, tempNameString, tempToolTip;
    int hotbarIndex, inventoryIndex, lootIndex, lines = 0, maxLines = 90;
    bool lootCall = false, toolTipCall = false, nameCall = false, hotbarCall = false, activeSkillCall = false, activeWeaponCall = false, drawWeaponCall = false, textCall = false, clearText = false, castTimeCall = false, inventoryCall = false, casting = false;
    Color alpha, bg, inventoryAlpha, lootAlpha;
    public Player player;
    private bool isInvShowing = false, isTooltipShowing = false, isPlayerLootShowing = false;
    Item tempItem, tempItem1, tempItem2;
    Skill tempSkill;
    public GameObject target, characterSheetWindow, playerLoot;
    public Scrollbar scrollbar;

    public void clearGui()
    {
        hideInventory();
        skillWindowEntity[0].SetActive(false);
        skillWindowEntity[1].SetActive(false);
        characterSheetWindow.SetActive(false);
        hidePlayerLoot();
    }

    public void clearPlayerLoot()
    {
        for (int i = 0; i < lootCount.Length; i++)
        {
            lootCount[i].text = "";
            lootItems[i].setItem(null);
            loot[i].texture = null;
            loot[i].color = new Color(255, 255, 255, 0);
        }
    }

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

    public void setTarget(string name, float health)
    {
        target.SetActive(true);
        targetName.text = name;
        targetHealth.fillAmount = health;
    }

    public void removeTarget()
    {
        target.SetActive(false);
        targetName.text = "";
        targetHealth.fillAmount = 0;
    }

    public void setCharacterSheet(string name, List<float> stats)
    {
        characterSheet[0].text = name;
        for (int i = 0; i < stats.Count; i++)
        {
            characterSheet[i+1].text = stats[i].ToString();
        }
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

    public void setLootIcon(int inventorySlot, Texture icon, bool transparent, Item item)
    {
        if (!transparent)
        {
            tempIconLoot = icon;
            lootItems[inventorySlot].setItem(item);
            lootAlpha = new Color(255, 255, 255, 255);
        }
        else
        {
            tempIconLoot = null;
            lootItems[inventorySlot].setItem(null);
            lootAlpha = new Color(255, 255, 255, 0);
        }

        lootIndex = inventorySlot;
        loot[lootIndex].texture = tempIconLoot;
        loot[lootIndex].color = lootAlpha;
    }

    public void setInventoryStackCount(int inventorySlot, Item item, bool hidden)
    {
        if (!hidden)
        {
            if (item is Stackable)
            {
                inventoryCount[inventorySlot].text = ((Stackable)item).stackCount.ToString();
            }
            else
            {
                inventoryCount[inventorySlot].text = "";
            }
        }
        else
        {
            inventoryCount[inventorySlot].text = "";
        }
    }

    public void showPlayerLoot()
    {
        playerLoot.SetActive(true);
        isPlayerLootShowing = true;
    }

    public void hidePlayerLoot()
    {
        playerLoot.SetActive(false);
        isPlayerLootShowing = false;
    }

    public bool isLootShowing()
    {
        return isPlayerLootShowing;
    }
    public void setLootStackCount(int inventorySlot, Item item, bool hidden)
    {
        if (!hidden)
        {
            if (item is Stackable)
            {
                lootCount[inventorySlot].text = ((Stackable)item).stackCount.ToString();
            }
            else
            {
                lootCount[inventorySlot].text = "";
            }
        }
        else
        {
            lootCount[inventorySlot].text = "";
        }
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

    public void makeSkillWindow(string type, List<Skill> input)
    {
        for (int i = 0; i < 12; i++)
        {
            skillWindowSkillEntity[i].SetActive(false);
        }
        switch (type)
        {
            case "combat": skillWindowGroupIcon[0].texture = Resources.Load("combat", typeof(Texture)) as Texture; skillWindowGroupText[0].text = "Combat Skills"; skillWindowGroupText[1].text = ""; break;
            case "crafting": skillWindowGroupIcon[0].texture = Resources.Load("crafting", typeof(Texture)) as Texture; skillWindowGroupText[0].text = "Crafting Skills"; skillWindowGroupText[1].text = ""; break;
            case "general": skillWindowGroupIcon[0].texture = Resources.Load("general", typeof(Texture)) as Texture; skillWindowGroupText[0].text = "General Skills"; skillWindowGroupText[1].text = ""; break;
            case "lesser magic": skillWindowGroupIcon[0].texture = Resources.Load("lesser-magic", typeof(Texture)) as Texture; skillWindowGroupText[0].text = "Lesser Magic"; skillWindowGroupText[1].text = player.player.getSkill(26).getSkillLevel().ToString(); break;
            case "weapon skill": skillWindowGroupIcon[0].texture = Resources.Load("weapon skill", typeof(Texture)) as Texture; skillWindowGroupText[0].text = "Weapon Skills"; skillWindowGroupText[1].text = ""; break;
        }

        for (int i = 0; i < input.Count; i++)
        {
            skillWindowSkillEntity[i].SetActive(true);
            skillWindowIcons[i].texture = input[i].getIcon();
            skillWindowNames[i].text = input[i].getSkillText();
            skillWindowLevels[i].text = input[i].getSkillLevel().ToString();
            skillWindowDescriptions[i].text = input[i].getSkillDescription();
            skillWindowSkills[i].setSkill(input[i]);
        }
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

        castProgress.fillAmount = Mathf.Abs(1-(input/total));
        }
        else
        {
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
        casting = false;
        castProgress.fillAmount = 0;
    }

    public void hideActiveWeapon()
    {
        activeWeapon.color = new Color(255, 255, 255, 0);
        activeWeaponBg.color = new Color(255, 255, 255, 0);
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
