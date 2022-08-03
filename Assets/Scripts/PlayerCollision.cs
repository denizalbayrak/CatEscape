using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public UI_Manager uiManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "NotShootableObstacle")
        {
          //  collision.gameObject.GetComponent<Obstacle>().Hit();            
            playerHealth.Collision();
           if(playerHealth.health <= 0)
            {
                uiManager.Fail();
            }
            else
            {
                collision.gameObject.SetActive(false);
            }
        }

    }
    
   
       
}
