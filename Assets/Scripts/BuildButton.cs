using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public float price;
    float unlockEpoch;
    Material selectedMat;
    Material iconMat;
    // public GameObject buildInfo;
    //BuildManager buildManager;
    bool isSelected;
    /*
    float power;
    float C02Waste;
    float NuclearWaste;
    */
    public HexTile nextTile;
    // private HexTile hexTile;
    bool isLocked;
    GameObject lockGameObj;

    public void Init(float price, float unlockEpoch, HexTile nextTile, 
        Material selectedMat, 
        Material iconMat, GameObject lockGameObj)
        //, BuildManager b)
    {
        this.price = price;
        this.unlockEpoch = unlockEpoch;
        this.nextTile = nextTile;//.Copy();
        //hexTile = nextTile.Copy();
        this.iconMat = iconMat;
        this.selectedMat = selectedMat;
        this.lockGameObj = lockGameObj;
        lockGameObj.GetComponent<Renderer>().material = iconMat;
        // buildManager = b;
        isSelected = false;
        Debug.Log("jiji nextTile" + nextTile);
    }

    public void SetLock(bool l)
    {
        isLocked = l;
        lockGameObj.SetActive(!l);
    }
    public void SetSelected(bool b)
    {
        BuildManager buildManager = BuildManager.Get();
        // unselect all,
        // select this one
        Debug.Log("SetSelected " + b + isSelected);
        if (b && !isSelected)
        {
            buildManager.UnselectAll();
            OnButtonSelected();
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = iconMat;
            isSelected = false;
            buildManager.selectedBuildButton = null;

            buildManager.buildInfo.SetActive(false);

            
            
        }
    }
    void OnButtonSelected()
    {
        BuildManager buildManager = BuildManager.Get();

        gameObject.GetComponent<Renderer>().material = selectedMat;
        isSelected = true;
        buildManager.selectedBuildButton = this;
        // nextTile.name, nextTile.description
        Debug.Log(this.price);
        buildManager.ShowBuildInfo(nextTile.tittle, "jojo");
    }
    public void UpdateEpoch(int e)
    {
        if (e >= unlockEpoch)
        {
            SetLock(false);
        }
    }
    /*
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coll");
        gameObject.GetComponent<Renderer>().material = selectedMat;
       // buildInfo.SetActive(true);
        // other.gameObject.GetComponent<Renderer>().material = world.desertMat;
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Coll");
       //  buildInfo.SetActive(false);

        // other.gameObject.GetComponent<Renderer>().material = world.waterMat;
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Coll");
    }
    */
}
