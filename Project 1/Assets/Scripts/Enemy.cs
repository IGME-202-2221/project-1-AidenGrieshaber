using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health;

    [SerializeField]
    public CollisionManager collisionManager;

    [SerializeField]
    int pointAmount;

    [SerializeField]
    public BulletBankManager bulletBankManager;

    [SerializeField]
    string powerType;

    public void damage(int damage)
    {
        health -= damage;
        if (health < 0)
            kill();
    }

    private void kill()
    {
        collisionManager.RemoveEnemy(gameObject);
        collisionManager.RegisterKill(pointAmount);
        Destroy(gameObject);
        bulletBankManager.RequestBullet(transform.position, powerType);
    }
}
