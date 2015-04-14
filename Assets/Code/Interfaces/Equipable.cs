using UnityEngine;
using System.Collections;

public interface Equipable : Item {

    string getItemSlot();
    float getDurability();
    bool setDurability(float change);
    void setStartingDurability(float start);
}
