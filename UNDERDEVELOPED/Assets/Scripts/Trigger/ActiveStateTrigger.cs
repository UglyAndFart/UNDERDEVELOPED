using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStateTrigger : MonoBehaviour
{
   [SerializeField]
   private GameObject _gameObject;
   [SerializeField]
   private bool _activeState;

   private void OnTriggerEnter2D(Collider2D col)
   {
      _gameObject.SetActive(_gameObject);
   }
}
