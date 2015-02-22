using UnityEngine;
using System.Collections;

public class SkillEntity : MonoBehaviour {

	private string name;
    private int id;

    public SkillEntity(int id, string i)
    {
        this.id = id;
        this.name = i;
    }

    public string getName()
    {
        return name;
    }

    public int getID()
    {
        return id;
    }
}
