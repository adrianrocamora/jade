using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum BuildingType{ 
    HOUSE, NUCLEAR_A,
}
public abstract class Building 
{
    protected float c02Pollution;
    protected float nuclearWaste;
    protected float power;
    protected float wellness;
    protected float price;
    protected float unlockEpoch;
    public int space;
    public HexTileResources[] hexTileRequirements;
    public string tittle, description;
    protected BuildingType type;
    /*
    protected Building(float c02Pollution, float nuclearWaste, float power, float wellness, float price, float unlockEpoch, string tittle, string description)
    {
        this.c02Pollution = c02Pollution;
        this.nuclearWaste = nuclearWaste;
        this.power = power;
        this.wellness = wellness;
        this.price = price;
        this.unlockEpoch = unlockEpoch;
        this.tittle = tittle;
        space = 1;
        this.description = description;
    }
    */
    protected Building(Building b)
    {
        this.c02Pollution = b.c02Pollution;
        this.nuclearWaste = b.nuclearWaste;
        this.power = b.power;
        this.wellness = b.wellness;
        this.price = b.price;
        this.unlockEpoch = b.unlockEpoch;
        this.tittle = b.tittle;
        this.description = b.description;
    }

    public static Building GetHouse()
    {
        return new BuildingA();
    }
    public static Building Clone(Building b)
    {
        switch (b.type)
        {
            case BuildingType.HOUSE:
                return new BuildingA(b);
        }
        return null;
        // return new Building(b);
    }
    public Building()
    {
        c02Pollution = 0.0f;
        nuclearWaste = 0.0f;
        power = 0.0f;
        wellness = 0.0f;
        price = 0.0f;
        unlockEpoch = 0.0f;
        tittle = "Title generic";
        description = "Dexccriot generic";
        hexTileRequirements = null;
        space = 1;
    }
    virtual public float GetC02Polution()
    {
        return c02Pollution;
    }

    virtual public float GetNuclearWaste()
    {
        return nuclearWaste;
    }
    virtual public float GetPower()
    {
        return power;
    }
    virtual public float GetWellness()
    {
        return wellness;
    }
    virtual public float GetPrice()
    {
        return price;
    }
    // negative money is a cost
    // positive money is a profit
    virtual public float GetMoney()
    {
        return 0;
    }
    virtual public float GetUnlockEpoch()
    {
        return unlockEpoch;
    }
    public void AddBuilding(GameObject tile)
    {
        float x = tile.transform.position.x;
        float y = tile.transform.position.y;
        float z = tile.transform.position.z;
        Vector3 normal = new Vector3(x, y, z);
        normal.Normalize();
        Quaternion r = Quaternion.LookRotation(normal, new Vector3(0.0f, 0.0f, 1.0f));
        
        GameObject o = GameObject.Instantiate(Get3dObject(), new Vector3(x, y, z) + normal * 0.015f, r); // Quaternion.Euler(new Vector3(nx,ny,nz)));
        o.SetActive(true);
        o.transform.localScale *= 0.015f;
        o.transform.transform.SetParent(tile.transform, true);
    }
    public abstract GameObject Get3dObject();
}

public class BuildingA : Building
{
    public BuildingA()
    {
        c02Pollution = 0.0f;
        nuclearWaste = 0.0f;
        power = 0.0f;
        wellness = 0.0f;
        price = 0.0f;
        unlockEpoch = 0.0f;
        tittle = "";
        description = "";
    }

    public BuildingA(Building b) : base(b)
    {
    }

    public override GameObject Get3dObject()
    {
        return WorldManager.Get().houseA;
    }
}

public class NuclearA : Building
{
    public NuclearA()
    {
        c02Pollution = 0.01f;
        nuclearWaste = 0.0f;
        power = 60.0f;
        wellness = 0.0f;
        price = 272.0f;
        unlockEpoch = 0.0f;
        tittle = "Power Plant A";
        description = "Obninsk was the world's first commercial nuclear power plant.\nIt was secretly built in the 1950s during the Cold War and for 4 years it was the only nuclear power plant that supplied power to the USSR.";
        hexTileRequirements = new HexTileResources[] { HexTileResources.LAND };
        space = 2;
    }

    public override GameObject Get3dObject()
    {
        return WorldManager.Get().nuclearA;
    }
}
public class NuclearB : Building
{
    public NuclearB()
    {
        c02Pollution = 0.01f;
        nuclearWaste = 0.0f;
        power = 70.0f;
        wellness = 0.0f;
        price = 250.0f;
        unlockEpoch = 0.0f;
        tittle = "Nuclear Power B";
        description = "Built in 1976, Laguna Verde is the only nuclear power generation plant in Mexico. It is located on the coast of the Gulf of Mexico, and uses sea water to cool itself.";
        space = 2;
    }
    public override GameObject Get3dObject()
    {
        return WorldManager.Get().nuclearB;
    }
}
public class NuclearC : Building
{
    public NuclearC()
    {
        c02Pollution = 0.01f;
        nuclearWaste = 0.0f;
        power = 80.0f;
        wellness = 0.0f;
        price = 200.0f;
        unlockEpoch = 0.0f;
        tittle = "Nuclear Power C";
        description = "'Kashiwazaki-Kariwa 6' is the first of the third-generation reactors with a ABWR system. It was build in 1993.";
        space = 2;
    }
    public override GameObject Get3dObject()
    {
        return WorldManager.Get().nuclearC;
    }
}


public class RenewA : Building
{
    public RenewA()
    {
        c02Pollution = 0.13f;
        nuclearWaste = 0.0f;
        power = 7.0f;
        wellness = 20.0f;
        price = 23.0f;
        unlockEpoch = 0.0f;
        tittle = "Hydroelectric";
        description = "On September 30, 1882, the world's first hydroelectric power plant began operation on the Fox River in Appleton, Wisconsin.";
        space = 3;
    }

    public override GameObject Get3dObject()
    {
        return WorldManager.Get().renewA;
    }
}
public class RenewB : Building
{
    public RenewB()
    {
        c02Pollution = 0.01f;
        nuclearWaste = 0.0f;
        power = 10.0f;
        wellness = 0.0f;
        price = 60.0f;
        unlockEpoch = 0.0f;
        tittle = "Wind energy";
        description = "It was the year 1981 when, as a result of the collaboration between NASA and the United States Department of Energy, the first wind farm in the world was launched: Towards 2000.";
        space = 3;
    }
    public override GameObject Get3dObject()
    {
        return WorldManager.Get().renewB;
    }
}
public class RenewC : Building
{
    public RenewC()
    {
        c02Pollution = 0.01f;
        nuclearWaste = 0.0f;
        power = 13.0f;
        wellness = 0.0f;
        price = 50.0f;
        unlockEpoch = 0.0f;
        tittle = "Wind energy";
        description = "The industrial conglomerate Grupo México is investing 250 million dollars in the construction of a wind farm, and a 60 kilometer electrical transmission line, in Nuevo León. Operation will begin in 2021.";
        space = 3;

    }
    public override GameObject Get3dObject()
    {
        return WorldManager.Get().renewC;
    }
}


public class CoalA : Building
{
    public CoalA()
    {
        c02Pollution = 1.39f;
        nuclearWaste = 0.0f;
        power = 6.0f;
        wellness = 0.0f;
        price = 50.0f;
        unlockEpoch = 0.0f;
        tittle = "Coal";
        description = "'Pearl power street station' was the first comercial central power plant in the U.S.It started generating energy in 1882.";
        space = 1;

    }

    public override GameObject Get3dObject()
    {
        return WorldManager.Get().coalA;
    }
}
public class CoalB : Building
{
    public CoalB()
    {
        c02Pollution = 0.57f;
        nuclearWaste = 0.0f;
        power = 7.0f;
        wellness = 0.0f;
        price = 41.0f;
        unlockEpoch = 0.0f;
        tittle = "Oil";
        description = "This power plant is located in the state of Baja California Sur, Mexico. Its first unit was built in 1979.";
        space = 1;

    }
    public override GameObject Get3dObject()
    {
        return WorldManager.Get().coalB;
    }
}
public class CoalC : Building
{
    public CoalC()
    {
        c02Pollution = 1.33f;
        nuclearWaste = 0.0f;
        power = 8.0f;
        wellness = 0.0f;
        price = 28.0f;
        unlockEpoch = 0.0f;
        tittle = "Gas";
        description = "'Merida 4' plant in the peninsula of Yucatan, Mexico, will begin operation in 2021.";
        space = 1;
    }
    public override GameObject Get3dObject()
    {
        return WorldManager.Get().coalC;
    }
}