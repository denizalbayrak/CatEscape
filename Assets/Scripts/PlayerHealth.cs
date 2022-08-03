using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement playerMovement;
    Vector3 PlayerScale;
    public List<GameObject> HeartList = new List<GameObject>();
    public int health=3;
    
    public void Collision()
    {
        HeartList[health-1].SetActive(false);
        health--;
        if (health > 0)
        {
            StartCoroutine(HealthLost());
        }
        else
        {
            playerMovement.stillAlive = false;
        }
    }

    public void StartGame()
    {
        health = 3;
        foreach (var item in HeartList)
        {
            item.SetActive(true);
        }
    }

    IEnumerator HealthLost()
    {
        PlayerScale = gameObject.transform.localScale;
        GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(0.25f);
        gameObject.transform.DOScale(0, 0.01f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(PlayerScale, 0.01f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(0, 0.01f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(PlayerScale, 0.01f);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
