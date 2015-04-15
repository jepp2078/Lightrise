using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class itemList : MonoBehaviour {
    public List<Item> generalItemList = new List<Item>();
	// Use this for initialization
	void Start () {
        generalItemList.Add(new Item_Resources_Iron_Ore());
        generalItemList.Add(new Item_Tool_Pickaxe());
        generalItemList.Add(new Item_Weapon_Mirdain_Spellstaff());
        generalItemList.Add(new Item_Weapon_Short_Bow());
        generalItemList.Add(new Item_Weapon_Troll_Clubber());
	}
	
	// Update is called once per frame
    public Item getGeneralItem(int id)
    {
        return generalItemList[id];
    }
}
