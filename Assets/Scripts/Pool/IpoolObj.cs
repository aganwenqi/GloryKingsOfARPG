using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IpoolObj {

	string Path { get; set;}

	void OnSpawn ();

	void OnUnSpawn();

}
