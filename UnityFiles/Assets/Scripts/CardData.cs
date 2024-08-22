[System.Serializable]
public class CardData
{
    public string description;
    public string leftAction;
    public string rightAction;
    public string leftConclusion;
    public string rightConclusion;
    public bool leftConclusionShown = false;
    public bool rightConclusionShown = false;
    public float healthRight;
    public float hungerRight;
    public float moodRight;
    public float energyRight;
    public float healthLeft;
    public float hungerLeft;
    public float moodLeft;
    public float energyLeft;
}