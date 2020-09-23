using UnityEngine;

public class WorldRotator : MonoBehaviour
{
    public GameObject target;

    public Vector3 correction;

    private void Start()
    {
        // correction = target.transform.position;
        correction = Vector3.zero;
    }

    public void RotateXUp()
    {
        transform.RotateAround(correction, Vector3.up, 100 * Time.deltaTime);
    }
    public void RotateXDown()
    {
        transform.RotateAround(correction, Vector3.down, 100 * Time.deltaTime);
    }
    public void RotateYUp()
    {
        transform.RotateAround(correction, Vector3.left, 100 * Time.deltaTime);
    }
    public void RotateYDown()
    {
        transform.RotateAround(correction, Vector3.right, 100 * Time.deltaTime);
    }


    void Update()
    {
        if (Input.GetKey("1"))
        {
            transform.RotateAround(correction, Vector3.up, 100 * Time.deltaTime);
        }
        if (Input.GetKey("2"))
        {
            transform.RotateAround(correction, Vector3.down, 100 * Time.deltaTime);
        }
        if (Input.GetKey("3"))
        {
            transform.RotateAround(correction, Vector3.left, 100 * Time.deltaTime);
        }
        if (Input.GetKey("4"))
        {
            transform.RotateAround(correction, Vector3.right, 100 * Time.deltaTime);
        }
     

        //transform.RotateAround(correction, Vector3.up, 100 * Time.deltaTime);
        //transform.RotateAround(correction, Vector3.left, 100 * Time.deltaTime);
        //transform.RotateAround(correction, Vector3.back, 100 * Time.deltaTime);

        //transform.Rotate(Vector3.up, 100 * Time.deltaTime);
        //transform.Rotate(Vector3.left, 100 * Time.deltaTime);
        //transform.Rotate(Vector3.back, 100 * Time.deltaTime);

    }
}