using UnityEngine;
using System.Collections;

public class RotatorBehaviour : MonoBehaviour
{
    public float m_HingeJointSpeed;
    public float m_changeSpeed;
    float timer;
    HingeJoint m_thisHingeJoint;

	// Use this for initialization
	void Start ()
    {
        timer = 0;
        StartCoroutine(MotorSpeed());
	}
	
	IEnumerator MotorSpeed()
    {
        yield return new WaitForSeconds(timer);

        m_thisHingeJoint = GetComponent<HingeJoint>();
        JointMotor joint = m_thisHingeJoint.motor;
        joint.targetVelocity = m_HingeJointSpeed;
        m_thisHingeJoint.motor = joint;

        if (timer != m_changeSpeed) timer = m_changeSpeed;
    }
}
