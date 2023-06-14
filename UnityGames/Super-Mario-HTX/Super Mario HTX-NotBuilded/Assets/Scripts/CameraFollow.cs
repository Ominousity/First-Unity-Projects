using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;

    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        // Vi angiver kameraets x position til at være det samme som karakterens.
        Vector3 tempx = transform.position;
        tempx.x = PlayerTransform.position.x;
        transform.position = tempx;

        // Vi angiver kameraets y position til at være det samme som karakterens.
        Vector3 tempy = transform.position;
        tempy.y = PlayerTransform.position.y;
        transform.position = tempy;
    }
}
