using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HexTileResources
{
    WATER, COAL, WIND, WATER_POWER, LAND, FOREST
}

public class HexTile : MonoBehaviour
{
    Material mat;
    public const int space = 3;
    List<Building> buildings = new List<Building>(space);

    protected HexTileResources[] hexTileResources;
    virtual public HexTile Copy()
    {
        HexTile h = new HexTile();
        h.mat = mat;
        return h;
    }
    public override string ToString() => "workwork";
    // carbon sink negative
    // Carbon producer
    virtual public float GetC02Polution() {
        // get pollution of buildings
        // get carbon sink of free space
        float co2 = 0;
        foreach (Building building in buildings)
        {
            co2 += building.GetC02Polution();
        }
        co2 -= GetCarbonSink(); 
        return co2;
    }
    virtual public float GetCarbonSink()
    {
        return 0.0f;
    }
    virtual public float GetNuclearWaste()
    {
        return 0.0f;
    }
    virtual public float GetPower()
    {
        float power = 0;
        foreach (Building building in buildings)
        {
            power += building.GetPower();
        }
        return power;
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
    virtual public float GetMoney()
    {
        float hexMoney = 0;
        foreach (Building building in buildings)
        {
            hexMoney += building.GetMoney();
        }        
        return hexMoney;
    }

    public bool HasSpace()
    {
        int space = buildings.Capacity - buildings.Count;
        return space >= 1;
    }
    bool HasSpace(Building b)
    {
        int space = buildings.Capacity - buildings.Count;
        return space >= b.space;
    }


    bool AllowsBuilding(Building b)
    {
        if(b.hexTileRequirements != null)
            for (int i = 0; i < b.hexTileRequirements.Length; ++i)
                if (HasTypeAvailable(b.hexTileRequirements[i]) == false)
                    return false;
        return true;
    }

    private bool HasTypeAvailable(HexTileResources hexTileResource)
    {
        for (int i = 0; i < hexTileResources.Length; ++i)
            if (hexTileResource == hexTileResources[i])
                return true;
        return false;
    }

    /*
public static void SetTile(GameObject tile)
{
   tile.AddComponent<HexTile>();
   tile.GetComponent<Renderer>().material = TileManager.Get().waterMat;
}*/
    /*
        public virtual void AddTileLocal(GameObject tile) {
            // Debug.Log("HexTile.AddTileLocal");
            AddBuilding(tile);
        }*/

    // public virtual GameObject Get3dObject() { return null; }
    // public static void AddTile(GameObject tile) { }
    virtual public void TriggerExit(GameObject tile)
    {
        tile.GetComponent<Renderer>().material = mat;
        Debug.Log("TriggerExit");
    }
    virtual public string TriggerEnter(BuildButton selectedBuildButton)
    {
        // Debug.Log("HexTile.TriggerEnter" + selectedBuildButton.nextTile);
        GameObject tile = gameObject;
        mat = tile.GetComponent<Renderer>().material;
        tile.GetComponent<Renderer>().material = TileManager.Get().selectMat;
        // NuclearAHexTile.AddTile(tile);
        if (selectedBuildButton != null)
        {
            Building b = selectedBuildButton.building;
            Debug.Log("space " + space);
            Debug.Log("space needed " + b.space);
            if (!HasSpace(b))
            {
                tile.GetComponent<Renderer>().material = TileManager.Get().fullMat;
                return "No hay espacio suficiente para el edificio";
            }
            if (!AllowsBuilding(b))
            {
                tile.GetComponent<Renderer>().material = TileManager.Get().NotAllowedMat;
                return "No estan los recursos para poner este edificio aqui";
            }
            // Debug.Log("selectedBuildButton.TriggerEnter" + selectedBuildButton.nextTile);

            AddBuilding(b, buildings.Count);
            // Debug.Log("selectedBuildButton.TriggerEnter" + selectedBuildButton.nextTile);
            // selectedBuildButton.nextTile.AddTileLocal(tile);
        }
        return null;
    }
    public void AddBuilding(Building b)
    {
        GameObject tile = gameObject;
        b.AddBuilding(tile,-1);
        PlaceBuild(b);
    }
    public void AddBuilding(Building b, float i)
    {
        GameObject tile = gameObject;
        b.AddBuilding(tile,i);
        PlaceBuild(b);
    }
    void PlaceBuild(Building b)
    {
        float totalPrice = 0;
        for (int i = 0; i < b.space; i++)
        {
            Building bNew = Building.Clone(b);
            buildings.Add(bNew);
            totalPrice += b.GetPrice();
        }
        WorldManager.Get().Pay(totalPrice);

    }

}

public class WaterHexTile : HexTile
{
   
    void Awake()
    {
        hexTileResources = new HexTileResources[] { HexTileResources.WATER };
    }
    public static void SetTile(GameObject tile)
    {
        tile.AddComponent<WaterHexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().waterMat;
    }
    override public float GetCarbonSink()
    {
        return 0.0f;
    }
}
public class ForestHexTile : HexTile
{
    void Awake()
    {
        hexTileResources = new HexTileResources[] { HexTileResources.LAND, HexTileResources.FOREST };
    }
    public static void SetTile(GameObject tile)
    {
        tile.AddComponent<ForestHexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().forestMat;
    }
    override public float GetCarbonSink()
    {
        return 10.0f;
    }
}
public class LandHexTile : HexTile
{
    void Awake()
    {
        hexTileResources = new HexTileResources[] { HexTileResources.LAND };
    }
    public new static void SetTile(GameObject tile)
    {
        tile.AddComponent<LandHexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().landMat;
    }
    override public float GetCarbonSink()
    {
        return 3.0f;
    }
}
public class DesertHexTile : HexTile
{
    void Awake()
    {
        hexTileResources = new HexTileResources[] { HexTileResources.LAND };
    }
    public static void SetTile(GameObject tile)
    {
        tile.AddComponent<DesertHexTile>();
        tile.GetComponent<Renderer>().material = TileManager.Get().desertMat;
    }
    override public float GetCarbonSink()
    {
        return 0.0f;
    }
}

//public class HouseAHexTile : HexTile
//{
//    bool firstTime = true;
//    public override void AddTileLocal(GameObject tile) {
//        AddTile(tile);
//    }
//    public override GameObject Get3dObject()
//    {
//        Debug.Log("Going nuckearA ");
//        return null;
//        //return TileManager.Get().houseA;
//    }

//    /*
//    public static void AddTile(GameObject tile)
//    {
//        float x = tile.transform.position.x;
//        float y = tile.transform.position.y;
//        float z = tile.transform.position.z;
//        Vector3 normal = new Vector3(x, y, z);
//        normal.Normalize();
//        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
//        GameObject o = Instantiate(TileManager.Get().houseA, new Vector3(x, y, z) + normal * 0.036f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
//        o.SetActive(true);
//        o.transform.transform.SetParent(tile.transform, true);
//    }*/

//    public override float GetUnlockEpoch()
//    {
//        return 4;
//    }
//}

//    // all units are in killowats
//public class NuclearAHexTile : HexTile
//{
//    bool firstTime = true;

//    public NuclearAHexTile()
//    {
//        tittle = "Nuclear Power Plant 1960";
//        description = "This is just a test";

//    }
//    public override void AddTileLocal(GameObject tile)
//    {
//        AddTile(tile);
//    }
//    /*
//    public static void AddTile(GameObject tile)
//    {
//        float x = tile.transform.position.x;
//        float y = tile.transform.position.y;
//        float z = tile.transform.position.z;
//        Vector3 normal = new Vector3(x, y, z);
//        normal.Normalize();
//        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
//        GameObject o = Instantiate(TileManager.Get().nuclearA, new Vector3(x, y, z) + normal * 0.026f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
//        o.SetActive(true);
//        o.transform.transform.SetParent(tile.transform, true);
//        o.transform.localScale *= 5.015f;
//        // o.transform.localScale *= 0.015f;
//    }*/
//    public override GameObject Get3dObject() {
//        return null;
//        /*
//        Debug.Log("Going nuckearA " );
//        return TileManager.Get().nuclearA;
//    */
//        }

//    public override float GetUnlockEpoch()
//    {
//        return 5;
//    }
//    override public float GetC02Polution()
//    {
//        if(firstTime)
//            return 10.0f;
//        return 0.0f;
//    }

//    override public float GetPower()
//    {
//        return 5.0f * 1000 ; 
//    }

//    override public float GetNuclearWaste()
//    {
//        return 10.0f;
//    }

//    override public float GetWellness()
//    {
//        return 7.0f;
//    }
//}


//public class NuclearBHexTile : HexTile
//{
//    bool firstTime = true;
//    override public float GetC02Polution()
//    {
//        if (firstTime)
//            return 10.0f;
//        return 0.0f;
//    }
//    public NuclearBHexTile()
//    {
//        tittle = "Nuclear Power Plant 1960";
//        description = "This is just a test";
//    }
//    /*
//    public override void AddTileLocal(GameObject tile)
//    {
//        Debug.Log("Puto NuclearBHexTile");
//        NuclearBHexTile.AddTile(tile);
//    }*/
//    public override GameObject Get3dObject()
//    {
//        return null;
//        /*
//        Debug.Log("Going nuckearB");
//        GameObject o = TileManager.Get().nuclearB;
//        o.transform.localScale *= 0.015f;
//        return o;*/
//    }
//    /*
//    public static void AddTile(GameObject tile)
//    {
//        float x = tile.transform.position.x;
//        float y = tile.transform.position.y;
//        float z = tile.transform.position.z;
//        Vector3 normal = new Vector3(x, y, z);
//        normal.Normalize();
//        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
//        GameObject o = Instantiate(TileManager.Get().nuclearB, new Vector3(x, y, z) + normal * 0.026f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
//        o.SetActive(true);
//        o.transform.transform.SetParent(tile.transform, true);
//        o.transform.localScale = new Vector3(300.0f, 300.0f, 300.0f);//5.015f;
//        // o.transform.localScale *= 0.015f;
//    }*/
//    override public float GetPower()
//    {
//        return 1634.0f * 1000;
//    }

//    override public float GetNuclearWaste()
//    {
//        return -8.0f;
//    }

//    override public float GetWellness()
//    {
//        return 9.0f;
//    }
//}

//public class NuclearCHexTile : HexTile
//{
//    bool firstTime = true;
//    override public float GetC02Polution()
//    {
//        if (firstTime)
//            return 10.0f;
//        return 0.0f;
//    }
//    /*
//    public static void AddTile(GameObject tile)
//    {

//        float x = tile.transform.position.x;
//        float y = tile.transform.position.y;
//        float z = tile.transform.position.z;
//        Vector3 normal = new Vector3(x, y, z);
//        normal.Normalize();
//        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
//        GameObject o = Instantiate(TileManager.Get().nuclearB, new Vector3(x, y, z) + normal * 0.026f, r); //Quaternion.Euler(new Vector3(nx,ny,nz)));
//        o.SetActive(true);
//        o.transform.transform.SetParent(tile.transform, true);
//        o.transform.localScale *= 0.015f;
//    }*/
//    override public float GetPower()
//    {
//        return 32034.0f * 1000;
//    }

//    override public float GetNuclearWaste()
//    {
//        return 0.0f;
//    }

//    override public float GetWellness()
//    {
//        return 11.0f;
//    }
//}
