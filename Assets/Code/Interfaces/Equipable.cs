using UnityEngine;
using System.Collections;

public interface Equipable {

    string getItemSlot();
    float getDurability();
    bool setDurability(float change);

}
