﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSave : MonoBehaviour {

    private string CharacterName;

    public string GetCharName() { return CharacterName; }
    public void SetCharName(string Name) { CharacterName = Name; }

    private int ModelNum;
    public int GetModelNum() { return ModelNum; }
    public void SetModelNum(int Num) { ModelNum = Num; }

    private int gender;
    public int GetGender() { return gender; }
    public void SetGender(int Gender) { gender = Gender; }

	// Use this for initialization
	void Start () {

	}

    public void SavePlayerPrefs()
    {
        PlayerPrefs.Save();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
