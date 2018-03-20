using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
/// <summary>
/// This is the main type for your game.
/// </summary>
public class Game1 : Game
{
GraphicsDeviceManager graphics;
SpriteBatch spriteBatch;


public Game1()
{
	graphics = new GraphicsDeviceManager(this);
	Content.RootDirectory = "Content";
	this.IsMouseVisible=true;
}

/// <summary>
/// Allows the game to perform any initialization it needs to before starting to run.
/// This is where it can query for any required services and load any non-graphic
/// related content.  Calling base.Initialize will enumerate through any components
/// and initialize them as well.
/// </summary>
protected override void Initialize()
{
	// TODO: Add your initialization logic here

	base.Initialize();
}

/// <summary>
/// LoadContent will be called once per game and is the place to load
/// all of your content.
/// </summary>
protected override void LoadContent()
{
	// Create a new SpriteBatch, which can be used to draw textures.
	spriteBatch = new SpriteBatch(GraphicsDevice);

	// TODO: use this.Content to load your game content here
}

/// <summary>
/// UnloadContent will be called once per game and is the place to unload
/// game-specific content.
/// </summary>
protected override void UnloadContent()
{
	// TODO: Unload any non ContentManager content here
}

/// <summary>
/// Allows the game to run logic such as updating the world,
/// checking for collisions, gathering input, and playing audio.
/// </summary>
/// <param name="gameTime">Provides a snapshot of timing values.</param>
protected override void Update(GameTime gameTime)
{
	if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
		Exit();

	// TODO: Add your update logic here
	mouseTest();

	base.Update(gameTime);
}



/// <summary>
/// This is called when the game should draw itself.
/// </summary>
/// <param name="gameTime">Provides a snapshot of timing values.</param>
protected override void Draw(GameTime gameTime)
{
	GraphicsDevice.Clear(Color.CornflowerBlue);

	base.Draw(gameTime);
}





int screenWidth => GraphicsDevice.PresentationParameters.BackBufferWidth;
int screenHeight => GraphicsDevice.PresentationParameters.BackBufferHeight;


Point lastMousePosition;
bool lastFrameMouseWasCentered=false;


void mouseTest ()
{

	MouseState newState=Mouse.GetState();

	int deltaX=newState.X-lastMousePosition.X;
	int deltaY=newState.Y-lastMousePosition.Y;

	Debug.WriteLine ("Position: {0},{1}     Delta: {2},{3}",newState.X,newState.Y,deltaX,deltaY);


	// try to "jail" the mouse inside the screen: Move to the center when it is out of bounds
	if (newState.X<0 || newState.X>=screenWidth
	 || newState.Y<0 || newState.Y>=screenHeight)
	{
		// the exception can be triggered if you move the mouse very very fast too. Just move it 
		// at normal and constant speed
		if (lastFrameMouseWasCentered) throw new System.Exception ("Error: Mouse was centered but it's still out of bounds after 1 frame!");

		Mouse.SetPosition (screenWidth/2,screenHeight/2);

		lastMousePosition=new Point (screenWidth/2,screenHeight/2);			// so that next frame's delta is correct
		lastFrameMouseWasCentered=true;
	}
	else
	{
		lastFrameMouseWasCentered=false;
		lastMousePosition=newState.Position;
	}



}

}
}
