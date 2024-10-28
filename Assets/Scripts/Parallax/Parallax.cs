using UnityEngine;

public enum ParallaxLayer
{ 
    Sky,   // 0.25
    Sea,   // 0.5
    Beach, // 2
    Palms, // 2
    Ground // 3
}

public class Parallax : MonoBehaviour
{
    SpriteRenderer spriteRendeder;
    FollowHero _followHero;
    float magnitude = 0;

    
    void Awake()
    {
        spriteRendeder = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _followHero = GameManager.Instance.Camera.GetComponent<FollowHero>();
    }

    // Update is called once per frame
    void Update()
    {
        float cameraVelX = _followHero.Velocity.x;
        magnitude += ((cameraVelX / transform.position.z)/60.0f) * Time.deltaTime;
        spriteRendeder.material.SetTextureOffset("_MainTex", Vector2.right * magnitude);
    }
}
