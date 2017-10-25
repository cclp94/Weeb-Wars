using UnityEngine;
using System.Collections;

public interface FollowTarget
{

    void SetTarget(Transform target);
    void Follow(Transform target);

}
