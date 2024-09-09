using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Character self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Char") && other.GetComponent<Character>() != self)
        {
            other.GetComponent<Character>().OnDeath();
            Destroy(gameObject);
        }
    }
}
