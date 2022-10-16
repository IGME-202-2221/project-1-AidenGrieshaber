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

    [SerializeField]
    LivesManager livesManager;

    public List<GameObject> smallPlayerBullets = new List<GameObject>();
    public List<GameObject> largePlayerBullets = new List<GameObject>();
    public List<GameObject> enemyBullets = new List<GameObject>();
    public List<GameObject> smallPowerItems = new List<GameObject>();
    public List<GameObject> largePowerItems = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        //LOOP REVERSE FOR ALL, OBJECTS MAY BE REMOVED FROM COLLECTIONS IF THE RESULT OF COLLISION INCLUDE THEM BEING MOVED, REVERSE TO AVOID MISSING INDEXES DUE TO REMOVAL
        //Enemy-Player contact
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (GetComponent<CollisionDetection>().CircleCollision(player, enemies[i]))
            {
                enemies[i].GetComponent<Enemy>().damage(10);
                Player p = player.GetComponent<Player>();
                p.Lives--;
                livesManager.DecreaseLife();
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
        for (int i = largePlayerBullets.Count - 1; i >= 0; i--)
        {
            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                if (GetComponent<CollisionDetection>().AABBCollision(largePlayerBullets [i], enemies[j]))
                {
                    enemies[j].GetComponent<Enemy>().damage(3);
                    largePlayerBullets[i].GetComponent<Playerbullet>().Collide();
                }
            }
        }
        //Player hit by enemy bullets
        for (int i = enemyBullets.Count - 1; i >= 0; i--)
        {
            if (GetComponent<CollisionDetection>().CircleCollision(player, enemyBullets[i]))
            {
                enemyBullets[i].GetComponent<Playerbullet>().Collide();
                Player p = player.GetComponent<Player>();
                p.Lives--;
                livesManager.DecreaseLife();
            }
        }
        //player collect items
        for (int i = smallPowerItems.Count - 1; i >= 0; i--)
        {
            if (GetComponent<CollisionDetection>().AABBCollision(player, smallPowerItems[i]))
            {
                smallPowerItems[i].GetComponent<Playerbullet>().Collide();
                player.GetComponent<Player>().Power += 0.04f;

                hud.power = player.GetComponent<Player>().Power;
            }
        }
        //player collect items
        for (int i = largePowerItems.Count - 1; i >= 0; i--)
        {
            if (GetComponent<CollisionDetection>().AABBCollision(player, largePowerItems[i]))
            {
                largePowerItems[i].GetComponent<Playerbullet>().Collide();
                player.GetComponent<Player>().Power += 0.2f;

                hud.power = player.GetComponent<Player>().Power;
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
