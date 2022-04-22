using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSeparation : MonoBehaviour
{
    GameObject[] AI;
    public float spaceBetween = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        AI = GameObject.FindGameObjectsWithTag("Tank");    
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in AI)
        {
            if (go != gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, this.transform.position);
                if (distance <= spaceBetween)
                {
                    Vector3 direction = transform.position - go.transform.position;
                    transform.Translate(direction * Time.deltaTime);
                }
            }
        }
    }
}
