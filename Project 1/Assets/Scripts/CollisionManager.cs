using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> obstacles = new List<GameObject>();

    [SerializeField]
    GameObject player;

    [SerializeField]
    TextMeshPro collisionType;

    bool AABBCollision = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
        foreach (GameObject g in obstacles)
        {
            g.GetComponent<SpriteRenderer>().color = Color.white;

            if (AABBCollision)
            {
                if (GetComponent<CollisionDetection>().AABBCollision(player, g))
                {
                    g.GetComponent<SpriteRenderer>().color = Color.red;
                    player.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else
            {
                if (GetComponent<CollisionDetection>().CircleCollision(player, g))
                {
                    g.GetComponent<SpriteRenderer>().color = Color.red;
                    player.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    public void ChangeCollision()
    {
        AABBCollision = !AABBCollision;

        if (AABBCollision)
        {
            collisionType.text = "Press C to change collision type! \nCurrent collision type: \nAABB <<< \nBounding Circles";
        }
        else
        {
            collisionType.text = "Press C to change collision type! \nCurrent collision type: \nAABB \nBounding Circles <<<";
        }
    }
}
