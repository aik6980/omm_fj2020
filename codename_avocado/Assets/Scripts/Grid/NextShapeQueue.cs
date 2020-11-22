using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextShapeQueue : MonoSingleton<NextShapeQueue>
{
    public bool randomized = true;

    [Range(1, 20)]
    public int queueItemsDisplayed = 3;

    public Unfold unfoldScript;
    private List<int> nextshapeQueue;
    private List<int> totalShapeQueue;

    public int PeekNext()
    {
        return nextshapeQueue[0];
    }

    public int[] PeekAll()
    {
        return nextshapeQueue.ToArray();
    }

    public int PopNext()
    {
        int next = PeekNext();

        nextshapeQueue.RemoveAt(0);
        EnqueueBack();

        return next;
    }

    // Start is called before the first frame update
    void Awake()
    {
        nextshapeQueue = new List<int>(queueItemsDisplayed);
        totalShapeQueue = new List<int>(0);

        for (int i = 0; i < queueItemsDisplayed; ++i)
        {
            EnqueueBack();
        }
    }

    private void EnqueueBack()
    {
        if (totalShapeQueue.Count == 0)
        { 
            int numThings = unfoldScript.UnfoldShapeDefinitionAmount();
            totalShapeQueue = Enumerable.Range(0, numThings).ToList();
            Shuffle(totalShapeQueue);
        }
        nextshapeQueue.Add(totalShapeQueue[totalShapeQueue.Count-1]);
        totalShapeQueue.RemoveAt(totalShapeQueue.Count-1);
    }

    private static void Shuffle<T>(List<T> array)
    {
        int n = array.Count;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
