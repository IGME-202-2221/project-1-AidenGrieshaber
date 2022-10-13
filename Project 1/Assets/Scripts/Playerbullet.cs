using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbullet : MonoBehaviour
{
    [SerializeField]
    public Vector3 velocity;

    [SerializeField]
    HUDManager manager;

    [SerializeField]
    string name;

    private bool active = false;

    private GameObject BulletBank;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.position += velocity * Time.deltaTime;

            //Check for far bounds
            //Stop Y
            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            if (transform.position.y > (height / 2))
            {
                Collide();
            }
            else if (transform.position.y < -(height / 2))
            {
                Collide();
            }
        }
    }

    public void Collide()
    {
        active = false;
        BulletBank.GetComponent<BulletBankManager>().ReturnBullet(gameObject, name);
    }

    public void Activate()
    {
        active = true;
    }

    public void SetBank(GameObject bank)
    {
        BulletBank = bank;
    }

    public void SetScreen(HUDManager screen)
    {
        manager = screen;
    }
}
