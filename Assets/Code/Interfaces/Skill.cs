using UnityEngine;
using System.Collections;

public interface Skill
{

    string getSkillText();
    string getType();
    int getID();
    string getSkillDescription();
    string getSkillGroup();
    int getPrice();
    float getSkillLevel();
    float getEffect();
    bool setSkillLevel(float change);
}
