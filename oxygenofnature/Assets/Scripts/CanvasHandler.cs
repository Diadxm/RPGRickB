using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour {
    public Toggle Male;
    public Toggle Female;

    private int GenderNum;
    public int GetGenderNum() { return GenderNum; }

    private int ModelSelected;
    public int GetModelSelected() { return ModelSelected; }

    public GameObject NameGender;
    public GameObject BaseMale;
    public GameObject BaseFemale;
    public GameObject Male2;
    public GameObject Male3;
    public GameObject Female2;
    public GameObject Female3;

    public GameObject FemaleChoice;

    public GameObject MaleChoice;

	// Use this for initialization
	void Start () {
		
	}

    // Selects male and disables female
    public void SelectMale()
    {
        Male.isOn = true;
        Female.isOn = false;
        GenderNum = 0;
    }

    // Selects female and disables male
    public void SelectFemale()
    {
        Female.isOn = true;
        Male.isOn = false;
        GenderNum = 1;
    }

    public void NameGenderDisable()
    {
        if (GenderNum == 1)
        {
            NameGender.SetActive(false);
            BaseMale.SetActive(false);
            FemaleChoice.SetActive(true);

        }

        else if (GenderNum == 0)
        {
            NameGender.SetActive(false);
            BaseFemale.SetActive(false);
            MaleChoice.SetActive(true);
        }
    }

    public void SelectFemale1()
    {
        BaseFemale.SetActive(true);
        Female2.SetActive(false);
        Female3.SetActive(false);
        ModelSelected = 0;
    }

    public void SelectFemale2()
    {
        BaseFemale.SetActive(false);
        Female2.SetActive(true);
        Female3.SetActive(false);
        ModelSelected = 1;
    }

    public void SelectFemale3()
    {
        BaseFemale.SetActive(false);
        Female2.SetActive(false);
        Female3.SetActive(true);
        ModelSelected = 2;
    }

    public void SelectMale1()
    {
        BaseMale.SetActive(true);
        Male2.SetActive(false);
        Male3.SetActive(false);
        ModelSelected = 3;
    }

    public void SelectMale2()
    {
        BaseMale.SetActive(false);
        Male2.SetActive(true);
        Male3.SetActive(false);
        ModelSelected = 4;
    }

    public void SelectMale3()
    {
        BaseMale.SetActive(false);
        Male2.SetActive(false);
        Male3.SetActive(true);
        ModelSelected = 5;
    }

	// Update is called once per frame
	void Update () {
        Debug.Log(ModelSelected);
	}
}
