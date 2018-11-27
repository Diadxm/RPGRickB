using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    //Starts new game, lets player select character and name him/her....
   public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    // Loads Save game for the player.....
    public void LoadSaveGame()
    {
        //Load save file HERE........
    }

	// Update is called once per frame
	void Update () {
		
	}
}
