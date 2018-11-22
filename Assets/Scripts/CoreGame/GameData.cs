using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    [SerializeField]
    float posX;
    [SerializeField]
    float posY;
    [SerializeField]
    float posZ;
    public float PosX
    {
        get
        {
            return posX;
        }

        set
        {
            posX = value;
        }
    }

    public float PosY
    {
        get
        {
            return posY;
        }

        set
        {
            posY = value;
        }
    }

    public float PosZ
    {
        get
        {
            return posY;
        }

        set
        {
            posY = value;
        }
    }

    public GameData(float posX, float posY, float posZ)
    {
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
    }

    public GameData() { }
   
    public Vector3 PosVector
    {
        get
        {
            return new Vector3(posX, posY, posZ);
        }
    }
}
