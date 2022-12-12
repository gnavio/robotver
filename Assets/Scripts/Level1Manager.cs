using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] GameObject Enemies;
    [SerializeField] TMPro.TMP_Text EnemyCount;
    int totalEnemies;
    [HideInInspector] public bool nivelCompletado = false;

    [Header("Missions")]
    [SerializeField] GameObject Mission1;
    // Start is called before the first frame update
    void Start()
    {
        Mission1.SetActive(true);
        totalEnemies = Enemies.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyKillCount();
    }

    void EnemyKillCount()
    {
        EnemyCount.text = totalEnemies - Enemies.transform.childCount + "/" + totalEnemies;

        if (Enemies.transform.childCount <= 0)
        {
            Debug.Log("Nivel Completado");
            Mission1.SetActive(false);
            nivelCompletado = true;
        }
    }
}
