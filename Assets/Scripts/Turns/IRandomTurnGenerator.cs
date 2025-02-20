using Levels;

namespace Turns
{
    public interface IRandomTurnGenerator
    {
        void TakeARandomTurn(ref LevelLayout currentLayout, ref Solution solution);
    }
}