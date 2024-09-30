using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        private Animator animator;
        private Rigidbody2D rigidBody2D;
        private Vector2 dir;
        private bool _activeUI = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidBody2D = GetComponent<Rigidbody2D>();
            HUDManager.OnUIOpen += DisablePlayerControls;
            HUDManager.OnUIClose += EnablePlayerControls;
        }

        private void Update()
        {
            if (_activeUI)
            {
                Debug.Log("TopDownCharacterController: a UI is Active");
                return;
            }

            dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
            }
                
            if(dir.x != 0 || dir.y != 0)
            {
                dir.Normalize();
                animator.SetFloat("X", dir.x);
                animator.SetFloat("Y", dir.y);
                animator.SetBool("PlayerMoving", true);
            }
            else
            {
                animator.SetBool("PlayerMoving", false);
            }

            rigidBody2D.MovePosition(rigidBody2D.position + dir * speed * Time.deltaTime);
        }

        private void DisablePlayerControls()
        {
            _activeUI = false;
        }

        private void EnablePlayerControls()
        {
            _activeUI = true;
        }

        private void OnDestroy()
        {
            HUDManager.OnUIOpen -= DisablePlayerControls;
            HUDManager.OnUIClose -= EnablePlayerControls;
        }
    }
}
