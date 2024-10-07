using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using Cinemachine;
using UnityEngine;

public class ActiveStateTrigger : MonoBehaviour
{
   [SerializeField]
   private Behaviour _behaviour;
   [SerializeField]
   private GameObject _gameObject;
   [SerializeField]
   private bool _activeState = false;

   public void SetGameObject(GameObject gameObject)
   {
      _gameObject = gameObject;
   }

   public void SetBehaviour(Behaviour behaviour)
   {
      _behaviour = behaviour;
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (_behaviour != null)
      {
         _behaviour.enabled = _activeState;
      }
      else if (_gameObject != null)
      {
         _gameObject.SetActive(_activeState); 
      }

      Debug.Log("ActiveStateTrigger: Activated!");
   }
}
