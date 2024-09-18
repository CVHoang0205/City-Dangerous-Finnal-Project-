using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Character self;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f + 0.05f + self.level);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Char") && other.GetComponent<Character>() != self)
        {
            other.GetComponent<Character>().OnDeath();
            self.GainLevel();   
            Destroy(gameObject);
        }
    }
}
