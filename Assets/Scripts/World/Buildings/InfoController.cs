using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text tName;
    public Text tDescription;
    public Text tStatistics;


    public void UpdatePanel(Building b)//string name, string description)
    {
        tName.text = b.tittle;

        tStatistics.text = $"{b.GetPrice()} $\n{b.GetC02Polution()} CO2\n{b.GetPower()} Power\n{b.space} Space"; //  + "";
        tDescription.text = b.description;
    }
}
