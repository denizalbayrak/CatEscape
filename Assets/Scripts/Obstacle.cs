using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    int Damage=0;
    public int Health;
    public int DamageScore;

    public void Hit()
    {
        if (Damage < Health)
        {
            Vector3 newScale = transform.localScale;
            newScale = newScale/Health;
            transform.localScale = transform.localScale-newScale;
            GameObject ScoreTxt = GameObject.Find("ScoreTxt");
            DamageScore = 1;
            ScoreTxt.GetComponent<Text>().text = (int.Parse((ScoreTxt.GetComponent<Text>().text.ToString())) + DamageScore).ToString();
            Damage++;
        }
        if (Damage == Health)
        {
            DamageScore = Health*3;
            GameObject ScoreTxt = GameObject.Find("ScoreTxt");
            ScoreTxt.GetComponent<Text>().text = (int.Parse((ScoreTxt.GetComponent<Text>().text.ToString())) + DamageScore).ToString();
            Destroy(this.gameObject);
        }
    }

    public int DamageScoreAdd()
    {
        return DamageScore;
    }


}
