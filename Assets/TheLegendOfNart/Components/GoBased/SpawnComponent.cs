using UnityEngine;

namespace TheLegendsOfNart.Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Space _space;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            //Debug.Log("Spawn");
            GameObject instance;
            if (_space == Space.World)
            {
                instance = Instantiate(_prefab, _target.position, Quaternion.identity);
                instance.transform.localScale = _target.lossyScale;
            }
            else
            {
                instance = Instantiate(_prefab, _target.position, Quaternion.identity, _target);
            }
            instance.SetActive(true);
        }
    }
}
