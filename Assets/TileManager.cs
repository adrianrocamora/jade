using UnityEngine;


public class TileManager : MonoBehaviour
{
    public Material waterMat;
    public Material desertMat;
    public Material forestMat;
    public Material landMat;

    public Material selectMat;

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


    static TileManager mTileManager;

    private void Awake()
    {
        mTileManager = this;
    }

    public static TileManager Get()
    {
        return mTileManager;
    }
}