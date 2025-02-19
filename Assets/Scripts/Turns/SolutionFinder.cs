using System.Collections.Generic;
using Levels;

namespace Turns
{
    public class SolutionFinder
    {
        Solution _bestSolution;
        List<Solution> _similarToBestSolutions = new();
        
        
        public Solution FindBestSolution(LevelLayout startingLayout, LevelLayout goalLayout)
        {
            if (LayoutComparer.Compare(startingLayout, goalLayout))
            {
                return null;
            }
            
            
            
            
        }

        Solution GenerateStartingSolution(LevelLayout startingLayout, LevelLayout goalLayout)
        {
            var solution = new Solution();
            solution.SetStartingLayout(startingLayout);
            
            var currentLayout = startingLayout;
        }
    }
}