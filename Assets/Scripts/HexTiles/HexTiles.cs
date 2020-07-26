using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HexTile : MonoBehaviour
{
    Material mat;
    public string tittle="Power Plant", description="";
    virtual public HexTile Copy()
    {
        HexTile h = new HexTile();
        h.tittle = tittle;
        h.description = description;
        h.mat = mat;
        return h;
    }
    public override string ToString() => "workwork";
    virtual public float GetC02Polution() {
        return 0.0f;
    }

    virtual public float GetNuclearWaste()
    {
        return 0.0f;
    }
    virtual public float GetPower()
    {
        return 0.0f;
    }
    virtual public float GetWellness()
    {
        return 0.0f;
    }
    virtual public float GetPrice()
    {
        return 7.0f;
    }
    virtual public float GetUnlockEpoch()
    {
        return 0.0f;
    }
    public static void SetTile(GameObject tile)
    {
        tile.AddComponent<HexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().waterMat;
    }
    public virtual void AddTileLocal(GameObject tile) {
        Debug.Log("HexTile.AddTileLocal");
        AddTile(tile);
    }
    public virtual GameObject Get3dObject() { return null; }
    // public static void AddTile(GameObject tile) { }
    virtual public void TriggerExit(GameObject tile)
    {
        tile.GetComponent<Renderer>().material = mat;
        Debug.Log("TriggerExit");
    }
    virtual public void TriggerEnter(GameObject tile, BuildButton selectedBuildButton)
    {
        // Debug.Log("HexTile.TriggerEnter" + selectedBuildButton.nextTile);

        mat = tile.GetComponent<Renderer>().material;
        tile.GetComponent<Renderer>().material = TileManager.Get().selectMat;
        // NuclearAHexTile.AddTile(tile);
        if (selectedBuildButton != null)
        {
            Debug.Log("selectedBuildButton.TriggerEnter" + selectedBuildButton.nextTile);

            selectedBuildButton.nextTile.AddTileLocal(tile);

        }
    }
    public void AddTile(GameObject tile)
    {
        float x = tile.transform.position.x;
        float y = tile.transform.position.y;
        float z = tile.transform.position.z;
        Vector3 normal = new Vector3(x, y, z);
        normal.Normalize();
        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
        GameObject o = Instantiate(Get3dObject(), new Vector3(x, y, z) + normal * 0.036f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
        o.SetActive(true);
        o.transform.transform.SetParent(tile.transform, true);
    }
}

public class WaterHexTile : HexTile
{
    public new static void SetTile(GameObject tile)
    {
        tile.AddComponent<WaterHexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().waterMat;
    }
}
public class ForestHexTile : HexTile
{
    public new static void SetTile(GameObject tile)
    {
        tile.AddComponent<ForestHexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().forestMat;
    }
}
public class LandHexTile : HexTile
{
    public new static void SetTile(GameObject tile)
    {
        tile.AddComponent<LandHexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().landMat;
    }
}
public class DesertHexTile : HexTile
{
    public new static void SetTile(GameObject tile)
    {
        tile.AddComponent<DesertHexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().desertMat;
    }
}

public class HouseAHexTile : HexTile
{
    bool firstTime = true;
    public override void AddTileLocal(GameObject tile) {
        AddTile(tile);
    }
    public override GameObject Get3dObject()
    {
        Debug.Log("Going nuckearA ");
        return TileManager.Get().houseA;
    }

    /*
    public static void AddTile(GameObject tile)
    {
        float x = tile.transform.position.x;
        float y = tile.transform.position.y;
        float z = tile.transform.position.z;
        Vector3 normal = new Vector3(x, y, z);
        normal.Normalize();
        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
        GameObject o = Instantiate(TileManager.Get().houseA, new Vector3(x, y, z) + normal * 0.036f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
        o.SetActive(true);
        o.transform.transform.SetParent(tile.transform, true);
    }*/

    public override float GetUnlockEpoch()
    {
        return 4;
    }
}

    // all units are in killowats
public class NuclearAHexTile : HexTile
{
    bool firstTime = true;

    public NuclearAHexTile()
    {
        tittle = "Nuclear Power Plant 1960";
        description = "This is just a test";

    }
    public override void AddTileLocal(GameObject tile)
    {
        AddTile(tile);
    }
    /*
    public static void AddTile(GameObject tile)
    {
        float x = tile.transform.position.x;
        float y = tile.transform.position.y;
        float z = tile.transform.position.z;
        Vector3 normal = new Vector3(x, y, z);
        normal.Normalize();
        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
        GameObject o = Instantiate(TileManager.Get().nuclearA, new Vector3(x, y, z) + normal * 0.026f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
        o.SetActive(true);
        o.transform.transform.SetParent(tile.transform, true);
        o.transform.localScale *= 5.015f;
        // o.transform.localScale *= 0.015f;
    }*/
    public override GameObject Get3dObject() {
        Debug.Log("Going nuckearA " );
        return TileManager.Get().nuclearA;
    }

    public override float GetUnlockEpoch()
    {
        return 5;
    }
    override public float GetC02Polution()
    {
        if(firstTime)
            return 10.0f;
        return 0.0f;
    }

    override public float GetPower()
    {
        return 5.0f * 1000 ; 
    }

    override public float GetNuclearWaste()
    {
        return 10.0f;
    }

    override public float GetWellness()
    {
        return 7.0f;
    }
}


public class NuclearBHexTile : HexTile
{
    bool firstTime = true;
    override public float GetC02Polution()
    {
        if (firstTime)
            return 10.0f;
        return 0.0f;
    }
    public NuclearBHexTile()
    {
        tittle = "Nuclear Power Plant 1960";
        description = "This is just a test";
    }
    /*
    public override void AddTileLocal(GameObject tile)
    {
        Debug.Log("Puto NuclearBHexTile");
        NuclearBHexTile.AddTile(tile);
    }*/
    public override GameObject Get3dObject()
    {
        Debug.Log("Going nuckearB");
        GameObject o = TileManager.Get().nuclearB;
        o.transform.localScale *= 0.015f;
        return o;
    }
    /*
    public static void AddTile(GameObject tile)
    {
        float x = tile.transform.position.x;
        float y = tile.transform.position.y;
        float z = tile.transform.position.z;
        Vector3 normal = new Vector3(x, y, z);
        normal.Normalize();
        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
        GameObject o = Instantiate(TileManager.Get().nuclearB, new Vector3(x, y, z) + normal * 0.026f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
        o.SetActive(true);
        o.transform.transform.SetParent(tile.transform, true);
        o.transform.localScale = new Vector3(300.0f, 300.0f, 300.0f);//5.015f;
        // o.transform.localScale *= 0.015f;
    }*/
    override public float GetPower()
    {
        return 1634.0f * 1000;
    }

    override public float GetNuclearWaste()
    {
        return -8.0f;
    }

    override public float GetWellness()
    {
        return 9.0f;
    }
}

public class NuclearCHexTile : HexTile
{
    bool firstTime = true;
    override public float GetC02Polution()
    {
        if (firstTime)
            return 10.0f;
        return 0.0f;
    }
    /*
    public static void AddTile(GameObject tile)
    {

        float x = tile.transform.position.x;
        float y = tile.transform.position.y;
        float z = tile.transform.position.z;
        Vector3 normal = new Vector3(x, y, z);
        normal.Normalize();
        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
        GameObject o = Instantiate(TileManager.Get().nuclearB, new Vector3(x, y, z) + normal * 0.026f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
        o.SetActive(true);
        o.transform.transform.SetParent(tile.transform, true);
        o.transform.localScale *= 0.015f;
    }*/
    override public float GetPower()
    {
        return 32034.0f * 1000;
    }

    override public float GetNuclearWaste()
    {
        return 0.0f;
    }

    override public float GetWellness()
    {
        return 11.0f;
    }
}
