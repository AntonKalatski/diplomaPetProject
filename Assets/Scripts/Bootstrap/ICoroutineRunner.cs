using System.Collections;
using UnityEngine;

namespace Bootstrap
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}