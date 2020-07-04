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

    public BuildButton[] buildButtons;

    public GameObject buildInfo;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        buildButtons = new BuildButton[9];index = 0;
        buildInfo.SetActive(false);
        CreateButton(-0.3f, -0.3f, nuclearAMat, new NuclearAHexTile());
        CreateButton(0f, -0.3f, nuclearBMat, new NuclearBHexTile());
        CreateButton(0.3f, -0.3f, nuclearCMat, new NuclearCHexTile());
        // 
        CreateButton(-0.3f, 0f, renewAMat, new HexTile());
        CreateButton(0f, 0f, renewBMat, new HexTile());
        CreateButton(0.3f, 0f, renewCMat, new HexTile());

        CreateButton(-0.3f, 0.3f, coalAMat, new NuclearAHexTile());
        CreateButton(0f, 0.3f, coalBMat, new NuclearBHexTile());
        CreateButton(0.3f, 0.3f, coalCMat, new NuclearCHexTile());
    }
    void CreateButton(float x, float y, Material m, HexTile tile) { 
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
        b.Init(tile.GetPrice(), tile.GetUnlockEpoch(), tile, selectedMat, m, nuclear2A, buildInfo, this);
        Debug.Log("jiji");
        b.SetLock(true);
        // nuclearA.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
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
