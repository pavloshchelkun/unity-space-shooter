using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
	public Boundary Boundary;
	public float Tilt;
	public float Dodge;
	public float Smoothing;
	public Vector2 StartWait;
	public Vector2 ManeuverTime;
	public Vector2 ManeuverWait;

    private Rigidbody _rigidbody;
	private float _currentSpeed;
	private float _targetManeuver;

    private void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentSpeed = _rigidbody.velocity.z;
		StartCoroutine(Evade());
	}

    private IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (StartWait.x, StartWait.y));
		while (true)
		{
			_targetManeuver = Random.Range (1, Dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (ManeuverTime.x, ManeuverTime.y));
			_targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (ManeuverWait.x, ManeuverWait.y));
		}
	}

    private void FixedUpdate ()
	{
		float newManeuver = Mathf.MoveTowards (_rigidbody.velocity.x, _targetManeuver, Smoothing * Time.deltaTime);
        _rigidbody.velocity = new Vector3 (newManeuver, 0.0f, _currentSpeed);
        _rigidbody.position = new Vector3
		(
			Mathf.Clamp(_rigidbody.position.x, Boundary.XMin, Boundary.XMax), 
			0.0f, 
			Mathf.Clamp(_rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)
		);

        _rigidbody.rotation = Quaternion.Euler (0, 0, _rigidbody.velocity.x * -Tilt);
	}
}
