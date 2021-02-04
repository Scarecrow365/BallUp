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
                coinParticle.transform.position = new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(4, 8),
                    coinParticle.transform.position.z);
                coinParticle.Play();
                break;
            case ParticleEffect.JumpCount:
                jumpParticle.transform.position = new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(4, 8),
                    jumpParticle.transform.position.z);
                jumpParticle.Play();
                break;
        }
    }
}