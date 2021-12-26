using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + new Vector3(0, 30, 0);

        transform.rotation = Quaternion.Euler(90f, playerPos.rotation.y, 90f);
    }
}
