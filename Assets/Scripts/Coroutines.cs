using System;
using System.Collections;
using UnityEngine;

public static class Coroutines
{
    public static IEnumerator Action(Action _Action)
    {
        _Action?.Invoke();
        yield break;
    }

    public static IEnumerator WaitWhile(Action _Action, Func<bool> _Predicate)
    {
        if (_Action == null || _Predicate == null)
            yield break;
        ;
        yield return new WaitWhile(_Predicate);
        _Action();
    }

    public static IEnumerator Delay(Action _Action, float _Delay)
    {
        if (_Delay > float.Epsilon)
            yield return new WaitForSeconds(_Delay);
        _Action?.Invoke();
    }
}