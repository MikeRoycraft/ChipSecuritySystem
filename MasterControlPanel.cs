using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChipSecuritySystem
{
    public class MasterControlPanel
    {
        private readonly Color _startMarker;
        private readonly Color _endMarker;
        private ChipMatcher _chipMatcher;
        private IEnumerable<ColorChip> _channelSlot;

        public MasterControlPanel(Color startMarker, Color endMarker)
        {
            _startMarker = startMarker;
            _endMarker = endMarker;
            _chipMatcher = new ChipMatcher();
            _channelSlot = new List<ColorChip>();
        }

        public Color StartMarker
        {
            get
            {
                return _startMarker;
            }
        }

        public Color EndMarker
        {
            get
            {
                return _endMarker;
            }
        }

        public ChipMatcher ChipMatcher
        {
            get
            {
                return _chipMatcher;
            }
        }

        public IEnumerable<ColorChip> ChannelSlot 
        { 
            get
            {
                return _channelSlot;
            }
            set
            {
                _channelSlot = value;
            }
        }

        public void GenerateChipSeriesForChannelSlot(IEnumerable<ColorChip> chipSet)
        {
            var matchingChipList = new List<ColorChip>();

            // get first chip in series
            var firstChip = ChipMatcher.FindNextChipInSeries(StartMarker, chipSet);
            if (firstChip != null)
            {
                // add first chip to match list
                matchingChipList.Add(firstChip);
            }

            // loop through the number of chips in the chipSet, minus the first chip
            int i = 0;
            while (i <= chipSet.Count() - 1 && matchingChipList.Count != 0)
            {
                var previousChip = matchingChipList.Last();
                if (previousChip == null)
                {
                    break;
                }

                var unusedChips = chipSet.Except(matchingChipList);

                var nextChip = ChipMatcher.FindNextChipInSeries(previousChip.EndColor, unusedChips);
                if (nextChip != null)
                {
                    matchingChipList.Add(nextChip);
                }

                i++;
            }

            if (matchingChipList.Count > 0)
            {
                // get the last chip in the series to match the end marker
                var lastChipIndex = ChipMatcher.FindIndexOfLastChipInSeries(EndMarker, matchingChipList);

                matchingChipList.RemoveRange(lastChipIndex + 1, matchingChipList.Count() - (lastChipIndex + 1));

                ChannelSlot = matchingChipList;
            }
        }
    }
}