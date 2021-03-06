﻿using RPG.Saving;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{

    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        [SerializeField] float timeForGameStartFadeOut = 2f;

        //private void Awake()
        //{
        //    StartCoroutine(LoadLastScene());
        //}

        //IEnumerator LoadLastScene()
        //{
        //    yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);

        //    Fader fader = FindObjectOfType<Fader>();
        //    fader.FadeOutImmediately();
        //    yield return fader.FadeIn(timeForGameStartFadeOut);
        //}

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediately();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(timeForGameStartFadeOut);
        }



        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }


        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }

}