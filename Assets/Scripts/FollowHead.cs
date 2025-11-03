using UnityEngine;
//Follow a target with an offset
public class FollowHead : MonoBehaviour
{
    public Transform target; // target to follow
    public Vector3 offset = new Vector3(0, 1.2f, 0); // default offset above the target

    void LateUpdate() //check position after all updates
    {
        if (target != null)
            transform.position = target.position + offset;
    }
}
