using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    public float speed = 5f;
    public float xMin = -39f;
    public float xMax = 41.74007f;
    private float rayDistance = 25f;
    //Move car horizontal axis
    public float zMin = -49.97f;
    public float zMax = 49.97f;
    private float rayDistance2 = 15f;

    public LayerMask carObstacle;

    public float frontOffset = 5f; // Khoảng cách từ tâm đến mũi xe

    private bool isStopped = false;

    // Enum để xác định loại xe
    public enum CarType { CarOne, CarTwo, CarThree }
    public CarType carType; // Biến công khai để chọn loại xe

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position; //Lưu vị trí ban đầu của xe
    }

    // Update được gọi mỗi khung hình
    void Update()
    {
        // Gọi hàm tương ứng dựa trên loại xe
        if (carType == CarType.CarOne)
        {
            CarOne();
        }
        else if (carType == CarType.CarTwo)
        {

            CarTwo();
        }
        else if (carType == CarType.CarThree)
        {
            CarThree();
        }
    }

    public void CarOne()
    {
        //Debug.Log(isStopped + " Big Rick");
        isStopped = CheckObstacle1();
        if (!isStopped)
        {
            if (!CheckObstacle1())
            {
                // Di chuyển xe theo hướng dương của trục x
                Vector3 move = new Vector3(1f, 0, 0) * speed * Time.deltaTime;
                transform.position += move;
            }

            if (transform.position.x >= xMax)
            {
                transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
            }
        }
    }

    public void CarTwo()
    {
        isStopped = CheckObstacle2();
        //Debug.Log(isStopped + " City Bus");
        if (!isStopped)
        {
            if (!CheckObstacle2())
            {
                // Thay đổi trực tiếp vị trí của transform để di chuyển
                Vector3 move = new Vector3(-1f, 0, 0) * speed * Time.deltaTime;
                transform.position += move;
            }

            if (transform.position.x <= xMin)
            {
                transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
            }
        }
    }

    public void CarThree()
    {
        //Debug.Log(isStopped + " police");
        isStopped = CheckObstacle3();
        //Debug.Log("aa" + CheckObstacle());
        if (!isStopped)
        {
            if (!CheckObstacle3())
            {
                // Thay đổi trực tiếp vị trí của transform để di chuyển
                Vector3 move = new Vector3(0, 0, -1f) * speed * Time.deltaTime;
                transform.position += move;
            }

            if (transform.position.z <= zMin)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
            }
        }
    }

    bool CheckObstacle1()
    {
        Vector3 frontPosition = transform.position + transform.forward * frontOffset;
        frontPosition.y += 0.8f;

        Debug.DrawLine(frontPosition, frontPosition + transform.forward * rayDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(frontPosition, transform.forward, out hit, rayDistance, carObstacle))
        {
            isStopped = true;
            return true;
        }

        isStopped = false;
        return false;
    }

    bool CheckObstacle2()
    {
        Vector3 frontPosition = transform.position + transform.forward * frontOffset;
        frontPosition.y += 0.8f;

        Debug.DrawLine(frontPosition, frontPosition + transform.forward * rayDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(frontPosition, transform.forward, out hit, rayDistance, carObstacle))
        {
            isStopped = true;
            return true;
        }
        isStopped = false;
        return false;
    }

    bool CheckObstacle3()
    {
        // Tạo một vị trí mới ở phía trước của xe dựa vào frontOffset
        Vector3 frontPosition = transform.position + transform.forward * frontOffset;
        frontPosition.y += 1.5f;

        // Vẽ đường ray để debug
        Debug.DrawLine(frontPosition, frontPosition + transform.forward * rayDistance2, Color.red);

        // Bắn raycast từ vị trí phía trước của xe
        RaycastHit hit;
        if (Physics.Raycast(frontPosition, transform.forward, out hit, rayDistance2, carObstacle))
        {
            isStopped = true;
            return true;
        }

        RaycastHit hitRight;
        Vector3 rightDirection = Quaternion.Euler(0, 30, 0) * transform.forward; // Quay 90 độ sang phải
        Debug.DrawLine(frontPosition, frontPosition + rightDirection * rayDistance2, Color.red);
        if (Physics.Raycast(frontPosition, rightDirection, out hitRight, rayDistance2, carObstacle))
        {
            isStopped = true;
            return true;
        }

        isStopped = false;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Char"))
        {
            Character charater = other.GetComponent<Character>();
            if (charater != null && !charater.isDeath)
            {
                charater.OnDeath();
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Char"))
    //    {
    //        Character charater = collision.collider.GetComponent<Character>();
    //        if (charater != null && !charater.isDeath)
    //        {
    //            charater.OnDeath();
    //        }
    //    }
    //}

    public void ResetPosition()
    {
        transform.position = initialPosition;
        isStopped = false;
    }
}
