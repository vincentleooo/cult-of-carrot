using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // Player has not selected the node yet, so he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                {
                    SendPlayerToNode(mapNode);
                }

                else
                {
                    Debug.Log("Selected node cannot be accessed");
                }
            }

            else
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                {
                    SendPlayerToNode(mapNode);
                }

                else
                {
                    Debug.Log("Selected node cannot be accessed");
                }
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void EnterNode(MapNode mapNode)
        {
            // We have access to blueprint name here as well
            Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);

            // Load appropriate scene with context based on nodeType,
            // or show appropriate GUI over the map
            // If you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false
            switch (mapNode.Node.nodeType)
            {
                case (NodeType.Tutorial):
                    SceneManager.LoadScene("InductionTest");
                    break;

                case (NodeType.MinorEnemy):
                    SceneManager.LoadScene("InductionTest");
                    break;

                case (NodeType.EliteEnemy):
                    break;

                case (NodeType.Rest):
                    SceneManager.LoadScene("RestNode");
                    break;

                case (NodeType.Shop):
                    SceneManager.LoadScene("ShopNode");
                    break;

                case (NodeType.Boss):
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}