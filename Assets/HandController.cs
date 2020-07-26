using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // static BuildButton selectedBuildButton = null;
    GameObject carryObject = null;
    // public TileManager world;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(carryObject);
        if (carryObject != null)
        {
//            Debug.Log("Update " + gameObject.transform.position);
  //          carryObject.transform.position = gameObject.transform.position;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        // other.gameObject.GetComponent<Renderer>().material = world.desertMat;

        HexTile hexTile = other.gameObject.GetComponent<HexTile>();
        // BuildButton selectedBuildButton = BuildManager.Get().selectedBuildButton; // other.gameObject.GetComponent<BuildButton>();
        BuildButton selectedBuildButton = other.gameObject.GetComponent<BuildButton>();
        // Debug.Log("BuildBtn " + selectedBuildButton);
        if (hexTile != null)
        {
            Debug.Log("Coll world " + BuildManager.Get().selectedBuildButton);
            hexTile.TriggerEnter(other.gameObject, BuildManager.Get().selectedBuildButton);
        }
        else if(selectedBuildButton != null)
        {
            selectedBuildButton.SetSelected(true);
            Debug.Log("Coll board " + selectedBuildButton);
            GameObject o = selectedBuildButton.nextTile.Get3dObject();
            Debug.Log("Coll board 1 " + o);
            /*
            if (o != null)
            {
                Debug.Log("Going nuckear " );

                o.SetActive(true);
                carryObject = o;

                o.transform.position = gameObject.transform.position;//  new Vector3(x, y, z);

            }*/
            // show it in the hand
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Coll exit trigger" + other.gameObject.name);

        if (other.gameObject.GetComponent<HexTile>() != null)
              other.gameObject.GetComponent<HexTile>().TriggerExit(other.gameObject);
       // other.gameObject.GetComponent<Renderer>().material = world.waterMat;

    }
    /*
    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Coll");
    }*/
}
