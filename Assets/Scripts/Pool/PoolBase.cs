using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase {

	//private List<IpoolObj> _freeObj = new List<IpoolObj>();
	//private List<IpoolObj> _busyObj = new List<IpoolObj> ();

	private Dictionary<string, List<IpoolObj>> _freeObjs = new Dictionary<string, List<IpoolObj>> ();
	private Dictionary<string, List<IpoolObj>> _busyObjs = new Dictionary<string, List<IpoolObj>> ();

    //提供池子里的给过去
    public IpoolObj Spawn(string path)
	{
		if (!_freeObjs.ContainsKey(path)) {
			_freeObjs.Add (path, new List<IpoolObj> ());
		}
		if (!_busyObjs.ContainsKey(path)) {
			_busyObjs.Add (path, new List<IpoolObj> ());
		}
        if (_freeObjs[path].Count > 0)
        {
            IpoolObj obj = null;
            List<IpoolObj> pool = _freeObjs[path];
            for (int i = 0; i < pool.Count; i++)
            {
                obj = pool[i];
                if (obj.Equals(null))
                {
                    pool.RemoveAt(i);
                }
                else
                {
                    _freeObjs[path].RemoveAt(i);
                    _busyObjs[path].Add(obj);
                    obj.Path = path;
                    obj.OnSpawn();
                    return obj;
                }
            }
        }

        GameObject res = (Resources.Load(path) as GameObject);
        if (res == null)
        {
            Debug.LogError("resources里没有 : " + path);
            return null;
        }
        else
        {
            GameObject obj = (GameObject)GameObject.Instantiate(res);
            IpoolObj iobj = obj.GetComponent<IpoolObj>();
            iobj.Path = path;
            iobj.OnSpawn();
            _busyObjs[path].Add(iobj);
            return iobj;
        }

        return null;
	}

	//回收到池子里
	public void UnSpawn(string path, IpoolObj obj)
	{
        if (obj == null)
            return;
        if (!_freeObjs.ContainsKey(path)) {
			_freeObjs.Add (path, new List<IpoolObj> ());
		}
		if (!_busyObjs.ContainsKey(path)) {
			_busyObjs.Add (path, new List<IpoolObj> ());
		}

		if (!_freeObjs [path].Contains(obj)) {
			_freeObjs [path].Add (obj);	
			obj.OnUnSpawn ();
		}

		if (_busyObjs[path].Contains(obj)) {
			_busyObjs [path].Remove (obj);
		}

	}
   public void DeleteSpawn(string path, FxBase obj)
    {
        if (obj == null)
            return;

        if (!_freeObjs[path].Contains(obj))
        {
            _freeObjs[path].Remove(obj);
        }

        if (_busyObjs[path].Contains(obj))
        {
            _busyObjs[path].Remove(obj);
        }
    }
}
public class PoolBaseObj<T> where T : new()
{
    private List<T> _freeObjs = new List<T>();
    private List<T> _busyObjs = new List<T>();
    //提供池子里的给过去
    public T Spawn()
    {
        T t;
        if (_freeObjs.Count <= 0)
        {
            t = new T();
            _busyObjs.Add(t);
            return t;
        }
        t = _freeObjs[0];
        _freeObjs.RemoveAt(0);
        _busyObjs.Add(t);
        return t;
    }

    //回收到池子里
    public void UnSpawn(T obj)
    {
        if (obj == null)
            return;

        if (!_freeObjs.Contains(obj))
        {
            _freeObjs.Add(obj);
        }
        if (_busyObjs.Contains(obj))
        {
            _busyObjs.Remove(obj);
        }

    }
}
