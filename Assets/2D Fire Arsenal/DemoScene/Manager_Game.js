#pragma strict
var text_fx_name : TextMesh;
var text_trail : TextMesh;
var fx_prefabs : GameObject[];
var index_fx : int = 0;

private var ray : Ray;
private var trail_ray : Ray;
private var ray_cast_hit : RaycastHit2D;
private var SHOWING_TRAIL : boolean = false;
private var current_trail : GameObject;


function Start () {
	text_fx_name.text = fx_prefabs[index_fx].name;
}


function Update () {
	if(Input.GetMouseButtonDown(0)){
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		ray_cast_hit = Physics2D.Raycast(Vector2(ray.origin.x, ray.origin.y), Vector2.zero);
		if(	ray_cast_hit ){
			switch(ray_cast_hit.transform.name){
				case "BG":
					if ( index_fx >= 4 && index_fx <= 8)
						MuzzleShot();
					else if ( index_fx >= 9 && index_fx <= 14)
						print("these are trails!");
					else
						Instantiate(fx_prefabs[ index_fx ], Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.identity);
					break;
				case "UI-arrow-right":
					ray_cast_hit.transform.GetComponent(Pressed_Button_Anim).Go();
					index_fx++;
					if(index_fx >= fx_prefabs.Length)
						index_fx = 0;
					text_fx_name.text = fx_prefabs[index_fx].name;
					//trail stuff
					if ( index_fx >= 9 && index_fx <= 14){
						text_trail.transform.position.z = -2;
						SpawnTrail();
					}else{
						text_trail.transform.position.z = 2;
						SHOWING_TRAIL = false;
						}
					break;
				case "UI-arrow-left":
					ray_cast_hit.transform.GetComponent(Pressed_Button_Anim).Go();
					index_fx--;
					if(index_fx <= -1)
						index_fx = fx_prefabs.Length - 1;
					text_fx_name.text = fx_prefabs[index_fx].name;
					//trail stuff
					if ( index_fx >= 9 && index_fx <= 14){
						text_trail.transform.position.z = -2;
						SpawnTrail();
					}else{
						text_trail.transform.position.z = 2;
						SHOWING_TRAIL = false;
						}
					break;
				case "Instructions":
					Destroy(ray_cast_hit.transform.gameObject);
					break;
			}
		}				
	}
	
	//Change-FX keyboard..	
	if ( Input.GetKeyDown("z") || Input.GetKeyDown("left") ){
		GameObject.Find("UI-arrow-left").GetComponent(Pressed_Button_Anim).Go();
		index_fx--;
		if(index_fx <= -1)
			index_fx = fx_prefabs.Length - 1;
		text_fx_name.text = fx_prefabs[index_fx].name;
		//trail stuff
		if ( index_fx >= 9 && index_fx <= 14){
			text_trail.transform.position.z = -2;
			SpawnTrail();
		}else{
			text_trail.transform.position.z = 2;
			SHOWING_TRAIL = false;
			}
	}
	
	if ( Input.GetKeyDown("x") || Input.GetKeyDown("right")){
		GameObject.Find("UI-arrow-right").GetComponent(Pressed_Button_Anim).Go();
		index_fx++;
		if(index_fx >= fx_prefabs.Length)
			index_fx = 0;
		text_fx_name.text = fx_prefabs[index_fx].name;
		//trail stuff
		if ( index_fx >= 9 && index_fx <= 14){
			text_trail.transform.position.z = -2;
			SpawnTrail();
		}else{
			text_trail.transform.position.z = 2;
			SHOWING_TRAIL = false;
			}
	}
	
	if ( Input.GetKeyDown("space") ){
		//Debug.Break();
		if( index_fx >= 9 && index_fx <= 14)
			print("these are trails!");
		else
			Instantiate(fx_prefabs[ index_fx ], Vector3(0, 1, 0), Quaternion.identity);	
	}

	//Trails..	
	if(SHOWING_TRAIL)
		MoveTrail();
}


function MuzzleShot(){
	var deltaX : float = ray.origin.x - 0;
	var deltaY : float = ray.origin.y - 1;	
	var angle : float = Mathf.Atan(deltaY/deltaX)*Mathf.Rad2Deg;
		
	if (deltaX < 0)
		angle += 180;
	if (angle < 0)
		angle += 360;
	transform.rotation.eulerAngles.z = angle;
	Instantiate(fx_prefabs[ index_fx ], Vector3(0, 1, 0), transform.rotation);	

}


function SpawnTrail(){
	var temp : GameObject;
	Destroy( GameObject.Find("TRAIL") );
	trail_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	temp = Instantiate(fx_prefabs[ index_fx ], Vector3(trail_ray.origin.x, trail_ray.origin.y, 0), Quaternion.identity);
	temp.name = "TRAIL";
	current_trail = temp;
	SHOWING_TRAIL = true;
}


function MoveTrail(){
	trail_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	current_trail.transform.position = Vector3(trail_ray.origin.x, trail_ray.origin.y, 0);
}