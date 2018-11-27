using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameManager instance = null;
    public static GameManager Instance { get { return Instance; } }

    public PlayerPrefsSave PlayerPrefSave;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        PlayerPrefSave = GetComponent<PlayerPrefsSave>();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
