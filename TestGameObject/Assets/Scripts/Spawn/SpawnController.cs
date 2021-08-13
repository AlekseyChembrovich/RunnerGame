using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    public class SpawnController : MonoBehaviour
    {
        public BlockMap[] transformBlocks;
        public BlockMap firstBlock;
        public Coin coin;

        private List<BlockMap> listBlock = new List<BlockMap>();

        private void Start() => listBlock.Add(firstBlock);

        private void Update()
        {
            if (!(Vector3.Distance(transform.position, listBlock[listBlock.Count - 1].end.position) < 3)) return;
            Spawn();
        }

        public void Spawn()
        {
            BlockMap newBlock = Instantiate(transformBlocks[Random.Range(0, transformBlocks.Length)]);
            newBlock.transform.position = listBlock[listBlock.Count - 1].end.position - newBlock.start.localPosition;
            listBlock.Add(newBlock);

            List<Coin> newCoins = new List<Coin>();
            for (int i = 0; i < newBlock.coinSpawns.Length; i++)
            {
                Coin newCoin = Instantiate(coin);
                newCoin.transform.position = newBlock.coinSpawns[i].position + (Vector3.up * 0.5F);
                newCoins.Add(newCoin);
            }

            foreach (var coin in newCoins)
            {
                coin.gameObject.transform.SetParent(newBlock.transform);
            }

            if (listBlock.Count < 3) return;
            Destroy(listBlock[0].gameObject);
            listBlock.RemoveAt(0);
        }
    }
}
