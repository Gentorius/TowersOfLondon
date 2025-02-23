using System.Collections.Generic;
using Levels;

namespace Turns
{
    public class SolutionFinder
    {
        const int _attempts = 1000;
        
        Solution _bestSolution;
        readonly List<Solution> _similarToBestSolutions = new();
        readonly IRandomTurnGenerator _turnGenerator = new TurnGenerator();

        public Solution FindBestSolution(LevelLayout startingLayout, LevelLayout goalLayout, out List<Solution> similarSolutions)
        {
            if (LayoutComparer.Compare(startingLayout, goalLayout))
            {
                similarSolutions = null;
                return null;
            }
            
            _bestSolution = GenerateStartingSolution(startingLayout, goalLayout);

            while (CouldFindABetterSolutionInANumberOfAttempts(startingLayout, goalLayout))
            {}
            
            similarSolutions = _similarToBestSolutions;
            return _bestSolution;
        }

        bool CouldFindABetterSolutionInANumberOfAttempts(LevelLayout startingLayout, LevelLayout goalLayout)
        {
            for (var i = 0; i < _attempts; i++)
            {
                if (!TryGenerateBetterSolution(startingLayout, goalLayout, out var solution))
                    continue;

                if (solution.TurnCount < _bestSolution.TurnCount)
                {
                    _bestSolution = solution;
                    _similarToBestSolutions.Clear();
                    return true;
                }
                
                if (solution.TurnCount == _bestSolution.TurnCount)
                {
                    _similarToBestSolutions.Add(solution);
                }
            }

            return false;
        }

        Solution GenerateStartingSolution(LevelLayout startingLayout, LevelLayout goalLayout)
        {
            var solution = new Solution();
            solution.SetStartingLayout(startingLayout);
            
            var currentLayout = GetACopyOf(startingLayout);
            
            while (!LayoutComparer.Compare(currentLayout, goalLayout))
            {
                _turnGenerator.TakeARandomTurn(ref currentLayout, ref solution);
            }
            
            return solution;
        }
        
        bool TryGenerateBetterSolution (LevelLayout startingLayout, LevelLayout goalLayout, out Solution solution)
        {
            solution = new Solution();
            solution.SetStartingLayout(startingLayout);
            
            var currentLayout = GetACopyOf(startingLayout);
            var exceedsBestSolution = false;
            
            while (!LayoutComparer.Compare(currentLayout, goalLayout))
            {
                _turnGenerator.TakeARandomTurn(ref currentLayout, ref solution);

                if (solution.TurnCount <= _bestSolution.TurnCount)
                    continue;

                exceedsBestSolution = true;
                break;
            }
            
            return !exceedsBestSolution;
        }

        LevelLayout GetACopyOf(LevelLayout layout)
        {
            return layout.Clone() as LevelLayout;
        }
    }
}