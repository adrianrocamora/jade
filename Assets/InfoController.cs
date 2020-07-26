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

    public void UpdatePanel(string name, string description)
    {
        tName.text = name;
        tDescription.text = description;
    }
}
