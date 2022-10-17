using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    GameObject Enemy1;
    [SerializeField]
    GameObject Enemy2;
    [SerializeField]
    GameObject Enemy3;
    [SerializeField]
    GameObject Enemy4;
    [SerializeField]
    GameObject Enemy5;
    [SerializeField]
    GameObject Boss;

    [SerializeField]
    GameObject Explosion;

    [SerializeField]
    HUDManager screen;

    [SerializeField]
    GameObject player;

    [SerializeField]
    CollisionManager collisionManager;

    [SerializeField]
    BulletBankManager bulletBankManager;

    private float spawnTimer = 0;
    private float seconds = 0;

    private bool start = false;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            spawnTimer += Time.deltaTime * 60;
            if (spawnTimer > 60)
            {
                Camera cam = Camera.main;
                float height = 2f * cam.orthographicSize;
                float width = height * cam.aspect;
                seconds++;
                spawnTimer = 0;
                switch (seconds)
                {
                    case 2:
                        for (int i = 0; i<4; i++)
                        {
                            Vector3 spawnPos = Vector3.zero;
                            Vector3 anchorPos = Vector3.zero;

                            spawnPos.y = (height / 2) + 1 + (.7f * i);
                            spawnPos.x = -width / 2 + .5f;

                            anchorPos.y = (height / 2) - ((height / 5) * 2);
                            anchorPos.x = ((width / 2) - (screen.hudWidth * width) -.5f);

                            GameObject enemy = Instantiate(Enemy1, spawnPos, Quaternion.identity, transform);
                            enemy.GetComponent<EnemyType1>().swingPoint = anchorPos;
                            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
                            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
                            enemy.transform.position = spawnPos;
                            collisionManager.AddEnemy(enemy);
                        }
                        break;
                    case 12:
                        for (int i = 0; i < 4; i++)
                        {
                            Vector3 spawnPos = Vector3.zero;
                            Vector3 anchorPos = Vector3.zero;

                            spawnPos.y = (height / 2) + 1 + (.7f * i);
                            spawnPos.x = (width / 2) - (screen.hudWidth * width) - .5f;

                            anchorPos.y = (height / 2) - ((height / 5) * 2);
                            anchorPos.x = ((-width / 2) + .5f);

                            GameObject enemy = Instantiate(Enemy1, spawnPos, Quaternion.identity, transform);
                            enemy.GetComponent<EnemyType1>().swingPoint = anchorPos;
                            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
                            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
                            enemy.transform.position = spawnPos;
                            collisionManager.AddEnemy(enemy);
                        }
                        break;
                    case 22:
                        for (int i = 0; i < 2; i++)
                        {
                            Vector3 spawnPos = Vector3.zero;
                            Vector3 anchorPos = Vector3.zero;

                            spawnPos.y = (height / 2) + 1 + (.7f * i);
                            spawnPos.x = -width / 2 + .5f;

                            anchorPos.y = (height / 2) - ((height / 5) * 2);
                            anchorPos.x = (-width / 2) + .5f + (width/5 * 2);

                            GameObject enemy = Instantiate(Enemy1, spawnPos, Quaternion.identity, transform);
                            enemy.GetComponent<EnemyType1>().swingPoint = anchorPos;
                            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
                            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
                            enemy.transform.position = spawnPos;
                            collisionManager.AddEnemy(enemy);
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            Vector3 spawnPos = Vector3.zero;
                            Vector3 anchorPos = Vector3.zero;

                            spawnPos.y = (height / 2) + 1 + (.7f * i);
                            spawnPos.x = (width / 2) - (screen.hudWidth * width) - .5f;

                            anchorPos.y = (height / 2) - ((height / 5) * 2);
                            anchorPos.x = ((width / 2) - .5f - (width / 5 * 2) - (screen.hudWidth * width));

                            GameObject enemy = Instantiate(Enemy1, spawnPos, Quaternion.identity, transform);
                            enemy.GetComponent<EnemyType1>().swingPoint = anchorPos;
                            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
                            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
                            enemy.transform.position = spawnPos;
                            collisionManager.AddEnemy(enemy);
                        }
                        break;
                    case 36:
                        for (int i = 0; i < 2; i++)
                        {
                            Vector3 spawnPos = Vector3.zero;

                            spawnPos.y = (height / 2) + 1;
                            spawnPos.x = Random.Range(-width / 2, (width / 2));

                            GameObject enemy = Instantiate(Enemy2, spawnPos, Quaternion.identity, transform);
                            enemy.GetComponent<EnemyType2>().player = player;
                            enemy.GetComponent<EnemyType2>().screen = screen;
                            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
                            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
                            enemy.transform.position = spawnPos;
                            collisionManager.AddEnemy(enemy);
                        }
                        break;
                }
            }
        }

        /*
        if (spawnTimer == 150)
        {
            spawnTimer -= Time.deltaTime * 60;
            //spawnTimer = 150;

            /*
            Vector3 spawnPos = Vector3.zero;
            Vector3 anchorPos = Vector3.zero;

            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            spawnPos.y = (height / 2) + 1;
            spawnPos.x = Random.Range(-width / 2, (width / 2));

            anchorPos.y = (height / 2) - Random.Range((height / 5), (height / 5) * 2);
            anchorPos.x = Random.Range(-width / 2, (width / 2) - (screen.hudWidth * width));

            GameObject enemy = Instantiate(Enemy1, spawnPos, Quaternion.identity, transform);
            enemy.GetComponent<EnemyType1>().swingPoint = anchorPos;
            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
            enemy.transform.position = spawnPos;
            collisionManager.AddEnemy(enemy);
            */

            /*
            Vector3 spawnPos = Vector3.zero;

            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            spawnPos.y = (height / 2) + 1;
            spawnPos.x = Random.Range(-width / 2, (width / 2));

            GameObject enemy = Instantiate(Enemy2, spawnPos, Quaternion.identity, transform);
            enemy.GetComponent<EnemyType2>().player = player;
            enemy.GetComponent<EnemyType2>().screen = screen;
            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
            enemy.transform.position = spawnPos;
            collisionManager.AddEnemy(enemy);
            */

            /*
            Vector3 spawnPos = Vector3.zero;

            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            spawnPos.y = (height / 2) + 1;
            spawnPos.x = Random.Range(-width / 2, (width / 2));

            GameObject enemy = Instantiate(Enemy3, spawnPos, Quaternion.identity, transform);
            enemy.GetComponent<EnemyType3>().player = player;
            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
            enemy.transform.position = spawnPos;
            collisionManager.AddEnemy(enemy);
            */
            /*
            Vector3 spawnPos = Vector3.zero;

            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            spawnPos.y = (height / 2) + 1;
            spawnPos.x = Random.Range(-width / 2, (width / 2));

            GameObject enemy = Instantiate(Enemy4, spawnPos, Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
            enemy.transform.position = spawnPos;
            collisionManager.AddEnemy(enemy);
            */
            /*
            Vector3 spawnPos = Vector3.zero;

            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            spawnPos.y = (height / 2) + 1;
            spawnPos.x = Random.Range(-width / 2, (width / 2));

            GameObject enemy = Instantiate(Enemy5, spawnPos, Quaternion.identity, transform);
            enemy.GetComponent<EnemyType5>().player = player;
            enemy.GetComponent<EnemyType5>().screen = screen;
            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
            enemy.transform.position = spawnPos;
            collisionManager.AddEnemy(enemy);
            
            Vector3 spawnPos = Vector3.zero;

            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            spawnPos.y = (height / 2) + 1;
            spawnPos.x = 0 - (screen.hudWidth/2);

            GameObject enemy = Instantiate(Boss, spawnPos, Quaternion.identity, transform);
            enemy.GetComponent<Boss>().player = player;
            enemy.GetComponent<Boss>().screen = screen;
            enemy.GetComponent<Boss>().explosion = Explosion;
            enemy.GetComponent<Boss>().col = collisionManager;
            enemy.GetComponent<Enemy>().collisionManager = collisionManager;
            enemy.GetComponent<Enemy>().bulletBankManager = bulletBankManager;
            enemy.transform.position = spawnPos;
            collisionManager.AddEnemy(enemy);
        }*/
    }

    public void Begin()
    {
        start = true;
    }
}
