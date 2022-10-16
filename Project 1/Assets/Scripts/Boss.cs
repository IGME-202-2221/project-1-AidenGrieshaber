using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float bulletCounter = 0;
    private float moveTimer = 0;
    private float delayTimer = 0;
    private int bulletsFired = 0;
    private float phasePause = 0;
    private Vector3 toPlayer = Vector3.zero;

    public GameObject player;

    public HUDManager screen;

    private bool moving = true;

    private Vector3 moveTo = Vector3.zero;

    private int phase = -1;

    [SerializeField]
    List<Sprite> spritesheet = new List<Sprite>();

    private float spriteTimer = 0;
    private int spriteNum = 0;

    //Phase 1 stuff
    private bool left = false;

    //Phase 2 stuff
    private Vector3 vecVel = Vector3.zero;
    private float angleTicker = 0;
    private float secondaryBulletCounter;

    // Update is called once per frame
    void Update()
    {
        //movement
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        spriteTimer += 60 * Time.deltaTime;
        if (spriteTimer > 10) //Simple spritesheet loop
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spritesheet[spriteNum];
            spriteNum++;
            if (spriteNum >= spritesheet.Count)
                spriteNum = 0;
        }

        if (phasePause > 0)
        {
            Vector3 moveTo = Vector3.zero;
            moveTo.x = -(screen.hudWidth * width) / 2;
            moveTo.y = (height / 2) - (height / 10);
            phasePause -= 60 * Time.deltaTime;
            Vector3 vecVel = (moveTo - transform.position) / 2;
        }
        else
        {
            switch (phase)
            {
                case -1: //Move into screen
                    moveTimer += 60 * Time.deltaTime;
                    if (moveTimer < 300)
                    {
                        Vector3 velocity = new Vector3(0, -.1f, 0) * Time.deltaTime;
                        transform.rotation = Quaternion.LookRotation(Vector3.back, velocity.normalized);
                        transform.position += velocity;
                    }
                    else
                    {
                        phase = 1;
                        phasePause = 240;
                    }
                    break;
                case 0: //Death
                    break;
                case 1:
                    moveTimer += 60 * Time.deltaTime;
                    bulletCounter += 60 * Time.deltaTime;
                    if (moveTimer > 240)
                    {
                        delayTimer += 60 * Time.deltaTime;
                        if (delayTimer > 20)
                        {
                            ShootPair();
                            bulletsFired++;
                            delayTimer = 0;
                        }
                        if (bulletsFired > 6)
                        {
                            moveTimer = 0;
                            delayTimer = 0;
                        }
                    }
                    else
                    {
                        if (left)
                        {
                            Vector3 velocity = new Vector3(-1, 0, 0) * Time.deltaTime;
                            transform.position += velocity;
                            if (transform.position.x < (-width/2) + (width / 20))
                            {
                                left = !left;
                            }
                        }
                        else
                        {
                            Vector3 velocity = new Vector3(1, 0, 0) * Time.deltaTime;
                            transform.position += velocity;
                            if (transform.position.x > (width / 2) - (width / 20) - (screen.hudWidth * width))
                            {
                                left = !left;
                            }
                        }
                        toPlayer = player.transform.position - transform.position;
                        transform.rotation = Quaternion.LookRotation(Vector3.back, toPlayer.normalized);
                    }
                    if (bulletCounter > 30)
                    {
                        ShootDownBeam();
                    }
                    if (gameObject.GetComponent<Enemy>().health < 800)
                    {
                        phase = 2;
                        phasePause = 180;
                        moveTimer = 0;
                        bulletCounter = 0;
                        delayTimer = 0;
                        bulletsFired = 0;
                    }
                    break;
                case 2:
                    moveTimer += 60 * Time.deltaTime;
                    if (moveTimer < 120)
                    {
                        vecVel *= Time.deltaTime;
                        transform.rotation = Quaternion.LookRotation(Vector3.back, vecVel.normalized);
                        transform.position += vecVel;
                    }
                    else
                    {
                        toPlayer = player.transform.position - transform.position;
                        transform.rotation = Quaternion.LookRotation(Vector3.back, toPlayer.normalized);

                        delayTimer += 60 * Time.deltaTime;
                        if (delayTimer > 20)
                        {
                            ShootAcceleratingRing(angleTicker);
                            angleTicker += 20;
                            delayTimer = 0;
                        }
                        bulletCounter += 60 * Time.deltaTime;
                        if (bulletCounter > 180)
                        {
                            secondaryBulletCounter += 60 * Time.deltaTime;
                            if (secondaryBulletCounter > 20)
                            {
                                FastShot();
                                bulletsFired++;
                                secondaryBulletCounter = 0;
                                if (bulletsFired > 2)
                                {
                                    bulletCounter = 0;
                                }
                            }
                        }
                    }
                    if (gameObject.GetComponent<Enemy>().health < 400)
                    {
                        phase = 3;
                        phasePause = 180;
                        moveTimer = 0;
                        bulletCounter = 0;
                        delayTimer = 0;
                        bulletsFired = 0;
                    }
                    break;
                case 3:
                    break;
            }
        }
    }

    private void ShootDownBeam()
    {
        Vector3 trajectory = moveTo - transform.position;
        //FINISH
    }

    private void ShootPair()
    {
        GameObject bullet = gameObject.GetComponent<Enemy>().bulletBankManager.RequestBullet(transform.position, "enemyBullet1");
        Vector3 trajectory = toPlayer;
        trajectory = RotateDegrees(trajectory, -30f);
        bullet.GetComponent<Playerbullet>().velocity = trajectory.normalized * 4;
        GameObject bullet2 = gameObject.GetComponent<Enemy>().bulletBankManager.RequestBullet(transform.position, "enemyBullet1");
        Vector3 trajectory2 = toPlayer;
        trajectory2 = RotateDegrees(trajectory2, 30f);
        bullet2.GetComponent<Playerbullet>().velocity = trajectory2.normalized * 4;
    }

    private Vector3 RotateDegrees(Vector3 v, float degrees)
    {
        return RotateRadians(v, degrees * Mathf.Deg2Rad);
    }

    private Vector3 RotateRadians(Vector3 v, float radians)
    {
        var ca = Mathf.Cos(radians);
        var sa = Mathf.Sin(radians);
        return new Vector3(ca * v.x - sa * v.y, sa * v.x + ca * v.y, v.z);
    }

    private void ShootAcceleratingRing(float extra)
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject bullet = gameObject.GetComponent<Enemy>().bulletBankManager.RequestBullet(transform.position, "enemyBullet3");
            Vector3 trajectory = toPlayer;
            trajectory = RotateDegrees(trajectory, (30 * i) + extra);
            bullet.GetComponent<Playerbullet>().velocity = trajectory.normalized;
        }
    }

    private void FastShot()
    {
        for (int i = 0; i <= 2; i++)
        {
            GameObject bullet = gameObject.GetComponent<Enemy>().bulletBankManager.RequestBullet(transform.position, "enemyBullet3");
            Vector3 trajectory = toPlayer;
            bullet.GetComponent<Playerbullet>().velocity = trajectory.normalized * (4 + i * 2);
        }
    }
}
