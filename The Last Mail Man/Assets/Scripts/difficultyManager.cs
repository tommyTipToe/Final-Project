using UnityEngine;

public class difficultyManager : MonoBehaviour
{
    [SerializeField] bool Hardmode;

    public void setDifficulty(bool hard)
    {
        Hardmode = hard;
    }

    public bool getDifficulty()
    {
        return Hardmode;
    }
}
