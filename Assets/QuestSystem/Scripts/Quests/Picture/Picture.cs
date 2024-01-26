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
        private Material _material;

        private bool _isRevealed;
        private Renderer _renderer;
        private Material _createdMaterial;

        private void Awake()
        {
            _createdMaterial = new Material(_material);
            _renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            _isRevealed = false;
            _renderer.material = _createdMaterial;
            //_renderer.material.mainTexture = _defaultTexture;
            _renderer.material.color = Color.blue;
        }

        public void SwapImage()
        {
            _isRevealed = !_isRevealed;

            if (_isRevealed)
            {
                //_renderer.material.mainTexture = _revealedTexture;
                _renderer.material.color = Color.red;
            }
            else
            {
                //_renderer.material.mainTexture = _defaultTexture;
                _renderer.material.color = Color.blue;
            }
        }
    }
}
