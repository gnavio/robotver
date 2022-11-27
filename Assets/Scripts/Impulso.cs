using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulso : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    [SerializeField] Transform orientation;
    [SerializeField] Animator anim;
    [SerializeField] public GameObject OverlayPrefab;
    [SerializeField] public GameObject overlayPos;
    [SerializeField] AudioSource BlasterAudio;

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
        anim.SetBool("Impulso", false);

        if (Input.GetKeyDown(DashKey) && cartuchos >= 1) 
        {
            BlasterAudio.Play(0);
            m_Rigidbody.AddForce(orientation.forward * -1 * dash);
            cartuchos -= 1;
            anim.SetBool("Impulso", true);
            Debug.Log("ImpulsoTrue");
            StartCoroutine(ImpulsoOverlay());
        }
    }

    public void SumarCartucho(int x)
    {
        cartuchos += x;
    }

    IEnumerator ImpulsoOverlay()
    {
        GameObject overlay = Instantiate(OverlayPrefab, overlayPos.transform.position, overlayPos.transform.rotation);
        overlay.transform.parent = overlayPos.transform;
        yield return new WaitForSeconds(1);
        Destroy(overlay);
    }
}
