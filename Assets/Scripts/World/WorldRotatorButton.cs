using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotatorEnum
{
    Left,
    Rigth,
    Up,
    Down
}

public class WorldRotatorButton : MonoBehaviour
{
    public WorldRotator worldRotator;
    public RotatorEnum rotatorEnum;
    public Material selectedMat;
    Material ownMat;
    private void Start()
    {
        ownMat = GetComponent<Renderer>().material;
        // selectedMat = WorldManager.Get().GetComponent<TileManager>().selectMat;
    }
    public void Rotate()
    {
        GetComponent<Renderer>().material = selectedMat;
        switch (rotatorEnum)
        {
            case RotatorEnum.Left:
                worldRotator.RotateXUp();
                break;
            case RotatorEnum.Rigth:
                worldRotator.RotateXDown();
                break;

            case RotatorEnum.Down:
                worldRotator.RotateYUp();
                break;
            case RotatorEnum.Up:
                worldRotator.RotateYDown();
                break;
        }
    }
    public void TriggerExit()
    {
        GetComponent<Renderer>().material = ownMat;

    }
}
