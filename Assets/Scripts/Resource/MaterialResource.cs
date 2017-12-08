using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialResource {

	public Material UISpark;


	public MaterialResource(){

		UISpark = Resources.Load("Material/UIImageGlow", typeof(Material)) as Material;
	}

}
