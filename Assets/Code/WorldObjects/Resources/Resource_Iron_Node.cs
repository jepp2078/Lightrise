using UnityEngine;
using System.Collections;

public class Resource_Iron_Node : Resource_Base_Node
{
    public Resource_Iron_Node()
    {
        nodeName = "Iron Node";
    }


    public override Item_Resources_BaseResource harvest
    {
        get 
        {
            if (_resourceCount > 0)
            {
                _resourceCount--;
                return new Item_Resources_Iron_Ore();

            }
            else
            {
                Debug.Log("Resource depleted");

            }

            return null;
        }
    }
}
