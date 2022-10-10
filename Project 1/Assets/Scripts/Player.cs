using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Vector3 vehiclePosition = Vector3.zero;

    [SerializeField]
    Vector3 direction = Vector3.zero;

    [SerializeField]
    Vector3 velocity = Vector3.zero;

    [SerializeField]
    float turnAmount = 0f;

    [SerializeField]
    HUDManager manager;

    [SerializeField]
    BulletBankManager bank;

    [SerializeField]
    Sprite CenterFrame;
    [SerializeField]
    Sprite LeftFrame1;
    [SerializeField]
    Sprite LeftFrame2;
    [SerializeField]
    Sprite RightFrame1;
    [SerializeField]
    Sprite RightFrame2;

    private float velocityIndex = 0;

    private float power = 1;

    public float Power
    {
        get
        { 
            return power; 
        }
        set
        {
            power = value;
            if (power > 4)
                power = 4;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Normalize direction
        direction.Normalize();

        //Turn the vehicle by some angle
        direction = Quaternion.EulerAngles(0, 0, turnAmount * Time.deltaTime) * direction;

        //velocity is direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        //Add velocity to position
        vehiclePosition += velocity;

        //"Draw" the vehicle at that position
        transform.position = vehiclePosition;


        //Stop Y
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        if (vehiclePosition.y > height / 2)
            vehiclePosition.y = height / 2;
        else if (vehiclePosition.y < -height / 2)
            vehiclePosition.y = -height / 2;

        //Stop X
        if (vehiclePosition.x > (width / 2) - (manager.hudWidth * width))
            vehiclePosition.x = (width / 2) - (manager.hudWidth * width);
        else if (vehiclePosition.x < -width / 2)
            vehiclePosition.x = -width / 2;

        //apply movement visual
        if (direction.x > 0)
        {
            if (velocityIndex < 0)
                velocityIndex = 0;
            velocityIndex += .2f * 60 * Time.deltaTime;
        }
        else if (direction.x < 0)
        {
            if (velocityIndex > 0)
                velocityIndex = 0;
            velocityIndex -= .2f * 60 * Time.deltaTime;
        }
        else
        {
            if (velocityIndex < 0)
                velocityIndex += .2f * 60 * Time.deltaTime;
            else if (velocityIndex > 0)
                velocityIndex -= .2f * 60 * Time.deltaTime;

            //Lock to center when close
            if (velocityIndex < .02f && velocityIndex > -.02f)
                velocityIndex = 0;
        }
        if (velocityIndex > 2)
            velocityIndex = 2;
        if (velocityIndex < -2)
            velocityIndex = -2;

        if (velocityIndex == 0)
            GetComponent<SpriteRenderer>().sprite = CenterFrame;
        else if (velocityIndex > 1)
            GetComponent<SpriteRenderer>().sprite = LeftFrame2;
        else if (velocityIndex > 0)
            GetComponent<SpriteRenderer>().sprite = LeftFrame1;
        else if (velocityIndex < -1)
            GetComponent<SpriteRenderer>().sprite = RightFrame2;
        else if (velocityIndex < 0)
            GetComponent<SpriteRenderer>().sprite = RightFrame1;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void Shoot()
    {
        bank.RequestBullet(transform.position, "playerBulletSmall");
    }
}
