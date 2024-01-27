using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class Picture : MonoBehaviour
    {
        [SerializeField]
        private Texture _defaultTexture;
        [SerializeField]
        private Texture _revealedTexture;
        [SerializeField]
        private MeshRenderer _meshRenderer;

        private bool _isRevealed;
        private Material _createdMaterial;

        private Item _item;

        private void Awake()
        {
            _item = GetComponent<Item>();
        }

        private void OnEnable()
        {
            _isRevealed = false;
            _meshRenderer.material.SetTexture("_BaseMap", _defaultTexture);
        }

        public void SwapImage()
        {
            _isRevealed = !_isRevealed;

            if (_isRevealed)
            { 
                _meshRenderer.material.SetTexture("_BaseMap", _revealedTexture);
                _meshRenderer.material.DisableKeyword("_EMISSION");
                MakeGrabble();
            }
            else
            {
                MakeUngrabble();
                _meshRenderer.material.EnableKeyword("_EMISSION");
                _meshRenderer.material.SetTexture("_BaseMap", _defaultTexture);
            }
        }

        public void MakeUngrabble()
        {
            _item.CanBeGrabbed = false;
        }

        public void MakeGrabble()
        {
            _item.CanBeGrabbed = true;
        }
    }
}
