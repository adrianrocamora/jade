
using System;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    GameObject[] tiles;
    int nTiles = 202;
    int numberHouses = 0;
    // materials

    public WindowGraph windowGraph;
    public WindowGraph windowGraphPower;
    public WindowGraph windowGraphCO2;
    public PloterController plot;
    public DisasterController disasters;
    public BuildManager buildManager;
    public GameOverController gameOverController;

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

    float currentMoney = 0;
    int epoch;
    float[] historyMoney = new float[20];
    float[] historyNuclear = new float[20];
    float[] historyC02 = new float[20];
    float[] historyPower = new float[20]; 
    float[] historyC02Plot = new float[20];
    float[] historyPowerPlot = new float[20];
    float[] historyWellness = new float[20];
    int[] historyPopulation = new int[20];

    public static WorldManager mWorldManager;
    public static WorldManager Get() { return mWorldManager; }

    public GameStatisticsController gameStatistics;
    // Start is called before the first frame update
    bool inGame = true;
    void Start()
    {
        tiles = new GameObject[nTiles];
        AssignRandomTerrain randomTerrain = new AssignRandomTerrain();
        int[] terrain = randomTerrain.GetTerrainArray();
        mWorldManager = this;
        for (int i = 0; i < nTiles; i++)
        {
            string search = "Planet." + toNumStr(i);
            // Debug.Log("finding " + search);
            tiles[i] = transform.Find(search).gameObject;
            BoxCollider b = tiles[i].AddComponent<BoxCollider>();
            Rigidbody r = tiles[i].AddComponent<Rigidbody>();

            // Material m = tiles[i].AddComponent<Material>();
            // tiles[i].GetComponent<Renderer>().material = waterMat;
            AddHexTile(tiles[i],i, terrain);
            r.useGravity = false;
            b.size = new Vector3(6, 6, 6);
            b.isTrigger = true;
        }
        Debug.Log("Init Plannet sucess ");
        epoch = 0;

        /*
        AddHouse();
        AddHouse();
        AddHouse();
        // AddHouse();
        /*
        historyWellness[epoch]  = 0;
        historyPower[epoch] = 0;
        historyNuclear[epoch] = 0;
        historyC02[epoch] = 0;
        historyMoney[epoch] = 0;
        historyPopulation[epoch] = GetPopulation();
        */
        InvokeRepeating("UpdateEveryMinute", 0, 60);
        // InvokeRepeating("UpdateEveryMinute", 0, 60);
    }

    public void AddHouse()
    {
        // adds a house in a random free tile
        TileManager tileManager = TileManager.Get();

        HexTile tile= tileManager.GetNextFreeTile();
        tile.AddBuilding(Building.GetHouse());
        // Debug.Log("ASdding house");
        numberHouses++;
    }

    public void Pay(float price)
    {
        currentMoney = currentMoney - price;
       // float[] historyC02 = new float[20];
        //float[] historyNuclear = new float[20];
        //float[] historyPower = new float[20];
        int pop = GetPopulation();
        gameStatistics.UpdatePanel(currentMoney);
        // gameStatistics.UpdatePanel(1900 + epoch * 10, historyC02[epoch], historyNuclear[epoch], historyPower[epoch], 0, currentMoney, pop, GetPowerDesire());

        Debug.Log("Pay " + currentMoney);
    }
    void AddHexTile(GameObject tile, int i, int[] terrain)
    {
        TileManager tileManager = TileManager.Get();

        int tileType = terrain[i];
        switch (tileType)
        {
            case 0:
                WaterHexTile.SetTile(tile);
                break;
            case 1:
                ForestHexTile.SetTile(tile);
                tileManager.AddForestTile(tile);
                break;
            case 2:
                    LandHexTile.SetTile(tile);
                    tileManager.AddLandTile(tile);
                break;
                case 3:
                    DesertHexTile.SetTile(tile);
                break;
        }
        /*
        tile.GetComponent<HexTile>().AddBuilding(new NuclearA(),0);
        tile.GetComponent<HexTile>().AddBuilding(new NuclearA(),1);
        tile.GetComponent<HexTile>().AddBuilding(new NuclearA(),2);
    */
        }
    private string toNumStr(int i)
    {
        if (i < 10)
            return "00" + i;
        else if (i < 100)
            return "0" + i;
        return "" + i;

    }

    // Update is called once per frame
    void UpdateEveryMinute()
    {
        if (!inGame)
            return;
        float epochC02 = 0.0f;
        float epochNuclear = 0.0f;
        float epochPower = 0.0f;
        float epochWellness = 0.0f;
        float epochMoney = 0.0f;

        for (int i = 0; i < nTiles; i++)
        {

            HexTile hexTile = tiles[i].GetComponent<HexTile>();
            epochC02 += hexTile.GetC02Polution();
            epochNuclear += hexTile.GetNuclearWaste();
            epochPower += hexTile.GetPower();
            epochWellness += hexTile.GetWellness();
            epochMoney += hexTile.GetMoney();
        }
       
        // update number of houses
        int population = GetPopulation();
        int popSpace = numberHouses * 500;
        while (popSpace < population)
        {
            AddHouse();
            popSpace = numberHouses * 500;
        }
        // update money
        // currentMoney += population * 1;
        // Debug.Log(currentMoney);
        
        epoch++;
        currentMoney -= epochMoney;
        


        
        // Update world 
        UpdateC02(epochC02);
        UpdateNuclear(epochNuclear);
        UpdatePower(epochPower);
        UpdateWellness(epochWellness);
        gameStatistics.UpdatePanel(1900 + epoch*10, epochC02, epochNuclear, epochPower, historyWellness[epoch], currentMoney, population, GetPowerDesire());
        //UpdateMoney();
        /*
        
         */
        // Update plots
        /*
         plot.UpdateEpoch(epoch);
         plot.UpdateC02(historyC02);
         plot.UpdateNuclear(historyNuclear);
         plot.UpdatePower(historyPower);
         plot.UpdateWellness(historyWellness);

         // Update Natural Disasters probability
         disasters.UpdateEpoch(epoch);
         disasters.UpdateC02(historyC02);
         disasters.UpdateNuclear(historyNuclear);
         disasters.UpdatePower(historyPower);
         disasters.UpdateWellness(historyWellness);
         */
        buildManager.UpdateEpoch(epoch);

        if(epoch > 14)
        {
            gameOverController.YouWon();
            inGame = false;
        }
        string s = "";
        Debug.Log("len " + historyWellness.Length);
        /*
        for (int i = 0; i < historyWellness.Length; i++)
        {
            s += historyPower[i]+" , " ;
        }
        */
        Debug.Log(s);
        /*
        if(epoch>1)
        {
            windowGraph.ShowGraph(historyWellness, epoch,0,100);
            windowGraphPower.ShowGraph(historyPowerPlot, epoch,0,100);
            windowGraphCO2.ShowGraph(historyC02Plot, epoch, 0, 100);
        }*/
        
    }
    int GetPopulation()
    {                     //  1870,    1900, 2,3,
        int[] population = new int[16] {
            1440, 1501, 1570, 1650, 1800,
            1950, 2100, 2400, 2700, 3000,
            3650, 4400, 5250, 6000, 7000, 7800 };//[990,  1650];
        return population[epoch];
    }
    private void UpdateMoney()
    {
        // throw new NotImplementedException();
        // float wellness = historyWellness[epoch];
        float prevMoney = historyMoney[epoch-1];
        float currentMoney = GetPopulation() + prevMoney;
        historyMoney[epoch] = currentMoney;
    }

    // wellnes [0-100]
    // if bellow 30 people get unhappy and die
    private void UpdateWellness(float epochWellness)
    {
        // wellness calculation is abit more complicated
        historyPopulation[epoch] = GetPopulation();
        // float C02Desire = 800; // less than, for the world
        float NuclearDesire = 80; // less than, for the world
        float powerDesire = GetPowerDesire();

        // float powerWellness = Math.Max(0, 100 * ( (historyPower[epoch] - powerDesire*0.1f)/ powerDesire));
        float b = 27; // historyPower[epoch]
        float x = Mathf.Max(0f, powerDesire - historyPower[epoch]);
        float powerWellness = Mathf.Min(1f, (b / (x + 0.8f * b)) - 0.2466f);

        x = historyC02[epoch];
        float C02Wellness = 1;
        if (x > 0)
            C02Wellness = Mathf.Min(1f, -(x * x) / (280 * 280) + 1);
        // float C02Wellness = 30 * Mathf.Clamp((historyC02[epoch] - C02Desire)/ C02Desire,0, 9999);
        // float NuclearWellness = 30 * Mathf.Clamp((historyNuclear[epoch] - NuclearDesire) / NuclearDesire, 0, 9999);
        // historyWellness[epoch] = historyWellness[epoch-1] + epochWellness + powerWellness +C02Wellness + NuclearWellness;
        // historyWellness[epoch] = powerWellness*100;
        if (powerWellness < 0.1 || C02Wellness < 0.1)
        {
            inGame = false;
            gameOverController.YouLost();
            Debug.Log("powerWellness = " + powerWellness);
            Debug.Log("C02Wellness = " + C02Wellness);
        }

        historyWellness[epoch] = C02Wellness*50 + powerWellness * 50;
        historyPowerPlot[epoch] = powerWellness*100;
        historyC02Plot[epoch] = C02Wellness * 100;
        if (epoch > 1)
        {
            windowGraph.ShowGraph(historyWellness, epoch, 0, 100);
            windowGraphPower.ShowGraph(historyPowerPlot, epoch, 0, 100);
            windowGraphCO2.ShowGraph(historyC02Plot, epoch, 0, 100);
            /*
            string s="";
            for (int i = 0; i < historyC02Plot.Length; i++)
            {
                s += historyC02Plot[i] + " , ";
            }
            Debug.Log("C02Wellness = " + s);
            */
        }
    }

    private float GetPowerDesire()
    {
                            // 0,  1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,  13,14 
        float [] powerDesire = new float[16] { 0.1f,0.1f,.2f,.3f,.4f,.5f,.6f,.7f,.8f,.9f,1.1f,1.9f,2.5f, 3.1f, 3f , 2.9f}; // kW per human

        int pop = GetPopulation();

        return pop * powerDesire[epoch] * 0.1f;
    }
    /*
    // each house occupies one space.
    // each house can hold upto 500 people.

    // start with 3 houses.

    */

    private void UpdatePower(float epochPower)
    {
        historyPower[epoch] = epochPower;
    }

    private void UpdateNuclear(float epochNuclear)
    {
        historyNuclear[epoch] =  epochNuclear;
    }

    private void UpdateC02(float epochC02)
    {
        historyC02[epoch] =  epochC02;
    }
}
