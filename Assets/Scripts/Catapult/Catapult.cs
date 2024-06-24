using UnityEngine;

public class Catapult : MonoBehaviour
{
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _springForce;
    [SerializeField] private HingeJoint _hingeJoint;
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private Rigidbody _workBodyRigidbody;
    [SerializeField] private Rigidbody _missileRigidbody;

    private Transform _workBodyTransform;
    private Transform _missileTransform;
    private Vector3 _workBodyStartPosition;
    private Vector3 _workBodyStartAngle;
    private Vector3 _missileStartPosition;
    private Vector3 _missileStartAngle;

    private void Awake()
    {
        JointLimits jointLimits = new JointLimits();

        _workBodyTransform = _workBodyRigidbody.transform;
        _workBodyStartPosition = _workBodyTransform.position;
        _workBodyStartAngle = _workBodyTransform.eulerAngles;
        _missileTransform = _missileRigidbody.transform;
        _missileStartPosition = _missileTransform.transform.position;
        _missileStartAngle = _workBodyTransform.eulerAngles;
        jointLimits.max = _maxAngle;
        _hingeJoint.useLimits = true;
        _hingeJoint.limits = jointLimits;
    }

    public void Throw()
    {
        _workBodyRigidbody.WakeUp();
        _springJoint.spring = _springForce;
    }

    public void Restart()
    {
        _springJoint.spring = 0;
        _workBodyTransform.eulerAngles = _workBodyStartAngle;
        _workBodyTransform.position = _workBodyStartPosition;
        _missileTransform.eulerAngles = _missileStartAngle;
        _missileRigidbody.velocity = Vector3.zero;
        _missileTransform.position = _missileStartPosition;
    }
}
