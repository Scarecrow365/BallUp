public class VfxController
{
    private int _coinCount;
    private int _jumpCount;
    private readonly VfxView _view;

    public VfxController(VfxView vfxView)
    {
        _view = vfxView;
    }

    public void SetCoinCount(int value)
    {
        _coinCount = value;
        if (_coinCount % 5 == 0)
        {
            _view.ActivateParticle(ParticleEffect.CoinCount);
        }
    }

    public void SetJumpCount(int value)
    {
        _jumpCount = value;
        if (_jumpCount % 5 == 0)
        {
            _view.ActivateParticle(ParticleEffect.JumpCount);
        }
    }
}