public class PowerBarScript : SliderController
{
    protected override float GetValue()
    {
        return playerController.powerupTime;
    }
}
