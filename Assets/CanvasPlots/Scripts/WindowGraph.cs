using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    //private RectTransform labelTemplateX;
    //private RectTransform labelTemplateY;
    //private RectTransform dashTemplateX;
    //private RectTransform dashTemplateY;
    private List<GameObject> gameObjectList;

    private int pressSpaceCount = 2;
    public float[] valueArray;

    private void Awake()
    {
        pressSpaceCount = 2;
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        //labelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        //labelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();
        //dashTemplateX = graphContainer.Find("DashTemplateY").GetComponent<RectTransform>();
        //dashTemplateY = graphContainer.Find("DashTemplateX").GetComponent<RectTransform>();

        gameObjectList = new List<GameObject>();

        valueArray = new float[] {0, 5, 98, 32 , 65, 0, 15, 45, 27, 100, 23, 34, 16, 88, 23, 23};
        //List<int> valueList = new List<int>() {5, 98, 32 , 65, 0, 15, 45, 27, 100};
        //List<float> valueList = new List<float>() {5, 98, 32 , 65, 0, 15, 45, 27, 100};
        //ShowGraph(valueArray, 12);
        //valueArray[0] = 30;
        pressSpaceCount = 7;
        // ShowGraph(valueArray, pressSpaceCount,0,1);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("WedgeCircle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11f, 11f);
        rectTransform.anchorMin = new Vector2(0f, 0f);
        rectTransform.anchorMax = new Vector2(0f, 0f);

        return gameObject;
    }

    public void ShowGraph(float[] valueArray, int epoch, float yMinimum, float yMaximum)
    {
        Debug.Log(valueArray);
        List<float> valueList = new List<float>();
        for (int i = 1; i <= epoch; ++i)
        {
            valueList.Add(valueArray[i]);
        }
        //ShowGraph(valueList);
        ShowGraph(
            valueList,
            yMinimum,
            yMaximum,
            (int _i) => (1880 + _i*10).ToString(), 
            (float _f) => Mathf.RoundToInt(_f).ToString()
            );
    }

    private void ShowGraph(
        List<float> valueList,
        float yMinimum,
        float yMaximum,
        Func<int, string> getAxisLabelX = null, 
        Func<float, string> getAxisLabelY = null
        )
    {
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
           getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString();};

        }

        foreach(GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        // Use 5% for each margin (top, bottom)
        float graphWidth = graphContainer.sizeDelta.x;
        float graphHeight = graphContainer.sizeDelta.y;

        //int maxVisibleValueAmount = 5;
        //float yMaximum = valueList[0];
        //float yMinimum = valueList[0];
        //foreach(float value in valueList)
        //{
        //    if (value > yMaximum)
        //    {
        //        yMaximum = value;
        //    }
        //    if (value < yMinimum)
        //    {
        //        yMinimum = value;
        //    }
        //}
        //yMaximum = yMaximum + (yMaximum - yMinimum) * 0.2f;
        //yMinimum = yMinimum - (yMaximum - yMinimum) * 0.2f;
//        graphHeight
  //          graphWidth
        GameObject dotConnectionGameObject2 = CreateDotConnection(new Vector2(0, graphHeight*0.1f), new Vector2(graphWidth, graphHeight * 0.1f), new Color(1f, 1, 0.5f, 0.5f));
        gameObjectList.Add(dotConnectionGameObject2);

        //float xSize = 50f;
        float xSize = graphWidth / (valueList.Count + 1);

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; ++i)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] -yMinimum ) / (yMaximum - yMinimum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            gameObjectList.Add(circleGameObject);
            if (lastCircleGameObject != null)
            {
                GameObject dotConnectionGameObject = CreateDotConnection(
                    lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, 
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition, new Color(0.2f, 1, 0.5f, 0.5f));
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastCircleGameObject = circleGameObject;

            //RectTransform labelX = Instantiate(labelTemplateX);
            //labelX.SetParent(graphContainer);
            //labelX.gameObject.SetActive(true);
            //labelX.gameObject.SetActive(true);
            //labelX.anchoredPosition = new Vector2(xPosition, -7f);
            //labelX.GetComponent<Text>().text = getAxisLabelX(i);
            //gameObjectList.Add(labelX.gameObject);

            //RectTransform dashX = Instantiate(dashTemplateX);
            //dashX.SetParent(graphContainer);
            //dashX.gameObject.SetActive(true);
            //dashX.gameObject.SetActive(true);
            //dashX.anchoredPosition = new Vector2(xPosition, -7f);
            //gameObjectList.Add(dashX.gameObject);
        }
        //int separatorCount = 10;
        //for (int i = 0; i <= separatorCount; ++i)
        //{
        //    RectTransform labelY = Instantiate(labelTemplateY);
        //    labelY.SetParent(graphContainer);
        //    labelY.gameObject.SetActive(true);
        //    labelY.gameObject.SetActive(true);
        //    float normalizedValue = (float)i / separatorCount;
        //    labelY.anchoredPosition = new Vector2(-30, normalizedValue * graphHeight);
        //    labelY.GetComponent<Text>().text = getAxisLabelY(yMinimum + normalizedValue * (yMaximum - yMinimum));
        //    gameObjectList.Add(labelY.gameObject);

        //    RectTransform dashY = Instantiate(dashTemplateY);
        //    dashY.SetParent(graphContainer);
        //    dashY.gameObject.SetActive(true);
        //    dashY.gameObject.SetActive(true);
        //    dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
        //    gameObjectList.Add(dashY.gameObject);
        //}
    }
    private float getAngleFromVectorFloat(Vector2 vec)
    {
        // Returns an angle in the range [0, 360)
        // General formula is:
        // theta = arctan (y / x)
        // But there will be edge cases
        float x = vec.x;
        float y = vec.y;

        if (y == 0) {
            return x >= 0 ? 0 : 180;
        } else if (x == 0) {
            return y >= 0 ? 90 : 270;
        }

        float radians = (float)Math.Atan2(y, x);
        float angle = radians * (180 / (float)Math.PI);
        return angle;
    }
    // Color s_color = new Color(0.2f, 1, 0.5f, 0.5f);
    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color)
    {
        // Color color = in_c!=null? : new Color(1f, 1, 1f, 0.5f);
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0f, 0f);
        rectTransform.anchorMax = new Vector2(0f, 0f);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, getAngleFromVectorFloat(dir));

        return gameObject;
    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pressSpaceCount++;
            ShowGraph(valueArray, pressSpaceCount,0,1);
        }
        if (Input.GetKeyDown("r"))
        {
            pressSpaceCount = 2;
            ShowGraph(valueArray, pressSpaceCount,0,1);
        }
    }
}
