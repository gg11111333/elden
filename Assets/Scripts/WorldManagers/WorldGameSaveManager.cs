using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SG{
    public class worldgamesavemanager : MonoBehaviour
    {
        public static worldgamesavemanager instance;
        [SerializeField] int worldSceneIndex = 1;
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadoperatior = SceneManager.LoadSceneAsync(worldSceneIndex);
            
            yield return null; 
        
        }
    
        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }

}

