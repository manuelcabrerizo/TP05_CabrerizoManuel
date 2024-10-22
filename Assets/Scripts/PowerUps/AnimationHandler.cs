using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private void DestroyGameObjectWhenAnimationFinish()
    {
        foreach (SpawnedPowerUp powerUp in PowerUpSpawner.Instance.GetSpawnedPowerUps())
        {
            if (powerUp.Obj == obj)
            {
                powerUp.timer = 0;
                return;
            }
        }

        Destroy(obj);
    }

}
