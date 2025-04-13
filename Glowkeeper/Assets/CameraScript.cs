using UnityEngine;

public class MatchParentTransform : MonoBehaviour
{
    // Set this to true if you want to match rotation and scale as well
    public bool matchRotation = false;
    public bool matchScale = false;

    void Start()
    {
        if (transform.parent != null)
        {
            // Match position to parent
            transform.localPosition = -Vector3.zero;

            if (matchRotation)
                transform.localRotation = Quaternion.identity;

            if (matchScale)
                transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogWarning("No parent found for " + gameObject.name);
        }
    }
}
