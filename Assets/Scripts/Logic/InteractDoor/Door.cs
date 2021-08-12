using DG.Tweening;
using Services;
using UnityEngine;
using Zenject;

namespace Logic.InteractDoor
{
    public class Door : MonoBehaviour
    {
        private const float DelayBeforeClosing = 3f;

        [SerializeField] private float openingDuration;
        [SerializeField] private DoorStatus status = DoorStatus.Close;

        private Sequence openAnimation;
        private Sequence closeAnimation;
        
        public float OpeningDuration => openingDuration;
        public DoorStatus Status => status;
        
        [Inject]
        private void Construct(InteractDoorsService interactDoorsService)
        {
            interactDoorsService.RegisterDoor(this);
        }

        private void Awake() =>
            CreateAnimations();

        public void Open()
        {
            if (openAnimation.IsPlaying())
                return;

            status = DoorStatus.Opening;
            openAnimation.Restart();
        }

        public Vector3 Position() =>
            transform.position;

        private void CreateAnimations()
        {
            openAnimation = DOTween.Sequence();
            closeAnimation = DOTween.Sequence();

            openAnimation.SetAutoKill(false);
            closeAnimation.SetAutoKill(false);

            var position = transform.position;
            var vectorUp = new Vector3(0, 1.2f, 0);
            
            openAnimation
                .Append(transform.DOMove(position + vectorUp, openingDuration))
                .OnComplete(() =>
                {
                    status = DoorStatus.Open;
                    closeAnimation.Restart();
                });

            closeAnimation
                .Append(transform.DOMove(position, openingDuration))
                .SetDelay(DelayBeforeClosing)
                .OnComplete(() => { status = DoorStatus.Close; });

            openAnimation.Pause();
            closeAnimation.Pause();
        }
    }
}