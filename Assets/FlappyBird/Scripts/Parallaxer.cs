using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour {
class PoolObject{
	public Transform transform;
	public bool inUse;
	public PoolObject (Transform t) { transform = t; }
	public void Use() { inUse = true; }
	public void Dispose() { inUse = false; }
}

[System.Serializable]public struct YSpanwRange{       //Serializable於unity為可見
	public float min;
	public float max;
}
public GameObject Prefab;            //生成物
public int poolSize;                 //size?
public float shiftSpeed;             //畫面移動速度
public float spawnRate;              //生成速率

public YSpanwRange ySpanwRange;      //生成範圍   此struct 含有兩個float 使用時用 ySpanwPrange.min ...etc
public Vector3 defaultSpawnPos;      //生成位置
public bool spawnImmediate;          //particle prewarm      跟partical有點像?   partical要一開始就生成blablabla   還不會
public Vector3 immediateSpawnPos;    //immediate生成位置   跟 defaultSpawnPos 不太一樣
public Vector2 targetAspectRatio;    //若銀幕太寬  生成得在銀幕外?????
float spawnTimer;
float targetAspect;                  //還是銀幕??????
PoolObject[] poolObjects;            //array
GameManager1 game;                   //a reference to GameManager

void Awake()
{
	Configure();
}

void Start()                         //GameManager 用的是 Awake()，且 Start() is called after Awake()
{
	game = GameManager1.Instance;    //所以可以確保game 不會存成null
}

void OnEnable()
{
	GameManager1.OnGameOverConfirmed += OnGameOverConfirmed;
}
void OnDisable()
{
	GameManager1.OnGameOverConfirmed -= OnGameOverConfirmed;
}

void OnGameOverConfirmed(){
	for(int i = 0; i < poolObjects.Length ; i ++){
		poolObjects[i].Dispose();
		poolObjects[i].transform.position = Vector3.one * 1000;
	}
	if(spawnImmediate){
		SpawnImmediate();
	}
}
void Update()
{
	if (game.GameOver) return;
	Shift();                         //穩定shift
	spawnTimer += Time.deltaTime;
	if(spawnTimer > spawnRate){      //間隔生成
		Spawn();
		spawnTimer = 0;
	}

}
void Configure(){
	targetAspect = targetAspectRatio.x / targetAspectRatio.y;    //銀幕長寬比  如何運作???????
	poolObjects = new PoolObject[poolSize];                      //poolObjects array ???????
	for (int i = 0 ; i < poolObjects.Length ; i++){
		GameObject go = Instantiate(Prefab) as GameObject;       //複製
		Transform t = go.transform;                              //存下transform
		t.SetParent(transform);                                  //設t的父物件的transform=t   ??????
		t.position = Vector3.one *9999;							 //off screen
		poolObjects[i] = new PoolObject(t);                      //????????
 	}
	if (spawnImmediate)
	{
		SpawnImmediate();
	}
}

void Spawn(){
	Transform t = GetPoolObject();
	if (t == null) { return; }          // if true, this indicates that poolSize is too small
	Vector3 pos = Vector3.zero;
	pos.x = (defaultSpawnPos.x * Camera.main.aspect) / targetAspect;
	pos.y = Random.Range( ySpanwRange.min, ySpanwRange.max );
	t.position = pos;
}
void SpawnImmediate(){
	Transform t = GetPoolObject();
	if (t == null) { return; }          // if true, this indicates that poolSize is too small
	Vector3 pos = Vector3.zero;
	pos.x = (immediateSpawnPos.x * Camera.main.aspect) / targetAspect;
	pos.y = Random.Range( ySpanwRange.min, ySpanwRange.max );
	t.position = pos;
	Spawn();
}

void Shift(){//move the parallax object
	for(int i = 0; i < poolObjects.Length ; i++ ){
		poolObjects[i].transform.localPosition += -Vector3.right * shiftSpeed * Time.deltaTime;
		CheckDisposeObject(poolObjects[i]);
	}
}

void CheckDisposeObject(PoolObject poolObject){   //check用
	if (poolObject.transform.position.x < (-defaultSpawnPos.x * Camera.main.aspect)/ targetAspect){
		poolObject.Dispose();
		poolObject.transform.position = Vector2.one * 9999;          // 用不到了
	}
}

Transform GetPoolObject(){
	for (int i = 0; i < poolObjects.Length;i++){
		if(!poolObjects[i].inUse){
			poolObjects[i].Use();
			return poolObjects[i].transform;
		}
	}
	return null;
}
}