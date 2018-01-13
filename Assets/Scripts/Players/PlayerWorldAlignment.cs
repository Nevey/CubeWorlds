using CCore.CubeWorlds.Worlds.WorldTiles;
using DG.Tweening;
using UnityEngine;

namespace CCore.CubeWorlds.Players
{
    [RequireComponent(typeof(Player))]
    public class PlayerWorldAlignment : MonoBehaviour, IPlayerEnabler
    {
        [SerializeField] private bool drawDebugLines = true;

        // TODO: Get dynamically from available worlds
        [SerializeField] private float raycastLength = 1f;

        [SerializeField] private Transform playerRotationHelperPrefab;

        [SerializeField] private float smoothRotationTime = 0.3f;

        private Transform playerRotationHelper;

        private Player player;

        private float currentDistance;

        private WorldTileSurface newWorldTileSurface;

        private SurfaceRotation currentRotation;

        private void FixedUpdate()
        {
            FindAndApplySurfaceRotation();
        }

        private void CreatePlayerRotationHelper()
        {
            playerRotationHelper = Instantiate(playerRotationHelperPrefab);
        }

        private void DestroyPlayerRotationHelper()
        {
            Destroy(playerRotationHelper);
        }

        private void FindAndApplySurfaceRotation(bool instantRotation = false)
        {
            currentDistance = raycastLength;

            // Forward/Backward
            FindWorldTileSurface(transform.forward);
            FindWorldTileSurface(-transform.forward);

            // Right/Left
            FindWorldTileSurface(transform.right);
            FindWorldTileSurface(-transform.right);

            // Up/Down
            FindWorldTileSurface(transform.up);
            FindWorldTileSurface(-transform.up);

            // If a new world tile surface was found and doesn't have the
            // current rotation, apply the new rotation
            if (newWorldTileSurface != null)
            {
                if (newWorldTileSurface.SurfaceRotation != currentRotation)
                {
                    ApplyRotation(instantRotation);
                }

                newWorldTileSurface = null;
            }
        }

        private void FindWorldTileSurface(Vector3 direction)
        {
            Ray ray = new Ray(transform.position, direction);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastLength))
            {
                WorldTileSurface worldTileSurface = hit.collider.gameObject.GetComponent<WorldTileSurface>();

                if (worldTileSurface != null)
                {
                    float distance = Vector3.Distance(transform.position, worldTileSurface.transform.position);

                    if (distance < currentDistance)
                    {
                        newWorldTileSurface = worldTileSurface;

                        currentDistance = distance;
                    }
                }
            }

            if (drawDebugLines)
            {
                Debug.DrawLine(ray.origin, ray.origin + (ray.direction * raycastLength), Color.green);
            }
        }

        private void ApplyRotation(bool instantRotation)
        {
            currentRotation = newWorldTileSurface.SurfaceRotation;

            Vector3 rotationEuler = 
                	PlayerSurfaceRotationConverter.
                	SurfaceRotationToWorldRotation(newWorldTileSurface.SurfaceRotation);
            
            playerRotationHelper.transform.rotation = Quaternion.identity;

            playerRotationHelper.Rotate(rotationEuler, Space.Self);

            if (instantRotation)
            {
                player.transform.localRotation = playerRotationHelper.transform.localRotation;
            }
            else
            {
                transform.DOLocalRotateQuaternion(playerRotationHelper.transform.localRotation, smoothRotationTime).SetEase(Ease.OutBack);
            }

            Log("Applied rotation on surface: " + newWorldTileSurface.SurfaceRotation);

            Log("And the local rotation is: " + playerRotationHelper.transform.localEulerAngles);
        }

        public void OnPlayerEnabled()
        {
            player = GetComponent<Player>();

            CreatePlayerRotationHelper();

            FindAndApplySurfaceRotation(true);
        }

        public void OnPlayerDisabled()
        {
            DestroyPlayerRotationHelper();
        }
    }
}