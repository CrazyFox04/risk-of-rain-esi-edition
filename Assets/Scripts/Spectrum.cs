using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class Spectrum : AbstractEnemy
{
    
    
    //-----------------Enemy Movement-----------------
    
    public void jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }
    
    //-----------------Enemy Attack-----------------
    
    
    //-----------------Enemy On Collision Behaviors-----------------
    
    void OnTriggerEnter2D(Collider2D other)
    {
        jump();
    }
    
    //-----------------Enemy Animation-----------------
    
}