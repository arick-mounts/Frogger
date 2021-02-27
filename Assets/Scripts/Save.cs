using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Save 
{
    public List<CarData> Cars = new List<CarData>();

    public float FrogPositionX;
    public float FrogPositionY;

    public int Score = 0;
    public int Lives = 0;
    public string Name;



}
