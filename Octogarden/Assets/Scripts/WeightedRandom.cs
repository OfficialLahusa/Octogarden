using System;
using System.Collections.Generic;

public class WeightedRandom<T>
{
    private List<(T result, float weight)> _entries;

    private Random _random;

    public WeightedRandom()
    {
        _entries = new List<(T result, float weight)>();
        _random = new Random();
    }

    public void Add(T result, float weight)
    {
        if (weight < 0)
        {
            throw new ArgumentException("Weight cannot be negative.", nameof(weight));
        }
        _entries.Add((result, weight));
    }

    public void Remove(T result)
    {
        _entries.RemoveAll(entry => EqualityComparer<T>.Default.Equals(entry.result, result));
    }

    public void Clear()
    {
        _entries.Clear();
    }

    public bool IsEmpty()
    {
        return _entries.Count == 0 || GetTotalWeight() <= 0f;
    }

    private float GetTotalWeight()
    {
        float totalWeight = 0;
        foreach (var entry in _entries)
        {
            totalWeight += entry.weight;
        }
        return totalWeight;
    }

    public T Draw()
    {
        if (_entries.Count == 0)
            throw new InvalidOperationException("No entries to draw from.");

        // Calculate the total weight of all entries
        float totalWeight = GetTotalWeight();

        float randomValue = (float)_random.NextDouble() * totalWeight;

        float cumulativeWeight = 0;
        foreach (var entry in _entries)
        {
            cumulativeWeight += entry.weight;

            // If random weight falls into range of this entry's weight, return the result
            if (randomValue < cumulativeWeight)
            {
                return entry.result;
            }
        }

        // This should never happen if the weights are set up correctly
        throw new InvalidOperationException("Failed to draw a result.");
    }
}
