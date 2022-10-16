using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    public GameObject player;
    
    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude < 4)
        {
            Vector3 vel = gameObject.GetComponent<Playerbullet>().velocity;
            Vector3 toPlayer = player.transform.position - transform.position;

            float playerAngle = (float)Mathf.Atan2(toPlayer.y, toPlayer.x);
            float velocityAngle = (float)Mathf.Atan2(vel.y, vel.x);

            float angleBetween = playerAngle - velocityAngle;
            if (Mathf.Abs(angleBetween) > .015f)
                angleBetween = .015f * (angleBetween/ Mathf.Abs(angleBetween));

            gameObject.GetComponent<Playerbullet>().velocity = RotateRadians(vel, angleBetween);
        }
    }

    private Vector3 RotateRadians(Vector3 v, float radians)
    {
        var ca = Mathf.Cos(radians);
        var sa = Mathf.Sin(radians);
        return new Vector3(ca * v.x - sa * v.y, sa * v.x + ca * v.y, v.z);
    }
}
