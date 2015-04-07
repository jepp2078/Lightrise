using UnityEngine;
using System.Collections;

public abstract class Resource_Base_Node : MonoBehaviour, ResourceSource
{
    public int _resourceCount = 100;
    public int _resourceMax = 100;
    public float _respawnCooldown = 600;
    protected float _respawnCooldownCurrent = 0;
    public string nodeName = "Default node name";

    public int resourceCount
    {
        get
        {
            return _resourceCount;
        }
        set
        {
            _resourceCount = value;
        }
    }

    public int resourceMax
    {
        get { return _resourceMax; }
    }

    public float respawnCooldown
    {
        get { return _respawnCooldown; }
    }

    public float respawnCooldownCurrent
    {
        get
        {
            return _respawnCooldownCurrent;
        }
        set
        {
            _respawnCooldownCurrent = value;
        }
    }

    public virtual Item_Resources_BaseResource harvest
    {
        get { throw new System.NotImplementedException(); }
    }

    string ResourceSource.nodeName
    {
        get
        {
            return nodeName;
        }
        set
        {
            nodeName = value;
        }
    }
}
