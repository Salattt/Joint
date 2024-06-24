using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Swing : MonoBehaviour
{
    [SerializeField] private float _connectedAnchorLenght;
    [SerializeField] private float _maxAngle;

    private Transform _transform;
    private Transform _movengPartTransform;
    private HingeJoint _joint;
    private Rigidbody _movingPartRigidbody;
    private Vector3 _velocity;
    private bool _isWork = false;

    private void Awake()
    {
        _joint = GetComponent<HingeJoint>();
        _movingPartRigidbody = _joint.connectedBody.GetComponent<Rigidbody>();
        _transform = transform;
        _movengPartTransform = _movingPartRigidbody.transform;
        _joint.connectedAnchor = new Vector3(0,_connectedAnchorLenght,0);
        _maxAngle = Mathf.Clamp(_maxAngle,0,180);
    }

    public void StartSwing()
    {
        if(_isWork == false) 
        { 
            _isWork = true;
            _movingPartRigidbody.velocity = CalculateVelocity();
        }
    }

    public void StopSwing()
    {
        if (_isWork == true)
        {
            _isWork = false;
            _movingPartRigidbody.velocity = Vector3.zero;
            _movengPartTransform.position = _transform.position - _joint.connectedAnchor;
            _movengPartTransform.eulerAngles = Vector3.zero ;
        }
    }

    private Vector3 CalculateVelocity()
    {
        float velocity;

        if(_maxAngle <= 90)
            velocity = Mathf.Sqrt(2 * -Physics.gravity.y * (_connectedAnchorLenght - _connectedAnchorLenght * Mathf.Sin(Mathf.Deg2Rad * (90 - _maxAngle))));
        else
            velocity = Mathf.Sqrt(2 * -Physics.gravity.y * (_connectedAnchorLenght + _connectedAnchorLenght * Mathf.Sin(Mathf.Deg2Rad * (-90 + _maxAngle))));

        return new Vector3(_joint.axis.z,0,_joint.axis.x).normalized * velocity;
    }
}
