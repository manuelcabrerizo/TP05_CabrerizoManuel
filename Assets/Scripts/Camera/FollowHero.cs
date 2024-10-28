using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHero : MonoBehaviour
{
    [SerializeField] private GameObject hero;
    [SerializeField] private float restLengthX = 0.5f;
    [SerializeField] private float restLengthY = 1;
    [SerializeField] private float damping = 0.1f;
    [SerializeField] private float springContant = 10;

    private Vector2 _position;
    private Vector2 _velocity;
    private Vector2 _forceAcc;

    public Vector2 Velocity => _velocity;

    void Awake()
    {
        _position = transform.position;
        _velocity = new Vector2();
        _forceAcc = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        // update the camera position every frame
        transform.position = new Vector3(_position.x, _position.y, -10);
    }

    private void FixedUpdate()
    {
        GenerateCameraForces();
        Integrate(Time.fixedDeltaTime);
    }

    void GenerateCameraForces()
    { 
        // processing x
        float forceX = hero.transform.position.x - transform.position.x;
        float magnitudeX = Mathf.Abs(forceX);
        if (magnitudeX > restLengthX)
        {
            // Calculate the direction of the force
            float directionX = forceX / magnitudeX;
            // Calculate the magnitude of the force
            magnitudeX = springContant * (restLengthX - magnitudeX);
            // Calculate the final force
            AddForce(Vector2.left * directionX * magnitudeX);
        }
        // processing y
        float forceY = hero.transform.position.y - transform.position.y;
        float magnitudeY = Mathf.Abs(forceY);
        if (magnitudeY > restLengthY)
        {
            // Calculate the direction of the force
            float directionY = forceY / magnitudeY;
            // Calculate the magnitude of the force
            magnitudeY = springContant * (restLengthY - magnitudeY);
            // Calculate the final force
            AddForce(Vector2.down * directionY * magnitudeY);
        }
    }

    void Integrate(float dt)
    {
        _position += _velocity * dt;
        _velocity += _forceAcc * dt;
        _velocity *= Mathf.Pow(damping, dt);
        ClearForces();
    }

    void AddForce(Vector2 force)
    {
        _forceAcc += force;
    }

    void ClearForces()
    {
        _forceAcc = new Vector2();
    }
}
