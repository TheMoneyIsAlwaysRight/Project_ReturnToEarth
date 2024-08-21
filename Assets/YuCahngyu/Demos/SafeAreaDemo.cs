using UnityEngine;
using System;

namespace yu
{
    public class SafeAreaDemo : MonoBehaviour
    {
        [SerializeField] KeyCode KeySafeArea = KeyCode.A;
        SafeAreaAsset.SimDevice[] Sims;
        int SimIdx;

        void Awake ()
        {
            if (!Application.isEditor)
                Destroy (this);

            Sims = (SafeAreaAsset.SimDevice[])Enum.GetValues (typeof (SafeAreaAsset.SimDevice));
        }

        void Update ()
        {
            if (Input.GetKeyDown (KeySafeArea))
                ToggleSafeArea ();
        }

        /// <summary>
        /// Toggle the safe area simulation device.
        /// </summary>
        void ToggleSafeArea ()
        {
            SimIdx++;

            if (SimIdx >= Sims.Length)
                SimIdx = 0;

            SafeAreaAsset.Sim = Sims[SimIdx];
            Debug.LogFormat ("Switched to sim device {0} with debug key '{1}'", Sims[SimIdx], KeySafeArea);
        }
    }
}
