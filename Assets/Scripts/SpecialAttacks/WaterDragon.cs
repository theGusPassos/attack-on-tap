using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackOnTap.SpecialAttacks
{
    public class WaterDragon : MonoBehaviour
    {
        public GameObject _base;
        public GameObject body;
        public GameObject dragonHead;

        public List<GameObject> otherObjects;

        private float timer;
        public float timeToCreate;
        private int created = 1;
        public float maxTime;

        private List<GameObject> sprites;
        
        private void Start()
        {
            if (dragonHead != null)
            {
                dragonHead.SetActive(true);
            }

            sprites = new List<GameObject>();

            sprites.Add(_base);

            if(dragonHead != null)
                sprites.Add(dragonHead);

            sprites.AddRange(otherObjects);
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer > timeToCreate * created)
            {
                GameObject t = Instantiate(body);
                t.transform.position = _base.transform.position;
                created++;
                    
                sprites.Add(t);
            }

            if (timer > maxTime)
            {
                StartCoroutine(DestroyObjects());
            }
        }

        private IEnumerator DestroyObjects()
        {
            while (sprites[0].GetComponent<SpriteRenderer>().color.a > 0)
            {
                foreach (GameObject sprite in sprites)
                {
                    sprite.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f * Time.deltaTime); 
                }

                yield return new WaitForSeconds(0.1f);
            }

            foreach(GameObject sprite in sprites)
                Destroy(sprite);

            Destroy(gameObject);
        }
    }
}
