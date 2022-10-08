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
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        if (direction.magnitude > 0) //So only turn when moving
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
    }
}
