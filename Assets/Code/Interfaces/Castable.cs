using UnityEngine;
using System.Collections;

public interface Castable
{
    GameObject cast();
    void stopEffect();
    float getManaCost();
    float getStaminaCost();
    float getHealthCost();
    float getDuration();
    bool setCurrentDuration(float durationChange);
    bool getState();
    void setState(bool state);
    string getCastMsg();
    float getCooldown();
    bool setCurrentCooldown(float cooldownChange);
    float getCurrentCooldown();
    float getGainPrCast();
    void updateGainPrCast();

}
