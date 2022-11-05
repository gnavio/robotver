using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShell : MonoBehaviour
{
    public GameObject explosion;
    public float speed = 10.0f;
    public int damage = 1;
    Rigidbody rb;

    void OnTriggerEnter(Collider col)
    {
        PlayerCharacter player = col.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.Hurt(damage);Destroy(this.gameObject);
        }
        
    }

    /*
    void OnOnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == player)
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.forward = rb.velocity;
    }
}
