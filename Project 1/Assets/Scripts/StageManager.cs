using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    GameObject Enemy1;

    [SerializeField]
    HUDManager screen;

    [SerializeField]
    CollisionManager collisionManager;

    [SerializeField]
    BulletBankManager bulletBankManager;

    private float spawnTimer = 150;

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime * 60;

        if (spawnTimer < 0)
        {
            spawnTimer = 150;

            Vector3 spawnPos = Vector3.zero;
            Vector3 anchorPos = Vector3.zero;

            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            spawnPos.y = (height / 2) + 1;
            spawnPos.x = Random.Range(-width / 2, (width / 2) - (screen.hudWidth * width));

            anchorPos.y = (height / 2) - Random.Range(-(height / 5) * 2, -(height / 5));
            anchorPos.x = Random.Range(-width / 2, (width / 2) - (screen.hudWidth * width));

            GameObject enemy = Instantiate(Enemy1, spawnPos, Quaternion.identity, transform);
            enemy.GetComponent<EnemyType1>().swingPoint = anchorPos;
            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager
            collisionManager.AddEnemy(enemy);
        }
    }
}
