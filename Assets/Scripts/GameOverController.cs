using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Text tTitle;
    public Text tStatistics;


    public void YouLost()
    {

        // tTitle.text = $"GAME OVER";
        gameObject.SetActive(true);
        tStatistics.text = "You Lost!!! ";

    }
    public void YouWon()
    {
        gameObject.SetActive(true);
        tStatistics.text = "You Won.";

    }
}
