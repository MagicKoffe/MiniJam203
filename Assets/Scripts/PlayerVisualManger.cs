using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualManger : MonoBehaviour
{
    SpriteRenderer playerSR;
    ColourManager playerCM;
    TrailRenderer playerTR;
    public ParticleSystem deathParticle;
    [SerializeField] SpriteRenderer playerReticle;

    private void Start()
    {
        playerSR = GetComponentInChildren<SpriteRenderer>();
        playerCM = GetComponent<ColourManager>();
        playerTR = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if(playerCM.currentColour == 0)
        {
            playerSR.color = Color.red;
            playerReticle.color = Color.red;
            playerTR.startColor = Color.red;
            playerTR.endColor = Color.red;
            deathParticle.startColor = Color.red;
        }
        else
        {
            playerSR.color = Color.blue;
            playerReticle.color = Color.blue;
            playerTR.startColor = Color.blue;
            playerTR.endColor = Color.blue;
            deathParticle.startColor = Color.blue;
        }
    }

}
