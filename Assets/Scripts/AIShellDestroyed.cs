using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShellDestroyed : MonoBehaviour
{
    public GameObject explotion;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag != null)
        {
            GameObject exp = Instantiate(explotion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
