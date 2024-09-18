using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow> 
{
    public Transform TF;
    public Transform playerTF;
    public Camera gameCamera;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offsetMainMenu;
    [SerializeField] Vector3 offsetShop;

    [SerializeField] Quaternion rotation;
    [SerializeField] Quaternion rotationMainMenu;

    private Vector3 currentOffset;
    private Quaternion currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = GetComponent<Camera>();
        ChangeState(1);
    }

    private void LateUpdate()
    {
        TF.position = Vector3.Lerp(TF.position, playerTF.position + currentOffset, Time.deltaTime * 5f);
        TF.rotation = Quaternion.Lerp(TF.rotation, currentRotation, Time.deltaTime * 5f);
    }

    public void ChangeState(int state)
    {
        rotationMainMenu = Quaternion.Euler(10f, 180f, 0f);
        rotation = Quaternion.Euler(40f, 0f, 0f);
        if (state == 1)
        {
            currentOffset = offsetMainMenu;
            currentRotation = rotationMainMenu;
        }
        else if (state == 2)
        {
            currentOffset = offset;
            currentRotation = rotation;
        }
        else if (state == 3)
        {
            currentOffset = offsetShop;
            currentRotation = rotationMainMenu;
        }

        Debug.Log("State Changed: " + state);
        Debug.Log("Offset: " + currentOffset);
        Debug.Log("Rotation: " + currentRotation);
    }
}
