using UnityEngine;

namespace Settings
{
    public class Settings : MonoBehaviour
    {
        Difficulty _difficulty = Difficulty.Medium;
        
        public int GetTurnSum(int turnCount)
        {
            return _difficulty switch
            {
                Difficulty.Easy => turnCount + 4,
                Difficulty.Medium => turnCount + 2,
                Difficulty.Hard => turnCount,
                _ => turnCount + 2
            };
        }
    }
}