using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPTable {
	
	public static EXPTable Normal;

	double exp_basic = 15;
	double exp_rate = 1.6;
	double exp_rate_rate = 0.98;
	double temp_final = 0;

	private List<int> _expTable;
	public List<int> ExpTable {
		get { return _expTable; }
	}

	static EXPTable(){
		Debug.Log ("exp init");
		EXPTable m_table = new EXPTable ();

		m_table._expTable = new List<int> ();
		m_table._expTable.Add (0); // 0等
		m_table._expTable.Add (0); // 1等
		for (int i = 2; i < 100; i++)
		{
			m_table.temp_final += m_table.exp_basic;
			m_table._expTable.Add(System.Convert.ToInt32(m_table.temp_final));
			if (i < 50) {
				m_table.exp_basic = m_table.exp_basic * m_table.exp_rate;
				m_table.exp_rate = (m_table.exp_rate * m_table.exp_rate_rate) < 1.1 ? 1.1 : m_table.exp_rate * m_table.exp_rate_rate;
			}
		}
		//For level 99
		m_table._expTable.Add (0);

		Normal = m_table;
	}

	public int GetNextNeedExp(StandardActor actor){
		return _expTable [actor.Level + 1] - actor.EXP;
	}

	public int GetToNextEXP(StandardActor actor){
		return _expTable [actor.Level + 1];
	}

	public int GetThisLevelBaseExp(StandardActor actor){
		return _expTable [actor.Level];
	}

	public int GetToNextEXP(int lv){
		return _expTable [lv + 1];
	}

	public int GetThisLevelBaseExp(int lv){
		return _expTable [lv];
	}
		
}
