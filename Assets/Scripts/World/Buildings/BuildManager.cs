using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject menuButton;
    public GameObject spawnPoint;
    public Material nuclearAMat;
    public Material nuclearBMat;
    public Material nuclearCMat;
    //
    public Material renewAMat;
    public Material renewBMat;
    public Material renewCMat;

    public Material coalAMat;
    public Material coalBMat;
    public Material coalCMat;

    public Material selectedMat;

    static BuildManager s_buildManager;
    public InfoController info;
    
    int index;
    protected BuildButton selectedBuildButton;
    protected BuildButton[] buildButtons;
    protected GameObject buildInfo;
    public static BuildManager Get()
    {
        return s_buildManager;
    }
    // Start is called before the first frame update
    void Start()
    {
        s_buildManager = this;
        selectedBuildButton = null;
        buildButtons = new BuildButton[9];
        index = 0;
        buildInfo = info.gameObject;
        buildInfo.SetActive(false);
        CreateButton(-0.3f, -0.3f, nuclearAMat, new NuclearA());
        CreateButton(0f, -0.3f, nuclearBMat, new NuclearB());
        CreateButton(0.3f, -0.3f, nuclearCMat, new NuclearC());
        // 
        CreateButton(-0.3f, 0f, renewAMat, new RenewA());
        CreateButton(0f, 0f, renewBMat, new RenewB());
        CreateButton(0.3f, 0f, renewCMat, new RenewC());

        CreateButton(-0.3f, 0.3f, coalAMat, new CoalA());
        CreateButton(0f, 0.3f, coalBMat, new CoalB());
        CreateButton(0.3f, 0.3f, coalCMat, new CoalC());
    }

    void CreateButton(float x, float y, Material m, Building building) { 
        GameObject nuclearA = Instantiate(menuButton, new Vector3(x, y, -0.5f), Quaternion.EulerRotation(0,0,0));
        GameObject nuclear2A = Instantiate(menuButton, new Vector3(x, y, -0.5f), Quaternion.EulerRotation(0, 0, 0));
        nuclearA.SetActive(true);
        nuclear2A.SetActive(true);
        nuclearA.transform.transform.SetParent(spawnPoint.transform, false);
        nuclear2A.transform.transform.SetParent(spawnPoint.transform, false);
        BoxCollider c = nuclear2A.AddComponent<BoxCollider>();
        c.isTrigger = true;
        BuildButton b = nuclear2A.AddComponent<BuildButton>();
        buildButtons[index++] = b;
        nuclearA.GetComponent<Renderer>().material = m;
        b.Init(building.GetPrice(), building.GetUnlockEpoch(), building, selectedMat, m, nuclear2A);
        b.SetLock(true);

        // nuclearA.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }
    public BuildButton GetSelectedBuilding()
    {
        return selectedBuildButton;
    }
    public void SetSelectedBuilding(BuildButton b)
    {
        selectedBuildButton = b;
        buildInfo.SetActive(b != null);
    }

    public void ShowBuildInfo(Building b) // string name, string description)
    {
        buildInfo.SetActive(true);
        info.UpdatePanel(b);
    }

    public void UpdateEpoch(int e)
    {
        UnityEngine.Debug.Log("Update epoch! " + e);
        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].UpdateEpoch(e);
        }
    }
    public void UnselectAll()
    {
        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].SetSelected(false);
        }
    }
}
