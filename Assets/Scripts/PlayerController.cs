using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float XMin, XMax, ZMin, ZMax;
}

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float Tilt;
    public Boundary Boundary;

    public GameObject Shot;
    public Transform ShotSpawn;
    public float FireRate;
    public SimpleTouchPad TouchPad;
    public SimpleTouchAreaButton AreaButton;

    private float _nextFire;
    private Quaternion _calibrationQuaternion;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        CalibrateAccelerometer();
    }

    private void Update()
    {
        if (AreaButton.CanFire() && Time.time > _nextFire)
        {
            _nextFire = Time.time + FireRate;
            Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
            _audioSource.Play();
        }
    }

    private void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), accelerationSnapshot);
        _calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    private Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = _calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    private void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //_rigidbody.velocity = movement * Speed;
        
        //Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(accelerationRaw);

        Vector2 direction = TouchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
        _rigidbody.velocity = movement * Speed;

        _rigidbody.position = new Vector3
        (
            Mathf.Clamp(_rigidbody.position.x, Boundary.XMin, Boundary.XMax),
            0.0f,
            Mathf.Clamp(_rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)
        );

        _rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, _rigidbody.velocity.x * -Tilt);
    }
}
