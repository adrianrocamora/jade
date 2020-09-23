using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatisticsController : MonoBehaviour
{
    public Text tTitle;
    public Text tStatistics;
    string other;
    public void UpdatePanel(float epochMoney)
    {
        tStatistics.text = $"<b>Money:</b>\t\t\t{epochMoney} $\n" + other;
    }
    public void UpdatePanel(float year, float epochC02, float epochNuclear, float epochPower, float epochWellness, float epochMoney, int pop, float powerDesire)//string name, string description)
    {
        tTitle.text = $"World Statistics\t\t{year}";


        other = $"<b>Pollution:</b>\t\t{epochC02} CO2\n<b>Power:</b>\t\t\t\t{epochPower} W\n<b>Wellness:</b>\t\t{epochWellness} %\n"; //  + "";
        other += $"<b>Population:</b>\t\t{pop}\n";
        other += $"<b>Power Desire:</b>\t\t{powerDesire}\n";

        tStatistics.text = $"<b>Money:</b>\t\t\t{epochMoney} $\n" + other;
    }
}
