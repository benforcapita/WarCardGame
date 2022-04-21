using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EncapsulatedMonoBehaviorComponent : MonoBehaviour
{
   //list of IDisposable objects
   private readonly List<IDisposable> _disposables = new List<IDisposable>();
   
   //add subscription to the list
   protected void AddSubscription(IDisposable disposable)
   {
      _disposables.Add(disposable);
   }
   
   //clean all disposables on disable
   private void OnDisable()
   {
      foreach (var disposable in _disposables)
      {
         disposable.Dispose();
      }
   }
}
