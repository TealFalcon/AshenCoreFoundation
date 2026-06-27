using UnityEngine;


namespace AshenCore.Core
{
    [CreateAssetMenu(fileName = "ACPoolDefinition", menuName = "AshenCore/Pool Definition")]
    public class ACPoolDefinition : ScriptableObject
    {
        public int Id;
        public int PoolSize;
        public GameObject Prefab;
        public int InitialSize;
        public int MaxSize;
        public bool Expandable = true;
        public ACSpawnContainerType ContainerType;
    }
    
}