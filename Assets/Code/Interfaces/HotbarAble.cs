using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface HotbarAble {

    void setHotbarSlot(int slot);
    int getHotbarSlot();
    int getInventoryID();
    int getSkillID();
    Texture getIcon();
	
}
