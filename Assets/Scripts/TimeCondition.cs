using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeCondition
{
    public enum Comparison
    {
        GreaterOrEqual,
        Equal,
        LessOrEqual,
        Children
    }

    public enum Chain
    {
        Start,
        And,
        Or
    }

    public Chain chain;
    public Comparison comparison;
    public int time;
    public TimeCondition[] children = new TimeCondition[0];

    public bool IsTrue(int currentTime)
    {
        switch (comparison)
        {
            case Comparison.GreaterOrEqual: return currentTime >= time;
            case Comparison.Equal: return currentTime == time;
            case Comparison.LessOrEqual: return currentTime <= time;
            case Comparison.Children:
            {
                bool isTrue = false;

                for (int i = 0; i < children.Length; i++)
                {
                    switch (children[i].chain)
                    {
                        case Chain.Start: isTrue = children[i].IsTrue(currentTime); break;
                        case Chain.And: isTrue &= children[i].IsTrue(currentTime); break;
                        case Chain.Or: isTrue |= children[i].IsTrue(currentTime); break;
                    }
                }

                return isTrue;
            }
        }

        return false; // shouldn't happen
    }
}