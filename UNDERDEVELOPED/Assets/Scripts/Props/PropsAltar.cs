using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//when player is on top on altar, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<GameObject> runes;
        public float lerpSpeed;
        private Color currentColor, targetColor, currentGlow, targetGlow;

        private void OnTriggerEnter2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 1);
            targetGlow = new Color(.42f, .78f, .85f, 1);
            //targetGlow = new Color(108, 199, 217, 1);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 1);
            targetGlow = new Color(.42f, .78f, .85f, 1); 
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 0);
            targetGlow = new Color(.42f, .78f, .85f, 0);
            //targetGlow = new Color(108, 199, 217, 0);
        }

        private void Update()
        {
            currentColor = Color.Lerp(currentColor, targetColor, lerpSpeed * Time.deltaTime);
            currentGlow = Color.Lerp(currentGlow, targetGlow, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.GetComponent<SpriteRenderer>().color = currentColor;
                r.GetComponent<Light2D>().color = currentGlow;
            }
        }
    }
}
