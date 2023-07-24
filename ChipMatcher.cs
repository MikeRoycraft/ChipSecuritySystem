using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    public class ChipMatcher
    {
        /// <summary>
        /// Finds the next matching chip in the series.
        /// </summary>
        /// <param name="targetColor"></param>
        /// <param name="chips"></param>
        /// <returns>The next chip in the series or null if no target is found.</returns>
        public ColorChip FindNextChipInSeries(Color targetColor, IEnumerable<ColorChip> chips)
        {
            foreach (var chip in chips)
            {
                if (chip.StartColor == targetColor)
                {
                    return chip;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the index of the last chip in the series.
        /// </summary>
        /// <param name="targetColor"></param>
        /// <param name="chips"></param>
        /// <returns>The index of the last chip in the series.</returns>
        public int FindIndexOfLastChipInSeries(Color targetColor, IEnumerable<ColorChip> chips)
        {
            var chipsInSeries = chips.ToList();
            var lastChip = chipsInSeries.Last();

            if (lastChip.EndColor == targetColor)
            {
                return chipsInSeries.LastIndexOf(lastChip);
            }

            chipsInSeries.Remove(lastChip);

            return FindIndexOfLastChipInSeries(targetColor, chipsInSeries);
        }
    }
}