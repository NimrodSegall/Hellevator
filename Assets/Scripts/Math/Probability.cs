using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Math
{
    public static class Probability
    {
        /// <summary>
        ///Selects a random result from a weighted distribution.
        /// <paramref name="possibleResults"/>: All possible results
        /// <paramref name="distribution"/>: Weights for each result
        /// </summary>
        public static T GetResultFromDistribution<T>(IEnumerable<T> possibleResults, IEnumerable<float> distribution)
        {
            var possibleResultsEnumed = possibleResults.ToArray();
            var distributionEnumed = distribution.ToArray();

            #region InputChecks
            if (possibleResultsEnumed.Length != distributionEnumed.Length)
            {
                Debug.LogError($"{nameof(possibleResults)}'s size != {nameof(distribution)}'s size.");
            }
            if (distributionEnumed.Select(x => x < 0 || x > 1).Count() > 0)
            {
                Debug.LogError($"All values in {nameof(distribution)} must be between 0 and 1 (inclusive)");
            }
            if (distribution.Sum() != 1f)
            {
                Debug.LogError($"All values in {nameof(distribution)} must sum up to 1.0");
            }
            #endregion

            var probabilityResult = UnityEngine.Random.Range(0f, 1f);
            var sum = 0f;

            for (int i = 0; i < possibleResultsEnumed.Length; i++)
            {
                sum += distributionEnumed[i];
                if (probabilityResult <= sum)
                {
                    return possibleResultsEnumed[i];
                }
            }
            throw new ArgumentOutOfRangeException($"Could not find result in {nameof(possibleResults)}");
        }
    }
}