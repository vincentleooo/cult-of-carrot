using UnityEngine;

namespace Map
{
    public enum NodeType
    {
        Tutorial,
        MinorEnemy,
        EliteEnemy,
        Rest,
        Shop,
        Boss
    }
}

namespace Map
{
    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        public Sprite sprite;
        public NodeType nodeType;
    }
}