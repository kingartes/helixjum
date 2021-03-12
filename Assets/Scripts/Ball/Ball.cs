using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlatformSegment platformSegment))
        {
            other.GetComponentInParent<Platform>().Break();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var isFinishPlatform = collision.gameObject.TryGetComponent(out FinishPlatform finishPlatform);
        if (isFinishPlatform)
        {
            finishPlatform.Win();
        }
    }
}
