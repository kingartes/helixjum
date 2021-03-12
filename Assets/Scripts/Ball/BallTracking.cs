using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTracking : MonoBehaviour
{
    [SerializeField] private Vector3 _directionOffset;
    [SerializeField] private float _length;

    private Ball _ball;
    private Cylinder _cylinder;
    private Vector3 _cameraPosition;
    private Vector3 _minimumBallPostion;


    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
        _cylinder = FindObjectOfType<Cylinder>();

        _cameraPosition = _ball.transform.position;
        _minimumBallPostion = _ball.transform.position;

        TrackBall();
    }

    private void Update()
    {
        if(_ball.transform.position.y < _minimumBallPostion.y)
        {
            TrackBall();
            _minimumBallPostion = _ball.transform.position;
        }
    }
    private void TrackBall()
    {
        var cylinderPosition = _cylinder.transform.position;
        cylinderPosition.y = _ball.transform.position.y;
        _cameraPosition = _ball.transform.position;
        var direction = (cylinderPosition - _ball.transform.position).normalized + _directionOffset;
        _cameraPosition -= direction * _length;
        transform.position = _cameraPosition;
        transform.LookAt(_ball.transform);
    }
}
