using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow> 
{
    public Transform TF;
    public Transform playerTF;
    public Camera gameCamera;

    [SerializeField] Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        gameCamera = GetComponent<Camera>();

    }

    private void LateUpdate()
    {
        TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 5f);
    }
}
