using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ColorChip> chipSet = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Green, Color.Orange),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Purple, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Blue),
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Blue, Color.Green)
            };


            var controlPanel = new MasterControlPanel(Color.Blue, Color.Green);

            controlPanel.GenerateChipSeriesForChannelSlot(chipSet);

            if (controlPanel.ChannelSlot.Count() > 0)
            {
                Console.WriteLine($"Master panel unlocked using {controlPanel.ChannelSlot.Count()} chips.");
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }

            Console.ReadLine();
        }
    }
}