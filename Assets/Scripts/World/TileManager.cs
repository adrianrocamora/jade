using Boo.Lang;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour
{
    public Material waterMat;
    public Material desertMat;
    public Material forestMat;
    public Material landMat;

    public Material selectMat;
    public Material fullMat;
    public Material NotAllowedMat;
    /*
    public GameObject houseA;
    
    public GameObject nuclearA;
    public GameObject nuclearB;
    public GameObject nuclearC;

    public GameObject renewA;
    public GameObject renewB;
    public GameObject renewC;

    public GameObject coalA;
    public GameObject coalB;
    public GameObject coalC;
    */
    static TileManager mTileManager;
    Queue<GameObject> freeLand;
    Queue<GameObject> freeForest;

    public void AddForestTile(GameObject forestTile)
    {
        freeForest.Enqueue(forestTile);
    }
    public void AddLandTile(GameObject forestTile)
    {
        freeLand.Enqueue(forestTile);
    }

    public HexTile GetNextFreeTile()
    {
        HexTile land;
        GameObject gameObject;
        do {
            gameObject = freeLand.Dequeue();
            land = gameObject.GetComponent<HexTile>();
        }
        while (!land.HasSpace());
        freeLand.Enqueue(gameObject);
        // if empty return forest
        return land;
    }


    private void Awake()
    {
        mTileManager = this;
        freeLand = new Queue<GameObject>();
        freeForest = new Queue<GameObject>();
    }

    public static TileManager Get()
    {
        return mTileManager;
    }
}