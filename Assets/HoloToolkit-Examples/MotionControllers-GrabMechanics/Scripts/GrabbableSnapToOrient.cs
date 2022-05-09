// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Examples.Grabbables
{
    /// <summary>
    /// This type of grab uses a parent child relationship and also immediately orients the child's forward to the parent's forward position
    /// </summary>
    public class GrabbableSnapToOrient : BaseGrabbable
    {
        protected override void StartGrab(BaseGrabber grabber)
        {
            base.StartGrab(grabber);
            transform.SetParent(grabber.GrabHandle);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<TimeBody>().gf = 1;
            transform.rotation = transform.parent.rotation;
        }

        protected override void EndGrab()
        {
            transform.SetParent(null);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<TimeBody>().gf = 0;
            base.EndGrab();
        }
    }
}
