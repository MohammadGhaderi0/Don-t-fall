public class PotionBarScript : SliderController
{
    protected override float GetValue()
    {
        return playerController.potionTime;
    }
}



