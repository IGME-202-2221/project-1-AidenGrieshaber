using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    GameObject player;

    [SerializeField]
    HUDManager hud;

    public List<GameObject> smallPlayerBullets = new List<GameObject>();
    public List<GameObject> enemyBullets = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        //LOOP REVERSE FOR ALL, OBJECTS MAY BE REMOVED FROM COLLECTIONS IF THE RESULT OF COLLISION INCLUDE THEM BEING MOVED, REVERSE TO AVOID MISSING INDEXES DUE TO REMOVAL
        //Enemy-Player contact
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (GetComponent<CollisionDetection>().CircleCollision(player, enemies[i]))
            {
                enemies[i].GetComponent<Enemy>().damage(20);
            }
        }
        //Enemy damage from bullets
        for (int i = smallPlayerBullets.Count - 1; i >= 0; i--)
        {
            for(int j = enemies.Count - 1; j >= 0; j--)
            {
                if (GetComponent<CollisionDetection>().AABBCollision(smallPlayerBullets[i], enemies[j]))
                {
                    enemies[j].GetComponent<Enemy>().damage(1);
                    smallPlayerBullets[i].GetComponent<Playerbullet>().Collide();
                }
            }
        }
        //Player hit by enemy bullets
        for (int i = enemyBullets.Count - 1; i >= 0; i--)
        {
            if (GetComponent<CollisionDetection>().CircleCollision(player, enemyBullets[i]))
            {
                enemyBullets[i].GetComponent<Playerbullet>().Collide();
            }
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void RegisterKill(int pointAmount)
    {
        hud.score += pointAmount;
    }
}
