using UnityEngine;

public interface FollowTarget
{

    void SetTarget(Transform target);
    void Follow(Transform target);

}
