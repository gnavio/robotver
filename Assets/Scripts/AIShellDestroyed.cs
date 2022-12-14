using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShellDestroyed : MonoBehaviour
{
    public GameObject explotion;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag != null && col.gameObject.tag != "Enemy")
        {
            GameObject exp = Instantiate(explotion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            GameObject exp = Instantiate(explotion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
            gameManager.GameOver();
        }
    }
}
