// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using System.Collections;
using HoloToolkit.Examples.Prototyping;
using System;

namespace HoloToolkit.Examples.InteractiveElements
{
    /// <summary>
    /// updates the button colors, position and scale based on the button theme
    /// </summary>
    public class ButtonThemeWidget : InteractiveThemeWidget
    {
        [Tooltip("tag for the color button theme")]
        public string ColorThemeTag = "defaultColor";

        [Tooltip("tag for the position button theme")]
        public string PositionThemeTag = "defaultPosition";

        [Tooltip("tag for the scale button theme")]
        public string ScaleThemeTag = "defaultScale";

        [Tooltip("Color transition animation component: optional")]
        public ColorTransition ColorBlender;

        [Tooltip("position animation component: optional")]
        public MoveToPosition MovePosition;

        [Tooltip("scale animation component: optional")]
        public ScaleToValue ScaleSize;

        // themes
        private ColorInteractiveTheme mColorTheme;
        private Vector3InteractiveTheme mPositionTheme;
        private Vector3InteractiveTheme mScaleTheme;

        // material
        private Material mMaterial;

        private string mCheckColorThemeTag = "";
        private string mCheckPositionThemeTag = "";
        private string mCheckScaleThemeTag = "";

        /// <summary>
        /// Get animation components
        /// </summary>
        private void Awake()
        {
            if (ColorBlender == null)
            {
                ColorBlender = GetComponent<ColorTransition>();
            }

            if (MovePosition == null)
            {
                MovePosition = GetComponent<MoveToPosition>();
            }

            if (ScaleSize == null)
            {
                ScaleSize = GetComponent<ScaleToValue>();
            }

            // get renderer
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                mMaterial = renderer.material;
            }
        }

        private void Start()
        {
            SetTheme();
            RefreshIfNeeded();
        }

        public override void SetTheme()
        {
            if (ColorThemeTag != "")
            {
                mColorTheme = GetColorTheme(ColorThemeTag);
                mCheckColorThemeTag = ColorThemeTag;
            }

            if (PositionThemeTag != "")
            {
                mPositionTheme = GetVector3Theme(PositionThemeTag);
                mCheckPositionThemeTag = PositionThemeTag;
            }

            if (ScaleThemeTag != "")
            {
                mScaleTheme = GetVector3Theme(ScaleThemeTag);
                mCheckScaleThemeTag = ScaleThemeTag;
            }
        }

        /// <summary>
        /// set states or start animations
        /// </summary>
        /// <param name="state"></param>
        public override void SetState(Interactive.ButtonStateEnum state)
        {
            base.SetState(state);
            
            if (mColorTheme != null)
            {
                if (ColorBlender != null)
                {
                    ColorBlender.StartTransition(mColorTheme.GetThemeValue(state));
                }
                else if(mMaterial != null)
                {
                    mMaterial.color = mColorTheme.GetThemeValue(state);
                }
            }
            
            if (mPositionTheme != null)
            {
                if (MovePosition != null)
                {
                    MovePosition.TargetValue = mPositionTheme.GetThemeValue(state);
                    MovePosition.StartRunning();
                }
                else
                {
                    transform.localPosition = mPositionTheme.GetThemeValue(state);
                }
            }

            if (mScaleTheme != null)
            {
                if (ScaleSize != null)
                {
                    ScaleSize.TargetValue = mScaleTheme.GetThemeValue(state);
                    ScaleSize.StartRunning();
                }
                else
                {
                    transform.localScale = mScaleTheme.GetThemeValue(state);
                }
            }
        }

        private void Update()
        {
            if(!mCheckScaleThemeTag.Equals(ScaleThemeTag) || !mCheckPositionThemeTag.Equals(PositionThemeTag) || !mCheckColorThemeTag.Equals(ColorThemeTag))
            {
                SetTheme();
                RefreshIfNeeded();
            }
        }

        /// <summary>
        /// clean up if material was created dynamically
        /// </summary>
        private void OnDestroy()
        {
            if(mMaterial != null)
            {
                GameObject.Destroy(mMaterial);
            }
        }
    }
}
