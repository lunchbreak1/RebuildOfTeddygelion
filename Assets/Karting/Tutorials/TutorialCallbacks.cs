﻿using UnityEngine;
//using Unity.Tutorials.Core.Editor;
using UnityEditor;

namespace Unity.Tutorials
{
    /// <summary>
    /// Implement your Tutorial callbacks here.
    /// </summary>
    /*public class TutorialCallbacks : ScriptableObject
    {
        public FutureObjectReference futureJumpInstance = default;
        public FutureObjectReference futureBotInstance = default;

        /// <summary>
        /// Keeps the Jump selected during a tutorial. 
        /// </summary>
        public void KeepJumpSelected()
        {
            SelectSpawnedGameObject(futureJumpInstance);
        }

        /// <summary>
        /// Keeps the Bot selected during a tutorial. 
        /// </summary>
        public void KeepBotSelected()
        {
            SelectSpawnedGameObject(futureBotInstance);
        }


        /// <summary>
        /// Selects a GameObject in the scene, marking it as the active object for selection
        /// </summary>
        /// <param name="futureObjectReference"></param>
        public void SelectSpawnedGameObject(FutureObjectReference futureObjectReference)
        {
            if (futureObjectReference.SceneObjectReference == null) { return; }
            Selection.activeObject = futureObjectReference.SceneObjectReference.ReferencedObjectAsGameObject;
        }

        public void SelectMoveTool()
        {
            Tools.current = Tool.Move;
        }

        public void SelectRotateTool()
        {
            Tools.current = Tool.Rotate;
        }

        public void StartTutorial(Tutorial tutorial)
        {
            TutorialManager.Instance.StartTutorial(tutorial);
        }
    }*/
}