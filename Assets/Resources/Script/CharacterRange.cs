using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    public List<Character> charsInCircle = new List<Character>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RemoveNullTarget()
    {
        for(int i = 0; i < charsInCircle.Count; i++)
        {
            if (charsInCircle[i] == null || !charsInCircle[i].CompareTag("Char"))
            {
                charsInCircle.Remove(charsInCircle[i]);
            }
        }
    }

    public Transform GetNearestTargeet() 
    {
        float distanceMin = float.MaxValue;
        int index = 0;
        for(int i = 0; i < charsInCircle.Count; i++)
        {
            float distance = (transform.position - charsInCircle[i].transform.position).magnitude;
            if(distanceMin > distance)
            {
                distanceMin = distance;
                index = i;
            }
        }
        return charsInCircle[index].transform;
    }
}
