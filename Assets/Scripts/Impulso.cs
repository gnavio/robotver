using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulso : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    [SerializeField] Transform orientation;
    
    public int cartuchos = 3;
    public float dash = 20f;

    [Header("Tecla")]
    [SerializeField] KeyCode DashKey = KeyCode.Mouse1;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(DashKey) && cartuchos >= 1) 
        {
            m_Rigidbody.AddForce(orientation.forward * -1 * dash);
            cartuchos -= 1;
        }
    }

    public void SumarCartucho(int x)
    {
        cartuchos += x;
    }
}
