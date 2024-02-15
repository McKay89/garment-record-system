using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarmentRecordSystemWPF.Utils
{
    internal static class Changes
    {
        private static int AddCounter { get; set; } = 0;
        private static int UpdateCounter { get; set; } = 0;
        private static int DeleteCounter { get; set; } = 0;
        private static int SortCounter { get; set; } = 0;

        public static void IncrementAddCounter() => AddCounter++;
        public static void IncrementUpdateCounter() => UpdateCounter++;
        public static void IncrementDeleteCounter() => DeleteCounter++;
        public static void IncrementSortCounter() => SortCounter = 1;

        public static Tuple<int, int, int, int> GetChanges()
        {
            var changes = Tuple.Create(AddCounter, UpdateCounter, DeleteCounter, SortCounter);
            return changes;
        }

        public static int GetChangeSum()
        {
            return AddCounter + UpdateCounter + DeleteCounter + SortCounter;
        }

        public static void ResetCounters()
        {
            AddCounter = 0;
            UpdateCounter = 0;
            DeleteCounter = 0;
            SortCounter = 0;
        }
    }
}
