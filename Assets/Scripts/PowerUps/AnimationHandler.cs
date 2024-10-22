using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private void DestroyGameObjectWhenAnimationFinish()
    {
        Destroy(obj);
    }

}
