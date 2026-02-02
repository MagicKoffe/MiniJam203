using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    public bool isPlayer = false;

    //0 for red, 1 for blue
    public int currentColour = 0;

    public void swapColour()
    {
        if(currentColour == 1)
        {
            currentColour = 0;
        }
        else
        {
            currentColour = 1;
        }
    }

    public void setRandomColour()
    {
        currentColour = UnityEngine.Random.Range(0, 2);

    }

}
