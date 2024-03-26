using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Player
{
    
    protected override void Start()
    {   
        base.Start();
        velocidad = 7f;
    }

    protected override void FixedUpdate()
    {
        velX = Input.GetAxis("Horizontal");
        velY = Input.GetAxis("Vertical");
        Movement(velocidad);
    }

}
