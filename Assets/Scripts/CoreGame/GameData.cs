﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    /*
    [SerializeField]
    float posX;
    [SerializeField]
    float posY;
    [SerializeField]
    float posZ;
    */
    [SerializeField]
    float[] posX;
    [SerializeField]
    float[] posY;
    [SerializeField]
    float[] posZ;

    /*
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
    */
    public GameData(float[] posX, float[] posY, float[] posZ)
    {
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
    }
    public GameData(Vector3[] positions)
    {
        posX = new float[positions.Length];
        posY = new float[positions.Length];
        posZ = new float[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            posX[i] = positions[i].x;
            posY[i] = positions[i].y;
            posZ[i] = positions[i].z;
        }
    }

    public GameData() { }

    public Vector3[] PosVectors
    {
        get
        {
            Vector3[] vectors = new Vector3[4];
            for(int i = 0; i < posX.Length; i++)
            {
                vectors[i] = new Vector3(posX[i], posY[i], posZ[i]);
            }

            return vectors;
        }
    }
}

