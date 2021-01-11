using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    private float zoomMin;
    private float zoomMax;

    private float positionXMin;
    private float positionXMax;

    private float positionZMin;
    private float positionZMax;

    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;

    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosiion;

    float MouseZoomSpeed = 15.0f;
    float TouchZoomSpeed = 0.05f;
    float ZoomMinBound = 6f;
    float ZoomMaxBound = 40f;

    private float _speed = 0f;
    private float _rotationSpeed = 0f;

    private Vector3 _rotation;

    [SerializeField] private Camera _camera;

    Touch _touch1, _touch2;
    private float _diff;
    private float _distance;

    // 
    const float pinchTurnRatio = Mathf.PI / 2;
    const float minTurnAngle = 0;

    const float pinchRatio = 0.5f;
    const float minPinchDistance = 0;

    const float panRatio = 1;
    const float minPanDistance = 0;
    static public float turnAngleDelta;
    static public float turnAngle;

    public float pinchDistanceDelta;
    static public float pinchDistance;

    Quaternion desiredRotation;

    // Start is called before the first frame update
    void Start()
    {
        zoomMin = 15.0f;
        zoomMax = 40.0f;

        positionXMin = -45.0f;
        positionXMax = 45.0f;

        positionZMin = -35.0f;
        positionZMax = 35.0f;
        _rotation = transform.rotation.eulerAngles;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    private void LateUpdate()
    {

        if (Input.touchCount > 0)
        {
            _touch1 = Input.GetTouch(0);
            if (Input.touchCount == 1)
            {
                switch (_touch1.phase)
                {
                    case TouchPhase.Began:
                        Plane plane = new Plane(Vector3.up, Vector3.zero);

                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        float entry;

                        if (plane.Raycast(ray, out entry))
                        {
                            dragStartPosition = ray.GetPoint(entry);
                        }
                        break;
                    case TouchPhase.Moved:
                        Plane plane1 = new Plane(Vector3.up, Vector3.zero);

                        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);

                        float entry1;

                        if (plane1.Raycast(ray1, out entry1))
                        {
                            dragCurrentPosition = ray1.GetPoint(entry1);

                            newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                        }
                        break;
                }
            }
            else if (Input.touchCount == 2)
            {
                desiredRotation = transform.rotation;
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);




                turnAngle = Angle(tZero.position, tOne.position);
                float prevTurn = Angle(tZeroPrevious,tOnePrevious);
                turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);
               
                if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                {
                    turnAngleDelta *= pinchTurnRatio;
                }
                else
                {
                    turnAngle = turnAngleDelta = 0;
                }
                Debug.Log(turnAngleDelta);

                if (Mathf.Abs(turnAngleDelta) > 0)
                {
                    Vector3 rotationDeg = Vector3.zero;
                    rotationDeg.y = turnAngleDelta;
                    //rotationDeg.y *= 10;
                    desiredRotation *= Quaternion.Euler(rotationDeg);
                }
            }
        }

        if (_camera.fieldOfView < ZoomMinBound)
        {
            _camera.fieldOfView = 0.1f;
        }
        else if (_camera.fieldOfView > ZoomMaxBound)
        {
            _camera.fieldOfView = 179.9f;
        }


        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.9f);


    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        _camera.fieldOfView += deltaMagnitudeDiff * speed;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, ZoomMinBound, ZoomMaxBound);
    }

    private float Angle(Vector2 pos1, Vector2 pos2)
    {
        Vector2 from = pos2 - pos1;
        float angle = Mathf.Atan2(from.y, from.x) * Mathf.Rad2Deg;

        return angle;
    }
}