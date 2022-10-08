using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBankManager : MonoBehaviour
{
    [SerializeField]
    HUDManager screen;
    //Bullet Types
    [SerializeField]
    GameObject playerBulletSmall;
    [SerializeField]
    GameObject enemyBullet1;
    [SerializeField]
    CollisionManager collisionManager;

    //Bullet Storage
    private Stack<GameObject> playerBulletSmallBank;
    private Stack<GameObject> enemyBullet1Bank;

    // Start is called before the first frame update
    void Start()
    {
        playerBulletSmallBank = new Stack<GameObject>();
        //Spawn initial Bullets

        //TODO: find out why spawning more here causes bullets to vanish
        for (int i = 0; i < 1; i++)
        {
            GameObject bullet = Instantiate(playerBulletSmall, transform.position, Quaternion.identity, transform);
            bullet.GetComponent<Playerbullet>().SetBank(gameObject);
            bullet.GetComponent<Playerbullet>().Collide();
            bullet.GetComponent<Playerbullet>().SetScreen(screen);
            playerBulletSmallBank.Push(bullet);
        }

        enemyBullet1Bank = new Stack<GameObject>();
        for (int i = 0; i < 1; i++)
        {
            GameObject bullet = Instantiate(enemyBullet1, transform.position, Quaternion.identity, transform);
            bullet.GetComponent<Playerbullet>().SetBank(gameObject);
            bullet.GetComponent<Playerbullet>().Collide();
            bullet.GetComponent<Playerbullet>().SetScreen(screen);
            enemyBullet1Bank.Push(bullet);
        }
    }

    public Stack<GameObject> getPlayerBulletSmalls()
    {
        return playerBulletSmallBank;
    }

    public void RequestBullet(Vector3 position, string kind)
    {
        Stack<GameObject> bank = null;
        GameObject bulletType = null;
        GameObject bullet = null;
        switch (kind)
        {
            case "playerBulletSmall":
                bank = playerBulletSmallBank;
                bulletType = playerBulletSmall;
                break;
            case "enemyBullet1":
                bank = enemyBullet1Bank;
                bulletType = enemyBullet1;
                break;
        }

        if (bank.Count > 0)
        {
            bullet = bank.Pop();
            bullet.GetComponent<Playerbullet>().Activate();
            bullet.transform.position = position;
        }
        else //new bullet
        {
            bullet = Instantiate(bulletType, transform.position, Quaternion.identity, transform);
            bullet.GetComponent<Playerbullet>().SetBank(gameObject);
            bullet.GetComponent<Playerbullet>().Activate();
            bullet.GetComponent<Playerbullet>().SetScreen(screen);
            bullet.transform.position = position;
        }

        //Add bullets to collisionManager
        switch (kind)
        {
            case "playerBulletSmall":
                collisionManager.smallPlayerBullets.Add(bullet);
                break;
            case "enemyBullet1":
                collisionManager.enemyBullets.Add(bullet);
                break;
        }
    }

    public void ReturnBullet(GameObject bullet, string kind)
    {
        switch(kind)
        {
            case "playerBulletSmall":
                playerBulletSmallBank.Push(bullet);
                collisionManager.smallPlayerBullets.Remove(bullet);
                break;
            case "enemyBullet1":
                enemyBullet1Bank.Push(bullet);
                collisionManager.enemyBullets.Remove(bullet);
                break;
        }
        bullet.transform.position = transform.position;
    }
}
