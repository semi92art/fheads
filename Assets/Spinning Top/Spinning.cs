using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForceModes
{
    acceleration,
    force,
    impulse,
    velocityChange
}


public class Spinning : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Vector3 torqVect;
    [SerializeField]
    private float maxAngVel;

    private bool isTorq;
    private ForceMode _forceMode;

    void Awake()
    {
        _rb.maxAngularVelocity = maxAngVel;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            isTorq = true;
        else if (Input.GetKeyUp(KeyCode.Q))
            isTorq = false;


        if (Input.GetKeyDown(KeyCode.Alpha1))
            _forceMode = ForceMode.Acceleration;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            _forceMode = ForceMode.Force;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            _forceMode = ForceMode.Impulse;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            _forceMode = ForceMode.VelocityChange;
    }

    void FixedUpdate()
    {
        Debug.Log(_rb.maxAngularVelocity);

        if (!isTorq)
        {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddTorque(torqVect, _forceMode);
            //_rb.angularVelocity = torqVect;
        }
    }
}
