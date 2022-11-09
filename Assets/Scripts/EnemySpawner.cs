using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject EnemyPrefab;

        public void SpawnNewEnemy()
    {
        Bounds bounds = GetComponent<Collider>().bounds;
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        GameObject newEnemy = GameObject.Instantiate(EnemyPrefab);
        newEnemy.transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
    }
}
