using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Singleton<T> where T : class
{
    private static T _instance;
    private static System.Object _lock = new System.Object();

    public static T Instance {
        get {
            if (_instance == null) {
                lock (_lock) {
                    if (_instance == null)
                        _instance = Activator.CreateInstance(typeof(T), true) as T;
                }
            }
            return _instance;
        }
    }
}
