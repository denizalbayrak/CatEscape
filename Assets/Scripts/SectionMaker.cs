using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionMaker : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;

    public GameObject Ground;
    public List<GameObject> SectionList = new List<GameObject>();
    public List<GameObject> CreatedPath = new List<GameObject>();
    public Text ScoreText;
    Vector3 startGroundPos;
    int secNum;
    int deletedSection = 0;
    float groundPos;
    int zPos = 15;
    public bool creatingSection;

    void Start()
    {
        groundPos = Ground.transform.position.z;
        startGroundPos = Ground.transform.position;
        CreateFirstSections();
    }

    public void StartGame()
    {
        foreach (var item in CreatedPath)
        {
            Destroy(item);
        }
        CreatedPath.Clear();
        zPos = 15;
        deletedSection = 0;
        Ground.transform.position = startGroundPos;
        groundPos = startGroundPos.z;
        CreateFirstSections();
        playerMovement.StartGame();
        playerHealth.StartGame();
        ScoreText.text = "0";
    }

    private void Update()
    {
        if (groundPos + 55 <= Ground.transform.position.z)
        {
            if (!creatingSection)
            {
                creatingSection = true;
                CreateSection();
            }

        }
    }

    void CreateFirstSections()
    {
        for (int i = 0; i < SectionList.Count; i++)
        {
            secNum = Random.Range(0, SectionList.Count);
            GameObject created = Instantiate(SectionList[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
            zPos += 50;
            CreatedPath.Add(created);
        }
    }
    void CreateSection()
    {
        for (int i = 0; i < 3; i++)
        {
            secNum = Random.Range(0, SectionList.Count);
            GameObject created = Instantiate(SectionList[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
            zPos += 50;
            groundPos += 50;
            CreatedPath.Add(created);
        }

        DeleteSection();
        creatingSection = false;
    }

    void DeleteSection()
    {
        Destroy(CreatedPath[deletedSection].gameObject);
        deletedSection++;
        playerMovement.moveSpeed += (deletedSection / 10f);
    }

}
