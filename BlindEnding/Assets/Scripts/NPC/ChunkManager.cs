using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public int chunkSize = 5;
    public string ObserveTag;

    [HideInInspector]
    public Dictionary<Vector2Int, List<Transform>> chunks = new Dictionary<Vector2Int, List<Transform>>();

    private Transform[] observedTransforms;

    void Start()
    {
        GetObserve();
    }

    void Update()
    {
        CalculateChunks();
    }

    void GetObserve()
    {
        GameObject[] observedObjects = GameObject.FindGameObjectsWithTag(ObserveTag);

        observedTransforms = new Transform[observedObjects.Length];

        for (int i = 0; i < observedObjects.Length; i++)
        {
            observedTransforms[i] = observedObjects[i].transform;
        }
    }

    public void CalculateChunks()
    {
        chunks.Clear();
        foreach (Transform observedTransform in observedTransforms)
        {
            Vector2Int chunkToAdd = ChunkIndexFromPosition(observedTransform.position);
            if (chunks.ContainsKey(chunkToAdd))
            {
                chunks[chunkToAdd].Add(observedTransform);
            }
            else
            {
                chunks.Add(chunkToAdd, new List<Transform> { observedTransform });
            }
        }
    }

    Vector2Int ChunkIndexFromPosition(Vector3 pos)
    {
        return Vector2Int.FloorToInt(pos / chunkSize);
    }

    public List<Transform> GetChunk(Vector2 pos)
    {
        List<Transform> output = new List<Transform>();
        Vector2Int centerChunkIndex = ChunkIndexFromPosition(pos);

        for(int y = -1; y <= 1; y++){
            for(int x = -1; x <= 1; x++){
                Vector2Int curChunkIndex = centerChunkIndex + new Vector2Int(x, y);

                if(chunks.ContainsKey(curChunkIndex)){
                    List<Transform> curChunk = chunks[curChunkIndex];
                    output.AddRange(curChunk);
                }
            }
        }

        return output;
    }
}
