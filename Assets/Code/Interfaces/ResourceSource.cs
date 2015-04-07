using UnityEngine;
using System.Collections;

public interface ResourceSource
{
    int resourceCount{get; set;}
    int resourceMax { get; }
    float respawnCooldown { get;}
    float respawnCooldownCurrent { get; set; }
    string nodeName { get; set; }
    Item_Resources_BaseResource harvest { get;}
}
