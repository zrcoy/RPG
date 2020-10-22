using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            
        }

        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            yield return FadeIn(1f);
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                float num_frames = time / Time.deltaTime;
                float alphaChangesPerFrame = 1 / num_frames;
                canvasGroup.alpha = Mathf.Clamp(canvasGroup.alpha += alphaChangesPerFrame, 0, 1);
                yield return null;
            }
        }
        
        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                float num_frames = time / Time.deltaTime;
                float alphaChangesPerFrame = 1 / num_frames;
                canvasGroup.alpha = Mathf.Clamp(canvasGroup.alpha -= alphaChangesPerFrame, 0, 1);
                yield return null;
            }
        }
    }
}
