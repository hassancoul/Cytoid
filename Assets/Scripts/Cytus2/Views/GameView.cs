﻿using System.Collections;
using Cytus2.Controllers;
using Cytus2.Models;
using DoozyUI;
using QuickEngine.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cytus2.Views
{
    public class GameView
    {
        public readonly Game Game;

        private GameObject background;
        private AlphaMask backgroundOverlayMask;
        private AlphaMask sceneTransitionMask;
        private Text titleText;

        public GameView(Game game)
        {
            Game = game;
        }

        public void OnStart()
        {
            var level = CytoidApplication.CurrentLevel;

            background = GameObject.FindGameObjectWithTag("Background");
            backgroundOverlayMask = GameObject.Find("BackgroundOverlayMask").GetComponent<AlphaMask>();
            backgroundOverlayMask.max = (int) PlayerPrefs.GetFloat("background dim", 17) / 20.0f;
            backgroundOverlayMask.willFadeIn = true;

            var canvas = backgroundOverlayMask.GetComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingLayerName = "GameBackground";
            canvas.sortingOrder = 1;

            var gameObject = GameObject.Find("SceneTransitionMask");
            if (gameObject != null)
            {
                sceneTransitionMask = GameObject.Find("SceneTransitionMask").GetComponent<AlphaMask>();
            }

            gameObject = GameObject.Find("TitleText");

            if (gameObject != null)
            {
                titleText = gameObject.GetComponent<Text>();
                titleText.text = level.title;
            }

            if (background != null)
            {
                canvas = background.GetComponent<Canvas>() == null
                    ? background.AddComponent<Canvas>()
                    : background.GetComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingLayerName = "GameBackground";
                canvas.sortingOrder = 0;
            }

            if (DisplayDifficultyView.Instance != null)
            {
                DisplayDifficultyView.Instance.SetDifficulty(level,
                    level.charts.Find(it => it.type == CytoidApplication.CurrentChartType));
            }
        }

        public void OnPause()
        {
            UIManager.ShowUiElement("PauseBackground", "Game", true);
            UIManager.ShowUiElement("PauseRoot", "Game", true);
        }

        public void OnUnpause()
        {
        }

        public void OnProceedToResult()
        {
            UIManager.HideUiElement("ScoreText", "Game");
            UIManager.HideUiElement("ComboText", "Game");
            UIManager.HideUiElement("TpText", "Game");
            UIManager.HideUiElement("TitleText", "Game");
            UIManager.HideUiElement("Mask", "Game");
            UIManager.HideUiElement("HP", "Game");
            UIManager.HideUiElement("PauseButton", "Game");
        }

        public IEnumerator ReturnToLevelSelectionCoroutine()
        {
            if (sceneTransitionMask != null)
            {
                sceneTransitionMask.willFadeIn = true;
                sceneTransitionMask.GetComponent<Image>().raycastTarget = true; // Block button interactions
                while (sceneTransitionMask.IsFading) yield return null;
            }

            SceneManager.LoadScene("LevelSelection");
        }
    }
}