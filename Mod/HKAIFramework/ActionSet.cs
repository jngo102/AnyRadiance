using System;
using System.Linq;

namespace HKAIFramework
{

    public class ActionSet
    {
        public Tuple<ActionSequence, float, int>[] TrackedSequences;
        public ActionSequence CurrentSequence { get; private set; }

        public ActionSequence GetSequence(string sequenceName)
        {
            var sequence = TrackedSequences.Select(x => x.Item1).FirstOrDefault(sequence => sequence.Name == sequenceName);
            CurrentSequence = sequence;
            return sequence;
        }

        public ActionSequence RandomSequence()
        {
            var sequence = RandomUtil.WeightedRandom(TrackedSequences);
            CurrentSequence = sequence;
            return sequence;
        }
    }
}