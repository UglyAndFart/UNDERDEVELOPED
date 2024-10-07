using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //let camera follow target
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float lerpSpeed = 1.0f;

        private Vector3 offset;

        private Vector3 targetPos;

        private void Start()
        {
            if (target == null)
            {
                target = CharacterPrefabLoader._instance.GetCurrentCharacter().transform;
            }
            
            // offset = transform.position - target.position;
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            // targetPos = target.position + offset;
            targetPos = target.position;
            targetPos = new Vector3 (targetPos.x, targetPos.y + 3.5f, transform.position.z);
            // targetPos = target.position;
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }

    }
}

//-69.7 - -73.31223
