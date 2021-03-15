using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class CollectionManager : Singleton<CollectionManager>
{
	//References
	[Header("UI references")]
	[SerializeField] TMP_Text coinUIText;
	[SerializeField] GameObject animatedCoinPrefab;
	[SerializeField] Transform target;

	[Space]
	[Header("Available coins : (coins to pool)")]
	[SerializeField] int maxCoins;
	Queue<GameObject> coinsQueue = new Queue<GameObject>();


	[Space]
	[Header("Animation settings")]
	[SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;

	Vector3 targetPosition;


	private int _c = 0;

	public int Coins
	{
		get { return _c; }
		set
		{
			_c = value;
			//update UI text whenever "Coins" variable is changed
			//coinUIText.text = Coins.ToString();
		}
	}

	void Awake()
	{

		Vector3 screenPoint = target.position + new Vector3(0, 0, 5);  //the "+ new Vector3(0,0,5)" ensures that the object is so close to the camera you dont see it

		//find out where this is in world space
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);


		targetPosition = worldPos;

		//prepare pool
		PrepareCoins();
	}

	void PrepareCoins()
	{
		GameObject coin;
		for (int i = 0; i < maxCoins; i++)
		{
			coin = Instantiate(animatedCoinPrefab);
			coin.transform.parent = transform;
			coin.SetActive(false);
			coinsQueue.Enqueue(coin);
		}
	}

	public void AddCoins(Vector3 collectedCoinPosition, Dictionary<PlantProperty, int> resource)
	{
		foreach (var pair in resource)
        {
			var amount = pair.Value;
			//get target position
			var target = HUD.Instance.hudByProperty[pair.Key].GetComponent<OneStatHud>().image.transform;
			Vector3 screenPoint = target.position + new Vector3(0, 0, 5);  //the "+ new Vector3(0,0,5)" ensures that the object is so close to the camera you dont see it

			//find out where this is in world space
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);


			targetPosition = worldPos;
			for (int i = 0; i < amount; i++)
			{
				//check if there's coins in the pool
				if (coinsQueue.Count > 0)
				{
					//extract a coin from the pool
					GameObject coin = coinsQueue.Dequeue();
					coin.GetComponent<SpriteRenderer>().sprite = HUD.Instance.propertyImage[(int)(pair.Key)];
					coin.SetActive(true);

					//move coin to the collected coin pos
					coin.transform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);




					//animate coin to target position
					float duration = Random.Range(minAnimDuration, maxAnimDuration);
					coin.transform.DOMove(targetPosition, duration)
					.SetEase(easeType)
					.OnComplete(() =>
					{
					//executes whenever coin reach target position
					coin.SetActive(false);
						coinsQueue.Enqueue(coin);

						PlantsManager.Instance.currentResource[pair.Key] += 1;
					});
				}
			}
		}
			
	}

}