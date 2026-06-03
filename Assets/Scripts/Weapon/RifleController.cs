using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace VRGunShoot.Weapon
{
    /// <summary>
    /// Thin controller for the MCX rifle. It exposes named weapon sockets and grab state
    /// for later sighting, ballistics, and recoil tasks.
    /// </summary>
    [RequireComponent(typeof(XRGrabInteractable))]
    public class RifleController : MonoBehaviour
    {
        [Header("Mount Points")]
        [SerializeField] private Transform muzzlePoint;
        [SerializeField] private Transform sightRear;
        [SerializeField] private Transform sightFront;
        [SerializeField] private Transform gripPrimary;
        [SerializeField] private Transform gripSecondary;

        private XRGrabInteractable _grabInteractable;

        public Transform MuzzlePoint => muzzlePoint;
        public Transform SightRear => sightRear;
        public Transform SightFront => sightFront;
        public Transform GripPrimary => gripPrimary;
        public Transform GripSecondary => gripSecondary;

        /// <summary>Whether the rifle is currently selected by at least one interactor.</summary>
        public bool IsHeld { get; private set; }

        /// <summary>Whether the rifle is currently selected by exactly two interactors.</summary>
        public bool IsTwoHanded { get; private set; }

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
        }

        private void OnEnable()
        {
            _grabInteractable ??= GetComponent<XRGrabInteractable>();
            _grabInteractable.selectEntered.AddListener(HandleSelectEntered);
            _grabInteractable.selectExited.AddListener(HandleSelectExited);
            RefreshGrabState();
        }

        private void OnDisable()
        {
            if (_grabInteractable == null)
            {
                return;
            }

            _grabInteractable.selectEntered.RemoveListener(HandleSelectEntered);
            _grabInteractable.selectExited.RemoveListener(HandleSelectExited);
            IsHeld = false;
            IsTwoHanded = false;
        }

        public void OnGrabbed()
        {
            RefreshGrabState();
        }

        public void OnReleased()
        {
            RefreshGrabState();
        }

        private void HandleSelectEntered(SelectEnterEventArgs args)
        {
            OnGrabbed();
        }

        private void HandleSelectExited(SelectExitEventArgs args)
        {
            OnReleased();
        }

        private void RefreshGrabState()
        {
            _grabInteractable ??= GetComponent<XRGrabInteractable>();
            var selectCount = _grabInteractable == null ? 0 : _grabInteractable.interactorsSelecting.Count;
            IsHeld = selectCount > 0;
            IsTwoHanded = selectCount == 2;
        }
    }
}
