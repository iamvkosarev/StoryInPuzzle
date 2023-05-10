using System.Collections;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
    }
}