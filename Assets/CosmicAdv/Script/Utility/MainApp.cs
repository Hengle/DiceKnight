﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;

public class MainApp : Singleton<MainApp> {
	protected MainApp () {} // guarantee this will be always a singleton only - can't use the constructor!

	public Subject subject;

	public T FindObject<T>(string p_path) where T : Object {
		Transform t_view = transform.Find(p_path);
		if (t_view) return t_view.GetComponent<T>();

		return default(T);
	}
	
	private Observer[] observers = new Observer[0];

	void Awake() {
		//Set up event notificaiton
		subject = new Subject();

		RegisterAllController(subject);
		subject.notify(EventFlag.Game.SetUp);
	}

	public T GetObserver<T>() where T : Observer {
		
		foreach (Observer observer in observers) {
			if (observer.GetType() == typeof(T)) return (T)observer;
		}

		return default(T);
	}

	void RegisterAllController(Subject p_subject) {
		Transform ctrlHolder = transform.Find("controller");
		if (ctrlHolder == null) return;
		observers = ctrlHolder.GetComponentsInChildren<Observer>();
		
		foreach (Observer observer in observers) {
			subject.addObserver( observer );
		}
	}

}
