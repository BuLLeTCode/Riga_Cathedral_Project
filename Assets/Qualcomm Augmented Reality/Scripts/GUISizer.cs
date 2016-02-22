/*
* Script GUISizer by Sergey Mohov (http://sergeymohov.com)
*
* Original idea for the resize algorithm by Simon Wittber (http://entitycrisis.blogspot.fr/)
*
* Feel free to reproduce this and use for any commercial or non-commercial projects.
*
* Although you don't have to, it would be nice of you to mention my name and link to my website
* if you decide to put this on the Wiki or expand.
*
* If you have questions, email me: sergey@sergeymohov.com
*
* */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GUISizer
{
				//get the screen resolution once, then adjust the GUI size to that resolution
		public static float WIDTH = 960;
		public static float HEIGHT = 600;

				//Get the button sizes
				public const float SMALL_BUTTON_WIDTH = 100f;
				public const float SMALL_BUTTON_HEIGHT = 50f;
				public const float MEDIUM_BUTTON_WIDTH = 200f;
				public const float MEDIUM_BUTTON_HEIGHT = 100f;
				public const float LARGE_BUTTON_WIDTH = 300f;
				public const float LARGE_BUTTON_HEIGHT = 100f;
				public const float BUTTON_GAP = 10f;

				private static int fontSizeDefault = 12; //GUI skin's font size should be reset to its default value every time

		static List<Matrix4x4> stack = new List<Matrix4x4> ();

				//call this funtion in the beginning of OnGUI
		static public void BeginGUI(PositionDef elementsPosition = PositionDef.middle)
				{
				stack.Add (GUI.matrix);
				Matrix4x4 m = new Matrix4x4 ();
				float w = (float)Screen.width;
				float h = (float)Screen.height;
				float aspect = w / h;
				float scale = 1f;
				Vector3 offset = Vector3.zero;

				if(aspect < (WIDTH/HEIGHT))
								{
												//screen is taller
												switch(elementsPosition)
												{
																case PositionDef.topLeft:
												scale = (Screen.width/WIDTH);
																				break;
																case PositionDef.topMiddle:
												scale = (Screen.width/WIDTH);
																				break;
																case PositionDef.topRight:
												scale = (Screen.width/WIDTH);
																				break;
																case PositionDef.bottomLeft:
												scale = (Screen.width/WIDTH);
												offset.y += (Screen.height - (HEIGHT * scale));
																				break;
																case PositionDef.bottomMiddle:
												scale = (Screen.width/WIDTH);
												offset.y += (Screen.height - (HEIGHT * scale));
																				break;
																case PositionDef.bottomRight:
												scale = (Screen.width/WIDTH);
												offset.y += (Screen.height - (HEIGHT * scale));
																				break;
																case PositionDef.left:
												scale = (Screen.width/WIDTH);
												offset.y += (Screen.height - (HEIGHT * scale)) * 0.5f;
																				break;
																case PositionDef.right:
												scale = (Screen.width/WIDTH);
												offset.y += (Screen.height - (HEIGHT * scale)) * 0.5f;
																				break;
																case PositionDef.middle:
												scale = (Screen.width/WIDTH);
												offset.y += (Screen.height - (HEIGHT * scale)) * 0.5f;
																				break;
																default:
																				scale = (Screen.width/WIDTH);
												offset.y += (Screen.height - (HEIGHT * scale)) * 0.5f;
																				break;
												}
								}
								else
								{
												//screen is wider
												switch(elementsPosition)
												{
																case PositionDef.topLeft:
												scale = (Screen.height / HEIGHT);
																				break;
																case PositionDef.topMiddle:
												scale = (Screen.height / HEIGHT);
																				offset.x += (Screen.width - (WIDTH * scale)) * 0.5f;
																				break;
																case PositionDef.topRight:
																				scale = (Screen.height / HEIGHT);
												offset.x += (Screen.width - (WIDTH * scale));
																				break;
																case PositionDef.bottomLeft:
												scale = (Screen.height / HEIGHT);
																				break;
																case PositionDef.bottomMiddle:
												scale = (Screen.height / HEIGHT);
												offset.x += (Screen.width - (WIDTH * scale)) * 0.5f;
																				break;
																case PositionDef.bottomRight:
												scale = (Screen.height / HEIGHT);
												offset.x += (Screen.width - (WIDTH * scale));
																				break;
																case PositionDef.left:
												scale = (Screen.height / HEIGHT);
																				break;
																case PositionDef.right:
												scale = (Screen.height / HEIGHT);
												offset.x += (Screen.width - (WIDTH * scale));
																				break;
																case PositionDef.middle:
												scale = (Screen.height / HEIGHT);
												offset.x += (Screen.width - (WIDTH * scale)) * 0.5f;
																				break;
																default:
														scale = (Screen.height / HEIGHT);
														offset.x += (Screen.width - (WIDTH * scale)) * 0.5f;
																				break;
												}
				}

				m.SetTRS(offset, Quaternion.identity, Vector3.one * scale);
				GUI.matrix *= m;
		}

				//call this function in the end of OnGUI
		static public void EndGUI()
				{
				GUI.matrix = stack[stack.Count - 1];
				stack.RemoveAt (stack.Count - 1);
		}

				//enum that defines position presets
				public enum PositionDef
				{
								topLeft,
								topMiddle,
								topRight,
								bottomLeft,
								bottomMiddle,
								bottomRight,
								left,
								right,
								middle
				}

				//enum that defines size presets
				public enum SizeDef
				{
								small,
								medium,
								big
				}

				#region methods
				//a method that converts GUIParams into a Rect
				public static Rect MakeRect (GUIParams guiParams)
				{
								Rect rectangle = new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height);

								return rectangle;
				}

				//draws a button and returns true if it was pressed
				public static bool ButtonPressed(GUIParams guiParams)
				{
								GUI.skin.button.fontSize = (int)guiParams.fontSize;
								if (GUI.Button(new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height), guiParams.text))
								{
												GUI.skin.button.fontSize = fontSizeDefault;
												return true;
								}
								else
								{
												GUI.skin.button.fontSize = fontSizeDefault;
												return false;
								}
				}

				//overload method that lets you make custom font sizes
				public static bool ButtonPressed(GUIParams guiParams, int customFontSize)
				{
								GUI.skin.button.fontSize = customFontSize;
								if (GUI.Button(new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height), guiParams.text))
								{
												GUI.skin.button.fontSize = fontSizeDefault;
												return true;
								}
								else
								{
												GUI.skin.button.fontSize = fontSizeDefault;
												return false;
								}
				}

				//draws a label
				public static void MakeLabel(GUIParams guiParams, string additionalText = "")
				{
								GUI.skin.label.fontSize = (int)guiParams.fontSize;
								GUI.Label(new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height), guiParams.text + additionalText);
								GUI.skin.label.fontSize = fontSizeDefault;
				}

				//overload method that lets you make custom font sizes
				public static void MakeLabel(GUIParams guiParams, int customFontSize, string additionalText = "")
				{
								GUI.skin.label.fontSize = customFontSize;
								GUI.Label(new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height), guiParams.text + additionalText);
								GUI.skin.label.fontSize = fontSizeDefault;
				}

				#endregion

				#region overload methods that let you define a custom style for your GUI element
				//draws a button and returns true if it was pressed
				public static bool ButtonPressed(GUIParams guiParams, GUIStyle style)
				{
								GUI.skin.button.fontSize = (int)guiParams.fontSize;
								if (GUI.Button(new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height), guiParams.text, style))
								{
												GUI.skin.button.fontSize = fontSizeDefault;
												return true;
								}
								else
								{
												GUI.skin.button.fontSize = fontSizeDefault;
												return false;
								}
				}

				//overload method that lets you make custom font sizes
				public static bool ButtonPressed(GUIParams guiParams, int customFontSize, GUIStyle style)
				{
								GUI.skin.button.fontSize = customFontSize;
								if (GUI.Button(new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height), guiParams.text, style))
								{
												GUI.skin.button.fontSize = fontSizeDefault;
												return true;
								}
								else
								{
												GUI.skin.button.fontSize = fontSizeDefault;
												return false;
								}
				}

				//draws a label
				public static void MakeLabel(GUIParams guiParams, GUIStyle style , string additionalText = "")
				{
								GUI.skin.label.fontSize = (int)guiParams.fontSize;
								GUI.Label(new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height), guiParams.text + additionalText, style);
								GUI.skin.label.fontSize = fontSizeDefault;
				}

				//overload method that lets you make custom font sizes
				public static void MakeLabel(GUIParams guiParams, int customFontSize, GUIStyle style , string additionalText = "")
				{
								GUI.skin.label.fontSize = customFontSize;
								GUI.Label(new Rect(guiParams.x, guiParams.y, guiParams.width, guiParams.height), guiParams.text + additionalText, style);
								GUI.skin.label.fontSize = fontSizeDefault;
				}
				#endregion


				//struct for GUI elements
				public struct GUIParams
				{
								//position of the button on screen
								public float x;
								public float y;

								//size of the button
								public float width;
								public float height;

								/*size of the font of the text written on the button, proportional to the length of the button
								instead of writing 7 more constructors for custom font sizes, you can override the font in all methods that draw GUI elements*/
								public float fontSize;

								public string text; //text written in the button

								#region Default style constructors
								//default everything
								public GUIParams(string guiText)
								{
												text = guiText;

												//size defaults to medium
												Vector2 buttonSize = SetSize(SizeDef.medium);
												width = buttonSize.x;
												height = buttonSize.y;

												//position defaults to middle
												Vector2 buttonPos = SetPosition(PositionDef.middle, width, height);
												x = buttonPos.x;
												y = buttonPos.y;

												fontSize = width/6;
								}

								//specify all params manually
								public GUIParams (float posX, float posY, float guiWidth, float guiHeight, string guiText)
								{
												text = guiText;

												x = posX;
												y = posY;

												width = guiWidth;
												height = guiHeight;

												fontSize = width/6;
								}

								//specify position manually, choose size from the list
								public GUIParams (float posX, float posY, SizeDef size, string guiText)
								{
												text = guiText;

												Vector2 buttonSize = SetSize(size);
												width = buttonSize.x;
												height = buttonSize.y;

												x = posX;
												y = posY;

												fontSize = width/6;
								}

								//specify size manually, choose position from the list
								public GUIParams (PositionDef position, float guiWidth, float guiHeight, string guiText)
								{
												text = guiText;

												width = guiWidth;
												height = guiHeight;

												Vector2 buttonPos = SetPosition(position, width, height);
												x = buttonPos.x;
												y = buttonPos.y;

												fontSize = width/6;
								}

								//choose size from the list, default position
								public GUIParams (SizeDef size, string guiText)
								{
												text = guiText;

												Vector2 buttonSize = SetSize(size);
												width = buttonSize.x;
												height = buttonSize.y;

												//position defaults to middle
												Vector2 buttonPos = SetPosition(PositionDef.middle, width, height);
												x = buttonPos.x;
												y = buttonPos.y;


												fontSize = width/6;
								}

								//choose position from the list, default size
								public GUIParams (PositionDef position, string guiText)
								{
												text = guiText;

												//size defaults to medium
												Vector2 buttonSize = SetSize(SizeDef.medium);
												width = buttonSize.x;
												height = buttonSize.y;

												Vector2 buttonPos = SetPosition(position, width, height);
												x = buttonPos.x;
												y = buttonPos.y;

												fontSize = width/6;
								}

								//choose size and position from the list
								public GUIParams (PositionDef position, SizeDef size, string guiText)
								{
												text = guiText;

												Vector2 buttonSize = SetSize(size);
												width = buttonSize.x;
												height = buttonSize.y;

												Vector2 buttonPos = SetPosition(position, width, height);
												x = buttonPos.x;
												y = buttonPos.y;

												fontSize = width/6;
								}
								#endregion
				}

				//button sizes can be set in this method
				private static Vector2 SetSize(SizeDef size)
				{
								Vector2 finalSize;

								switch (size)
								{
												case SizeDef.small:
																finalSize = new Vector2(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT);
																break;

												case SizeDef.medium:
																finalSize = new Vector2(MEDIUM_BUTTON_WIDTH, MEDIUM_BUTTON_HEIGHT);
																break;

												case SizeDef.big:
																finalSize = new Vector2(LARGE_BUTTON_WIDTH, LARGE_BUTTON_HEIGHT);
																break;
												default:
																Debug.LogError("Unknown button size, defaults to medium.");
																finalSize = new Vector2(MEDIUM_BUTTON_WIDTH, MEDIUM_BUTTON_HEIGHT);
																break;
								}

								return finalSize;
				}

				//button positions can be set in this method
				private static Vector2 SetPosition(PositionDef position, float width, float height)
				{
								Vector2 finalPosition;

												switch (position)
												{
																case PositionDef.topLeft:
																				finalPosition = new Vector2(BUTTON_GAP, BUTTON_GAP);
																				break;

																case PositionDef.topMiddle:
																				finalPosition = new Vector2(WIDTH/2-width/2, BUTTON_GAP);
																				break;

																case PositionDef.topRight:
																				finalPosition = new Vector2(WIDTH-width-BUTTON_GAP, BUTTON_GAP);
																				break;

																case PositionDef.bottomLeft:
																				finalPosition = new Vector2(BUTTON_GAP, HEIGHT-height-BUTTON_GAP);
																				break;

																case PositionDef.bottomMiddle:
																				finalPosition = new Vector2(WIDTH/2-width/2, HEIGHT-height-BUTTON_GAP);
																				break;

																case PositionDef.bottomRight:
																				finalPosition = new Vector2(WIDTH-width-BUTTON_GAP, HEIGHT-height-BUTTON_GAP);
																				break;

																case PositionDef.left:
																				finalPosition = new Vector2(BUTTON_GAP, HEIGHT/2-height/2);
																				break;

																case PositionDef.right:
																				finalPosition = new Vector2(WIDTH-width-BUTTON_GAP, HEIGHT/2-height/2);
																				break;

																case PositionDef.middle:
																				finalPosition = new Vector2(WIDTH/2-width/2, HEIGHT/2-height/2);
																				break;

																default:
																				finalPosition = new Vector2(WIDTH/2-width/2, HEIGHT/2-height/2);
																				Debug.LogError("Undefined position, assigned to middle.");
																				break;
												}

								return finalPosition;
				}
}
