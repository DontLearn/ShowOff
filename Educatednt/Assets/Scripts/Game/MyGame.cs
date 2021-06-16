/*using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine
using System.Drawing;

public class MyGame : Game
{
	public MyGame() : base(800, 600, false)		// Create a window that's 800x600 and NOT fullscreen
	{
        PersistentData persistentData = PersistentData.instance;
        persistentData.SaveAllPersistentItems();

        //Scene.Load("Scene 2");

        persistentData.LoadAllPersistentItems();
    }

    void Update()
	{
		// Empty
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}*/