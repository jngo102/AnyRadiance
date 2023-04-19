using System;
using System.Linq;

namespace HKAIFramework
{
    /// <summary>
    /// Contains a set of action sequences to execute.
    /// </summary>
    public class ActionSet
    {
        /// <summary>
        /// An array of sequences whose weights and number of repeats are tracked.
        /// </summary>
        public Tuple<ActionSequence, float, int>[] TrackedSequences;
        
        /// <summary>
        /// The current sequence being executed.
        /// </summary>
        public ActionSequence CurrentSequence { get; private set; }

        /// <summary>
        /// Gets a sequence by name.
        /// </summary>
        /// <param name="sequenceName">The name of the sequence to get</param>
        /// <returns>The action sequence whose name matches the input query</returns>
        public ActionSequence GetSequence(string sequenceName)
        {
            var sequence = TrackedSequences.Select(x => x.Item1).FirstOrDefault(sequence => sequence.Name == sequenceName);
            CurrentSequence = sequence;
            return sequence;
        }

        /// <summary>
        /// Gets a random sequence from the set of tracked sequences.
        /// </summary>
        /// <returns>The randomly fetched action sequence</returns>
        public ActionSequence RandomSequence()
        {
            var sequence = RandomUtil.WeightedRandom(TrackedSequences);
            CurrentSequence = sequence;
            return sequence;
        }
    }
}