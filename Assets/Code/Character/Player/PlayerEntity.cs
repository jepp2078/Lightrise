using UnityEngine;
using System.Collections;

public class PlayerEntity
{
    private int id;
    private int x, y;

    public PlayerEntity(int i, int x, int y)
    {
        this.id = i; //Skal flyttes til playerobject
        this.x = x;
        this.y = y;
    }

    public int getID()
    {
        return id;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public void moveX(int x)
    {
        this.x += x;
    }

    public void moveY(int y)
    {
        this.y += y;
    }
}
