using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string colliderTag = "Player";

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == colliderTag)
        {
            Destroy(this.gameObject);
        }
    }
}
