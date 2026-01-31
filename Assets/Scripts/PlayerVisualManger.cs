using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualManger : MonoBehaviour
{
    SpriteRenderer playerSR;
    ColourManager playerCM;
    [SerializeField] SpriteRenderer playerReticle;

    private void Start()
    {
        playerSR = GetComponentInChildren<SpriteRenderer>();
        playerCM = GetComponent<ColourManager>();
    }

    private void Update()
    {
        if(playerCM.currentColour == 0)
        {
            playerSR.color = Color.red;
            playerReticle.color = Color.red;
        }
        else
        {
            playerSR.color = Color.blue;
            playerReticle.color = Color.blue;
        }
    }

}
