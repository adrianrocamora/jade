using System;

public class AssignRandomTerrain 
{
    public static void shuffle(int[] card,
                               int n)
    {
        System.Random rand = new System.Random();

        for (int i = 0; i < n; i++)
        {

            // Random for remaining positions. 
            int r = i + rand.Next(n - i);

            //swapping the elements 
            int temp = card[r];
            card[r] = card[i];
            card[i] = temp;

        }
    }

    // Start is called before the first frame update
    public int[] GetTerrainArray()
    {
        //
        int maxTiles = 202;
        //int maxTiles = 160;
        // Terrain    %    tiles      id
        // =============================
        // water    70%      112       0
        // forest   11%       18       1
        // land     10%       16       2
        // desert    9%       14       3
        // -----------------------------
        // TOTAL   100%      160

        int[] terrainArray = new int[maxTiles];
        //List<int> terrainArray = new List<int>();

        int numWater = (int)Math.Round(70 * (double)maxTiles / 100);
        int numForest = (int)Math.Round(11 * (double)maxTiles / 100);
        int numLand = (int)Math.Round(10 * (double)maxTiles / 100);
        int numDesert = (int)Math.Round(9 * (double)maxTiles / 100);
        UnityEngine.Debug.Log(numForest +" "+ numLand+" "+ numWater+" "+ numDesert);
        while (numWater + numForest + numLand + numDesert < maxTiles)
        {
            numWater += 1;
        }
        /*
        Debug.Log("numWater: " + numWater);
        Debug.Log("numForest: " + numForest);
        Debug.Log("numLand: " + numLand);
        Debug.Log("numDesert: " + numDesert);
        */
        for (int i = 0; i < maxTiles; ++i)
        {
            if (i < numWater)
            {
                terrainArray[i] = 0;
            }
            else if (i < numWater + numForest)
            {
                terrainArray[i] = 1;
            }
            else if (i < numWater + numForest + numLand)
            {
                terrainArray[i] = 2;
            }
            else
            {
                terrainArray[i] = 3;
            }
        }
        shuffle(terrainArray, terrainArray.Length);
        return terrainArray;
        /*
        Debug.Log("CONSOLE LOG: ");
        string[] terrainStrings = new string[maxTiles];
        for (int i = 0; i < terrainArray.Length; ++i)
        {
            terrainStrings[i] = terrainArray[i].ToString();
        }
        string terrainStr = string.Concat(terrainStrings);

        Debug.Log(terrainStr);
        */
    }


}