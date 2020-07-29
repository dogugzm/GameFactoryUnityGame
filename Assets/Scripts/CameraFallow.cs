
using UnityEngine;

public class CameraFallow : MonoBehaviour
{

    

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _smoothness = 1.0f;
    [SerializeField]
    private Vector3 _offset;  // ne kadar uzaktan takip ettiğim. 

    private void FixedUpdate()
    {
        if (_target==null)
        {
            return;
        
        }

        transform.position = Vector3.Lerp(transform.position , _target.position + _offset , Time.deltaTime * _smoothness);
    }

}
