using Entities;
using Services;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class LocalInstaller : MonoInstaller
    {
        [SerializeField] private WalkerInteractDoor walkerInteractDoor;

        public override void InstallBindings()
        {
            BindWalkerInteractDoor();
            BindServices();
        }

        private void BindServices()
        {
            BindWalkersService();
            BindRoomsService();
            BindInteractDoorService();
        }

        private void BindWalkersService()
        {
            Container
                .Bind<WalkersService>()
                .AsSingle();
        }

        private void BindRoomsService()
        {
            Container
                .Bind<RoomsService>()
                .AsSingle();
        }

        private void BindInteractDoorService()
        {
            Container
                .Bind<InteractDoorsService>()
                .AsSingle();
        }

        private void BindWalkerInteractDoor()
        {
            Container
                .Bind<WalkerInteractDoor>()
                .FromInstance(walkerInteractDoor)
                .AsSingle();
        }
    }
}