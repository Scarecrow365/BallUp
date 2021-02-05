using UnityEngine;

public class VfxView : MonoBehaviour
{
    [SerializeField] private ParticleSystem coinParticle;
    [SerializeField] private ParticleSystem jumpParticle;

    public void ActivateParticle(ParticleEffect effect)
    {
        switch (effect)
        {
            case ParticleEffect.CoinCount:
                coinParticle.transform.position = SetRandomPos(coinParticle.gameObject);
                coinParticle.Play();
                break;
            case ParticleEffect.JumpCount:
                jumpParticle.transform.position = SetRandomPos(jumpParticle.gameObject);
                jumpParticle.Play();
                break;
        }
    }

    private Vector3 SetRandomPos(GameObject obj) => new Vector3(
        Random.Range(-4, 4),
        Random.Range(2, 6),
        obj.transform.position.z);
}