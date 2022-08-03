using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ShootManager : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Player;
    public Image LoadingBar;
    public Button Shootbtn;
    float currentValue;
    public bool shooted;
    public float speed;
    public PlayerMovement playerMovement;

    private void Update()
    {
        if (shooted)

            if (currentValue < 100)
            {
                currentValue += speed * Time.deltaTime * (playerMovement.moveSpeed);
            }

        LoadingBar.fillAmount = currentValue / 100;

        if (LoadingBar.fillAmount == 1)
        {
            Shootbtn.interactable = true;
            shooted = false;

        }
    }
    public void BulletCreate()
    {
        LoadingBar.fillAmount = 0;
        currentValue = 0;
        shooted = true;
        Shootbtn.interactable = false;
        GameObject bullet = Instantiate(Bullet, new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z + 1), Quaternion.identity);

    }

}
