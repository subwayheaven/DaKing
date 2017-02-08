﻿using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public string jsonDataPath;

    GameObject mainCamera;
    PlayerAttributes playerAttributes;
    GameMaster gameMaster;
    GameObject treasureChest;

    public List<string> lstJsonData = new List<string>();

    Dictionary<string, GameObject> dicCharacterByName;

    public static ResourceManager instance;

    void Awake()
    {
        ResourceManager.instance = this;
        ResourceManager.instance.dicCharacterByName = new Dictionary<string, GameObject>();

        //Get all characters
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, jsonDataPath);
        foreach (GameObject character in characters)
        {
            //Debug.Log("loading characters...");
            StartCoroutine(LoadWWW(filePath + "/" + character.name + "Text.json"));
        }

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        gameMaster = GameObject.FindGameObjectWithTag("Controller").GetComponent<GameMaster>();
        treasureChest = GameObject.Find("TreasureChest");
        playerAttributes = GameObject.Find("king").GetComponent<PlayerAttributes>();
    }

    public GameObject getTreasureChest()
    {
        return treasureChest;
    }

    public GameMaster getGameMaster()
    {
        return gameMaster;
    }

    public PlayerAttributes getPlayerAttributes()
    {
        if(!playerAttributes)
            playerAttributes = GameObject.FindGameObjectWithTag("King").GetComponent<PlayerAttributes>();

        return playerAttributes;
    }

    public GameObject getMainCamera()
    {
        return mainCamera;
    }

    public GameObject getCharacterByName(string charName)
    {
        GameObject theChar;
        dicCharacterByName.TryGetValue(charName, out theChar);
        return theChar;
    }

    private IEnumerator LoadWWW(string filePath)
    {
        //Debug.Log("filePath is: " + filePath);
        if (filePath.Contains("://"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            //Debug.Log(www.text);
            Debug.Log("On a remote machine, loading the file via WWW");

            lstJsonData.Add(www.text);
        }
        else {
            Debug.Log("On a local machine, loading the file via System.IO");

            lstJsonData.Add(System.IO.File.ReadAllText(filePath));
        }
    }

}
