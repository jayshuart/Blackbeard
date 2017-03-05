﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles main play cycle logic. Anything within game manager should happen during gameplay
/// and terminate when advancing to other menus.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region Fields
    //Assigned in inspector
    //public Player player;

    // put enums in here so all NPC's and squads have access to them
    public enum Team { RED, BLUE };

    // temporary value, will delete when gameManager is overhauled
    [SerializeField]
    private int numSquadsPerTeam;

    // list of all red and blue squads
    [SerializeField]
    private List<GameObject> blueSquads;
    [SerializeField]
    private List<GameObject> redSquads;

    // temporary storage for squad lists
    //private GameObject[] blueSquadsArray;
    //private GameObject[] redSquadsArray;

    #endregion

    #region Properties

    #endregion

    protected GameManager(){}

	void Awake()
	{
        // assign arrays
        //blueSquadsArray = GameObject.FindGameObjectsWithTag("BlueSquad");
        //redSquadsArray = GameObject.FindGameObjectsWithTag("RedSquad");
        Debug.Log("Game Manager is alive");

        //redSquads = new List<GameObject>();
        //blueSquads = new List<GameObject>();

        StartRound();
	}

	public void StartGame()
	{
		MenuManager.Instance.GoToScreen("GameScreen");
		
	}

	/// <summary>
	/// Will be called in other places later on
    /// assign enemies to squads
	/// </summary>
	public void StartRound()
	{
        //set score to zero
        
        for (int i = 0; i < numSquadsPerTeam; i++)
        {
            redSquads[i].GetComponent<SquadManager>().EnemySquadObjects = blueSquads;
            blueSquads[i].GetComponent<SquadManager>().EnemySquadObjects = redSquads;          
        }

	}

    void Update()
    {


    }
}
